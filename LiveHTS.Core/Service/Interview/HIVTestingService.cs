using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Service.Interview
{
    public class HIVTestingService : IHIVTestingService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsTestResultRepository _obsTestResultRepository;
        private readonly IObsFinalTestResultRepository _obsFinalTestResultRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClientStateRepository _clientStateRepository;

        private List<CategoryItem> _categoryItems;
        
        public HIVTestingService(IEncounterRepository encounterRepository,
            IObsTestResultRepository obsTestResultRepository,
            IObsFinalTestResultRepository obsFinalTestResultRepository, ICategoryRepository categoryRepository, IClientStateRepository clientStateRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTestResultRepository = obsTestResultRepository;
            _obsFinalTestResultRepository = obsFinalTestResultRepository;
            _categoryRepository = categoryRepository;
            _clientStateRepository = clientStateRepository;
            LoadItems();
        }

        public Encounter OpenEncounter(Guid encounterId)
        {
            var exisitngEncounter = _encounterRepository.LoadTest(encounterId, true);
            return exisitngEncounter;
        }

        public Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId,Guid deviceId)
        {
            var exisitngEncounter = _encounterRepository
                .GetAll(x => x.EncounterTypeId == encounterTypeId &&
                             x.ClientId == clientId)
                .FirstOrDefault();

            if (null != exisitngEncounter)
            {
                return OpenEncounter(exisitngEncounter.Id);
            }

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId,practiceId,deviceId,null);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {

            return _encounterRepository.LoadTestAll(encounterTypeId, clientId, true).ToList();
        }

        public void SaveTest(ObsTestResult testResult,Guid clientId)
        {
            _obsTestResultRepository.SaveOrUpdate(testResult);            

            var final = _obsFinalTestResultRepository.GetAll(x => x.EncounterId == testResult.EncounterId)
                .FirstOrDefault();

            if (testResult.TestName.Contains("1"))
            {
                if (null != final)
                {
                    //         update
                    final.UpdateSetFirstResult(testResult.IsValid ? testResult.Result : Guid.Empty);
                    _obsFinalTestResultRepository.SaveOrUpdate(final);
                }
                else
                {
                    if (testResult.IsValid)
                    {
                        final = ObsFinalTestResult.CreateFirst(testResult.Result, testResult.EncounterId,clientId);
                        _obsFinalTestResultRepository.Save(final);
                    }
                }
            }

            if (testResult.TestName.Contains("2"))
            {
                if (null != final)
                {
                    //         update
                    final.UpdateSetSecondResult(testResult.IsValid ? testResult.Result : Guid.Empty);
                    _obsFinalTestResultRepository.SaveOrUpdate(final);
                }

            }

            UpdateFinalResult(testResult.EncounterId,clientId);
        }

        public void SaveFinalTest(ObsFinalTestResult testResult)
        {
            var test = _obsFinalTestResultRepository.Get(testResult.Id);
            if (null != test)
            {
                test.ResultGiven = testResult.ResultGiven;
                test.CoupleDiscordant = testResult.CoupleDiscordant;
                test.SelfTestOption = testResult.SelfTestOption;
                test.PnsDeclined = testResult.PnsDeclined;
                test.Remarks = testResult.Remarks;
                _obsFinalTestResultRepository.SaveOrUpdate(test);

                _clientStateRepository.DeleteState(test.ClientId, test.EncounterId);

                if (null != test.SelfTestOption && test.SelfTestOption.Value == new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                {
                    _clientStateRepository.SaveOrUpdate(new ClientState(test.ClientId, test.EncounterId, LiveState.HtsPnsAcceptedYes));
                }
                else
                {
                    _clientStateRepository.SaveOrUpdate(new ClientState(test.ClientId, test.EncounterId, LiveState.HtsPnsAcceptedNo));
                }
                _clientStateRepository.SaveOrUpdate(new ClientState(test.ClientId,test.EncounterId,ClientState.GetState(test.FinalResult.Value)));

            }
        }

        public void DeleteTest(ObsTestResult testResult,Guid clientId)
        {
            _obsTestResultRepository.Delete(testResult.Id);

            var final = _obsFinalTestResultRepository.GetAll(x => x.EncounterId == testResult.EncounterId)
                .FirstOrDefault();

            if (testResult.TestName.Contains("1"))
            {
                if (null != final)
                {
                    //         update
                    final.UpdateSetFirstResult(Guid.Empty);
                    _obsFinalTestResultRepository.SaveOrUpdate(final);
                }
            }
            if (testResult.TestName.Contains("2"))
            {
                if (null != final)
                {
                    //         update
                    final.UpdateSetSecondResult(Guid.Empty);
                    _obsFinalTestResultRepository.SaveOrUpdate(final);
                }
            }

            UpdateFinalResult(testResult.EncounterId,clientId);
        }

        public void UpdateFinalResult(Guid encounterId,Guid clientId)
        {
            _clientStateRepository.DeleteState(clientId,encounterId);
            LoadItems();

            var final = _obsFinalTestResultRepository.GetAll(x => x.EncounterId == encounterId)
                .FirstOrDefault();

            if (null != final)
            {
                final.ProcessEndResult(_categoryItems);
                _obsFinalTestResultRepository.SaveOrUpdate(final);
                if (null != final.FinalResult && !final.FinalResult.IsNullOrEmpty())
                {
                    _clientStateRepository.SaveOrUpdate(new ClientState(clientId, encounterId,
                        ClientState.GetState(final.FinalResult.Value)));
                }
                else
                {
                    _encounterRepository.UpdateStatus(final.EncounterId,false);
                }
            }

        }

        public void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed)
        {
            _encounterRepository.UpdateStatus(encounterId,userId,completed);
        }

        public void MarkEncounterCompleted(Guid encounterId, bool completed)
        {
            _encounterRepository.UpdateStatus(encounterId,  completed);
        }

        public void UpdateEncounterDate(Guid encounterId, Guid clientId)
        {
            var encounterDate = _encounterRepository.GetPretestEncounterDate(clientId);
            if (encounterDate > new DateTime(1901, 1, 1))
                _encounterRepository.UpdateEncounterDate(encounterId, encounterDate);
        }

        public bool IsIndividual(Guid clientId)
        {
            return _encounterRepository.GetIndividual(clientId);
        }

        public ObsFinalTestResult GetFinalTest(Guid clientId)
        {
            return _obsFinalTestResultRepository.Find(clientId).FirstOrDefault();
        }


        private void LoadItems()
        {
            if (null != _categoryItems && _categoryItems.Count > 0)
                return;

            _categoryItems = new List<CategoryItem>();

            var categotry = _categoryRepository.GetWithCode("TestResult");

            if (null != categotry)
            {
                var items = categotry.Items.ToList();
                if (null != items && items.Count > 0)
                {
                    _categoryItems.AddRange(items);
                }
            }

            var categotry2 = _categoryRepository.GetWithCode("FinalResult");

            if (null != categotry2)
            {
                var items = categotry2.Items.ToList();
                if (null != items && items.Count > 0)
                {
                    _categoryItems.AddRange(items);
                }
            }
        
        }
    }
}