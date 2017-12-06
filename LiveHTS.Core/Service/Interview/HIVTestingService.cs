using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Interview
{
    public class HIVTestingService : IHIVTestingService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsTestResultRepository _obsTestResultRepository;
        private readonly IObsFinalTestResultRepository _obsFinalTestResultRepository;
        private readonly ICategoryRepository _categoryRepository;

        private List<CategoryItem> _categoryItems;
        
        public HIVTestingService(IEncounterRepository encounterRepository,
            IObsTestResultRepository obsTestResultRepository,
            IObsFinalTestResultRepository obsFinalTestResultRepository, ICategoryRepository categoryRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTestResultRepository = obsTestResultRepository;
            _obsFinalTestResultRepository = obsFinalTestResultRepository;
            _categoryRepository = categoryRepository;
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

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId,practiceId,deviceId);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {

            return _encounterRepository.LoadTestAll(encounterTypeId, clientId, true).ToList();
        }

        public void SaveTest(ObsTestResult testResult)
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
                        final = ObsFinalTestResult.CreateFirst(testResult.Result, testResult.EncounterId);
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

            UpdateFinalResult(testResult.EncounterId);
        }

        public void SaveFinalTest(ObsFinalTestResult testResult)
        {
            var test = _obsFinalTestResultRepository.Get(testResult.Id);
            if (null != test)
            {
                test.ResultGiven = testResult.ResultGiven;
                test.CoupleDiscordant = testResult.ResultGiven;
                test.SelfTestOption = testResult.SelfTestOption;
                test.Remarks = testResult.Remarks;
                _obsFinalTestResultRepository.SaveOrUpdate(test);
            }
        }

        public void DeleteTest(ObsTestResult testResult)
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

            UpdateFinalResult(testResult.EncounterId);
        }

        public void UpdateFinalResult(Guid encounterId)
        {

            LoadItems();

            var final = _obsFinalTestResultRepository.GetAll(x => x.EncounterId == encounterId)
                .FirstOrDefault();

            if (null != final)
            {
                final.ProcessEndResult(_categoryItems);
                _obsFinalTestResultRepository.SaveOrUpdate(final);
            }
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