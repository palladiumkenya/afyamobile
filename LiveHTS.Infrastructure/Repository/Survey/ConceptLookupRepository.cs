using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ConceptLookupRepository : BaseRepository<ConceptLookup>, IConceptLookupRepository
    {
        public ConceptLookupRepository()
        {
            //concept
            var lookupYesNo = new ConceptLookup { Name = "YesNo", Description = "YesNo" };
            var lookupReferral = new ConceptLookup { Name = "Referral", Description = "Referral" };

            //items
            var items = new List<ConceptLookupItem>
            {
                new ConceptLookupItem {Display = "Y",Description = "Yes",Rank = 1.0m,ConceptLookupId = lookupYesNo.Id},
                new ConceptLookupItem {Display = "N",Description = "No",Rank = 2.0m,ConceptLookupId = lookupYesNo.Id},
                new ConceptLookupItem {Display = "Family planning",Description = "Family planning",Rank = 1.0m,ConceptLookupId = lookupReferral.Id},
                new ConceptLookupItem {Display = "Spiritual support",Description = "Spiritual support",Rank = 2.0m,ConceptLookupId = lookupReferral.Id},
                new ConceptLookupItem {Display = "Legal services",Description = "Legal services",Rank = 3.0m,ConceptLookupId = lookupReferral.Id},
            };

            //concept-items
            lookupYesNo.Items.ToList().AddRange(new List<ConceptLookupItem>{items[0],items[1]});
            lookupReferral.Items.ToList().AddRange(new List<ConceptLookupItem>{items[2],items[3],items[4]});

            _entities.AddRange(new List<ConceptLookup>(){ lookupYesNo, lookupReferral });
        }
    }
}