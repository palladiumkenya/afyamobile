using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class EncounterRepository : BaseRepository<Encounter, Guid>, IEncounterRepository
    {
        public EncounterRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public Encounter Load(Guid id, bool includeObs = false)
        {
            var encounter = Get(id);

            if (includeObs)
            {
                if (null != encounter)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == encounter.Id)
                        .ToList();
                    encounter.Obses = obses;
                }
            }

            return encounter;
        }

        public Encounter Load(Guid formId, Guid encounterTypeId, Guid clientId, bool includeObs = false)
        {
            var encounter= GetAll(x => x.FormId == formId &&
                               x.EncounterTypeId == encounterTypeId &&
                               x.ClientId == clientId)
                .FirstOrDefault();

            if (includeObs)
            {
                if (null != encounter)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == encounter.Id)
                        .ToList();
                    encounter.Obses = obses;
                }
            }

            return encounter;
        }

        public IEnumerable<Encounter> LoadAll(Guid clientId)
        {
            var encounters = GetAll(x => x.ClientId == clientId)
                .ToList();

            foreach (var e in encounters)
            {
                if (null != e)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.Obses = obses;

                    var obsTestResults = _db.Table<ObsTestResult>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsTestResults = obsTestResults;

                    var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsFinalTestResults = obsFinalTestResults;

                    var obsTraceResults = _db.Table<ObsTraceResult>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsTraceResults = obsTraceResults;

                    var obsLinkages = _db.Table<ObsLinkage>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsLinkages = obsLinkages;


                    var obsMemberScreenings = _db.Table<ObsMemberScreening>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsMemberScreenings = obsMemberScreenings;

                    var obsFamilyTraceResults = _db.Table<ObsFamilyTraceResult>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsFamilyTraceResults = obsFamilyTraceResults;

                    var obsPartnerScreenings = _db.Table<ObsPartnerScreening>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsPartnerScreenings = obsPartnerScreenings;

                    var obsPartnerTraceResults = _db.Table<ObsPartnerTraceResult>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsPartnerTraceResults = obsPartnerTraceResults;

                    e.EncounterType = _db.Table<EncounterType>().FirstOrDefault(x => x.Id == e.EncounterTypeId);

                }

            }
            return encounters;
        }

        public IEnumerable<Encounter> LoadAllKey(Guid clientId)
        {
            var encounters = GetAll(x => x.ClientId == clientId)
                .ToList();

            foreach (var e in encounters)
            {
                if (null != e)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();

                    e.Obses = obses;

                    var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsFinalTestResults = obsFinalTestResults;

                    var obsMemberScreenings = _db.Table<ObsMemberScreening>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsMemberScreenings = obsMemberScreenings;



                    var obsPartnerScreenings = _db.Table<ObsPartnerScreening>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.ObsPartnerScreenings = obsPartnerScreenings;
                }
            }
            return encounters;
        }

        public IEnumerable<Encounter> LoadAll(Guid formId, Guid clientId, bool includeObs = false)
        {
            var encounters = GetAll(x => x.FormId == formId &&
                               x.ClientId == clientId)
                .ToList();

            if (includeObs)
            {
                foreach (var e in encounters)
                {
                    if (null != e)
                    {
                        var obses = _db.Table<Obs>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.Obses = obses;

                        var obsTestResults = _db.Table<ObsTestResult>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsTestResults = obsTestResults;

                        var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsFinalTestResults = obsFinalTestResults;

                        var obsTraceResults = _db.Table<ObsTraceResult>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsTraceResults = obsTraceResults;

                        var obsLinkages = _db.Table<ObsLinkage>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsLinkages = obsLinkages;



                    }
                }
            }
            return encounters;
        }

        public IEnumerable<Encounter> LoadAll(Guid formId, Guid clientId, Guid indexClientId, bool includeObs = false)
        {
            var encounters = GetAll(x => x.FormId == formId &&
                                         x.ClientId == clientId&&
                                         x.IndexClientId==indexClientId)
                .ToList();

            if (includeObs)
            {
                foreach (var e in encounters)
                {
                    if (null != e)
                    {
                        var obses = _db.Table<Obs>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.Obses = obses;

                        var obsTestResults = _db.Table<ObsTestResult>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsTestResults = obsTestResults;

                        var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsFinalTestResults = obsFinalTestResults;

                        var obsTraceResults = _db.Table<ObsTraceResult>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsTraceResults = obsTraceResults;

                        var obsLinkages = _db.Table<ObsLinkage>()
                            .Where(x => x.EncounterId == e.Id)
                            .ToList();
                        e.ObsLinkages = obsLinkages;



                    }
                }
            }
            return encounters;
        }


        public Encounter LoadTest(Guid id, bool includeObs = false)
        {
            var encounter = Get(id);

            if (null != encounter && includeObs)
            {
                var obsTestResults = _db.Table<ObsTestResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsTestResults = obsTestResults;
                var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsFinalTestResults = obsFinalTestResults;

                var obsTraceResults = _db.Table<ObsTraceResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsTraceResults = obsTraceResults;

                var obsLinkages = _db.Table<ObsLinkage>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsLinkages = obsLinkages;

                var obsMemberScreenings = _db.Table<ObsMemberScreening>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsMemberScreenings = obsMemberScreenings;

                var obsFamilyTraceResults = _db.Table<ObsFamilyTraceResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsFamilyTraceResults = obsFamilyTraceResults;

                var obsPartnerScreenings = _db.Table<ObsPartnerScreening>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsPartnerScreenings = obsPartnerScreenings;

                var obsPartnerTraceResults = _db.Table<ObsPartnerTraceResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsPartnerTraceResults = obsPartnerTraceResults;
            }

            return encounter;
        }

        public Encounter LoadFinalTest(Guid id, bool includeObs = false)
        {
            var encounter = Get(id);

            if (null != encounter && includeObs)
            {
                var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsFinalTestResults = obsFinalTestResults;
            }

            return encounter;
        }

        public Encounter LoadTest(Guid encounterTypeId, Guid clientId, bool includeObs = false)
        {
            var encounter = GetAll(x => x.EncounterTypeId == encounterTypeId && x.ClientId == clientId)
                .FirstOrDefault();

            if (null != encounter && includeObs)
            {
                var obsTestResults = _db.Table<ObsTestResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsTestResults = obsTestResults;
                var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsFinalTestResults = obsFinalTestResults;

                var obsTraceResults = _db.Table<ObsTraceResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsTraceResults = obsTraceResults;

                var obsLinkages = _db.Table<ObsLinkage>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsLinkages = obsLinkages;

                var obsFamilyTraceResults = _db.Table<ObsFamilyTraceResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsFamilyTraceResults = obsFamilyTraceResults;

                var obsPartnerTraceResults = _db.Table<ObsPartnerTraceResult>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.ObsPartnerTraceResults = obsPartnerTraceResults;
            }



            return encounter;
        }

        public List<Encounter> LoadTestAll(Guid encounterTypeId, Guid clientId, bool includeObs = false)
        {
            var encounters = GetAll(x => x.EncounterTypeId == encounterTypeId && x.ClientId == clientId).ToList();

            foreach (var encounter in encounters)
            {
                if (null != encounter && includeObs)
                {
                    var obsTestResults = _db.Table<ObsTestResult>()
                        .Where(x => x.EncounterId == encounter.Id)
                        .ToList();
                    encounter.ObsTestResults = obsTestResults;
                    var obsFinalTestResults = _db.Table<ObsFinalTestResult>()
                        .Where(x => x.EncounterId == encounter.Id)
                        .ToList();
                    encounter.ObsFinalTestResults = obsFinalTestResults;
                }
            }
            return encounters;
        }

        public DateTime GetPretestEncounterDate(Guid clientId)
        {
            var encounterTypeId = new Guid("7e5164a6-6b99-11e7-907b-a6006ad3dba0");
            var encounter = GetAll(x => x.EncounterTypeId == encounterTypeId && x.ClientId == clientId)
                .FirstOrDefault();

            if (null != encounter)
                return encounter.EncounterDate;

            return new DateTime(1900,1,1);
        }

        public bool CheckPretestComplete(Guid clientId,bool downloaded=false)
        {
            var encounterTypeId = new Guid("7e5164a6-6b99-11e7-907b-a6006ad3dba0");

            var encounter = GetAll(x => x.EncounterTypeId == encounterTypeId && x.ClientId == clientId)
                .FirstOrDefault();

            if (downloaded)
            {
                if (null != encounter)
                    return encounter.IsComplete;
                return true;
            }
            else
            {
                return null != encounter && encounter.IsComplete;
            }
        }

        public bool CheckEncountersExisit(Guid clientId, Guid encounterTypeId)
        {
            var encounter = GetAll(x => x.EncounterTypeId == encounterTypeId && x.ClientId == clientId)
                .FirstOrDefault();

            return null != encounter;
        }

        public bool GetIndividual(Guid clientId)
        {
            var encounterTypeId = new Guid("7e5164a6-6b99-11e7-907b-a6006ad3dba0");
            var encounter = GetAll(x => x.EncounterTypeId == encounterTypeId && x.ClientId == clientId)
                .FirstOrDefault();

            if (null != encounter)
            {
                var obses = _db.Table<Obs>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.Obses = obses;
                if (encounter.HasObs)
                {
                    var testedAsObs= encounter.Obses.FirstOrDefault(x => x.QuestionId == new Guid("b260401e-852f-11e7-bb31-be2e44b06b34"));
                    if (null != testedAsObs)
                    {
                        return null != testedAsObs.ValueCoded && testedAsObs.ValueCoded.Value == new Guid("B25EDE36-852F-11E7-BB31-BE2E44B06B34");
                    }
                }
            }

            return false;
        }


        public void ClearObs(Guid id)
        {
            _db.Execute("DELETE FROM Obs WHERE EncounterId=?", id.ToString());
        }

        public void UpdateStatus(Guid id, bool completed)
        {
            var encounter = Get(id);
            if (null != encounter)
            {
                encounter.IsComplete = completed;
            }

            Update(encounter);
        }

        public void UpdateStatus(Guid id, Guid userId, bool completed)
        {
            var encounter = Get(id);
            if (null != encounter)
            {
                encounter.UserId = userId;
                encounter.IsComplete = completed;
            }

            Update(encounter);
        }

        public void UpdateEncounterDate(Guid id, DateTime encounterDate, VisitType visitType)
        {
            var encounter = Get(id);
            if (null != encounter)
            {
                encounter.EncounterDate = encounterDate;
                encounter.VisitType = visitType;
            }
            Update(encounter);
        }

        public void UpdateEncounterDate(Guid id, DateTime encounterDate)
        {
            var encounter = Get(id);
            if (null != encounter)
            {
                encounter.EncounterDate = encounterDate;
            }
            Update(encounter);
        }

        public void Upload(Encounter encounter)
        {
            Purge(encounter.Id);
            InsertOrUpdate(encounter);

            if(encounter.Obses.Any())
                _db.InsertAll(encounter.Obses);
            if (encounter.ObsLinkages.Any())
                _db.InsertAll(encounter.ObsLinkages);
            if (encounter.ObsTraceResults.Any())
                _db.InsertAll(encounter.ObsTraceResults);
            if (encounter.ObsTestResults.Any())
                _db.InsertAll(encounter.ObsTestResults);
            if (encounter.ObsFinalTestResults.Any())
                _db.InsertAll(encounter.ObsFinalTestResults);
            if (encounter.ObsMemberScreenings.Any())
                _db.InsertAll(encounter.ObsMemberScreenings);
            if (encounter.ObsFamilyTraceResults.Any())
                _db.InsertAll(encounter.ObsFamilyTraceResults);
            if (encounter.ObsPartnerScreenings.Any())
                _db.InsertAll(encounter.ObsPartnerScreenings);
            if (encounter.ObsPartnerTraceResults.Any())
                _db.InsertAll(encounter.ObsPartnerTraceResults);
        }

        public void Purge(Guid id, string obsName="")
        {

            _db.Execute($"DELETE FROM {nameof(Obs)} WHERE EncounterId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ObsTestResult)} WHERE EncounterId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ObsFinalTestResult)} WHERE EncounterId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ObsLinkage)} WHERE EncounterId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ObsTraceResult)} WHERE EncounterId=?", id.ToString());

            _db.Execute($"DELETE FROM {nameof(ObsFamilyTraceResult)} WHERE EncounterId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ObsMemberScreening)} WHERE EncounterId=?", id.ToString());

            _db.Execute($"DELETE FROM {nameof(ObsPartnerTraceResult)} WHERE EncounterId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ObsPartnerScreening)} WHERE EncounterId=?", id.ToString());

            _db.Execute($"DELETE FROM {nameof(ClientState)} WHERE EncounterId=?", id.ToString());


        }

        public void PurgeAny(Guid id)
        {
            var ids = _db.Table<Encounter>().Where(x => x.ClientId == id).Select(x => x.Id).ToList();
            foreach (var guid in ids)
            {
                _db.Execute($"DELETE FROM {nameof(Obs)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsPartnerTraceResult)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsPartnerScreening)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsTraceResult)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsLinkage)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsTestResult)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsFamilyTraceResult)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsMemberScreening)} WHERE EncounterId=?", guid.ToString());
                _db.Execute($"DELETE FROM {nameof(ObsFinalTestResult)} WHERE EncounterId=?", guid.ToString());
                Delete(guid);
            }

        }
    }
}
