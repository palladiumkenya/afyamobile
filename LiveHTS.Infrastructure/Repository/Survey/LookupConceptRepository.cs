using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class LookupConceptRepository:BaseRepository<LookupConcept>, ILookupConceptRepository
    {
        public LookupConceptRepository()
        {
            //items
            var items = new List<LookupItem>
            {
                new LookupItem {Display = "Y",Description = "Yes"},
                new LookupItem {Display = "N",Description = "No"},
                new LookupItem {Display = "Family planning",Description = "Family planning"},
                new LookupItem {Display = "Spiritual support",Description = "Spiritual support"},
                new LookupItem {Display = "Legal services",Description = "Legal services"},
            };

            //concept
            var lookupConceptYesNo = new LookupConcept {Name = "YN", Description = "YesNo"};
            var lookupConceptReferral = new LookupConcept { Name = "RF", Description = "Referral" };

            //concept-items
            lookupConceptYesNo.ConceptItems.ToList().AddRange(new List<LookupConceptItem>
            {
                new LookupConceptItem(){LookupConceptId = lookupConceptYesNo.Id,LookupItemId = items[0].Id},
                new LookupConceptItem(){LookupConceptId = lookupConceptYesNo.Id,LookupItemId = items[1].Id},            
            });

            lookupConceptReferral.ConceptItems.ToList().AddRange(new List<LookupConceptItem>
            {
                new LookupConceptItem(){LookupConceptId = lookupConceptReferral.Id,LookupItemId = items[0].Id},
                new LookupConceptItem(){LookupConceptId = lookupConceptReferral.Id,LookupItemId = items[1].Id},
                new LookupConceptItem(){LookupConceptId = lookupConceptReferral.Id,LookupItemId = items[2].Id},
            });

            _entities.AddRange(new List<LookupConcept>(){lookupConceptYesNo,lookupConceptReferral});
        }
    }
}