using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;

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


        public void ClearObs(Guid id)
        {
            _db.Execute("DELETE FROM Obs WHERE EncounterId=?", id.ToString());
        }

        public void UpdateStatus(Guid id, bool completed)
        {
            var encounter = Get(id);
            if (null != encounter)
                encounter.IsComplete = completed;

            Update(encounter);
        }
    }
}