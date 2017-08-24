using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Service.Interview
{
    public class HIVTestingService:IHIVTestingService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsTestResultRepository _obsTestResultRepository;
        private readonly IObsFinalTestResultRepository _obsFinalTestResultRepository;

        public HIVTestingService(IEncounterRepository encounterRepository, IObsTestResultRepository obsTestResultRepository, IObsFinalTestResultRepository obsFinalTestResultRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTestResultRepository = obsTestResultRepository;
            _obsFinalTestResultRepository = obsFinalTestResultRepository;
        }
        public Encounter OpenEncounter(Guid encounterId)
        {
            var exisitngEncounter = _encounterRepository.LoadTest(encounterId, true);
            return exisitngEncounter;
        }
        public Encounter StartEncounter(Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId)
        {
            var exisitngEncounter = _encounterRepository
                .GetAll(x => x.EncounterTypeId == encounterTypeId &&
                             x.ClientId == clientId)
                .FirstOrDefault();

            if (null != exisitngEncounter)
            {
                return OpenEncounter(exisitngEncounter.Id);
            }

            var encounter = Encounter.CreateNew(Guid.Empty, encounterTypeId, clientId, providerId, userId);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }       
        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {
            
            return _encounterRepository.LoadTestAll(encounterTypeId, clientId,true).ToList();
        }

        public void SaveTest(ObsTestResult testResult)
        {
            _obsTestResultRepository.SaveOrUpdate(testResult);

            if (testResult.TestName.Contains("1"))
            {
                var final = _obsFinalTestResultRepository.GetAll(x => x.EncounterId == testResult.EncounterId)
                    .FirstOrDefault();

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
        }
        public void DeleteTest(ObsTestResult testResult)
        {
            _obsTestResultRepository.Delete(testResult.Id);
        }
    }
}