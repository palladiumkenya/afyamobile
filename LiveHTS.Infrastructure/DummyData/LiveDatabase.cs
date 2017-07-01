using LiveHTS.Core.Model.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHTS.Infrastructure.DummyData
{
    public static class LiveDatabase
    {
        public static IEnumerable<Module> Read()
        {
            var modules = ReadModules();
            var forms = ReadForms();
            var sections = ReadSections();
            var concepts = ReadConcepts();

            foreach (var s in sections)
            {
                s.Concepts.ToList().AddRange(concepts.Where(x => x.SectionId == s.Id));
            }

            foreach (var f in forms)
            {
                f.Sections.ToList().AddRange(sections.Where(x => x.FormId == f.Id));
            }

            foreach (var m in modules)
            {
                m.Forms.ToList().AddRange(forms.Where(x => x.ModuleId == m.Id));
            }
            return modules;
        }
        private static IEnumerable<Module> ReadModules()
        {
            return new List<Module>
            {
                new Module { Name = "HTS", Description = "HTS" }
            };
        }
        private static IEnumerable<Form> ReadForms()
        {
            var module = ReadModules().First();
            return new List<Form>
            {
                new Form { Name = "HTS Form", Description = "HTS Form", ModuleId = module.Id }
            };
        }
        private static IEnumerable<Section> ReadSections()
        {
            var form = ReadForms().First();
            return new List<Section>
            {
                new Section {Name = "Section A", Description = "Section A", FormId = form.Id},
                new Section {Name = "Section B", Description = "Section B", FormId = form.Id},
            };
        }
        private static IEnumerable<Concept> ReadConcepts()
        {
            var lookups = ReadConceptLookups().ToList();
            var conceptTypes = ReadConceptTypes().ToList();
            var sections = ReadSections().ToList();

            return new List<Concept>
            {
                 new Concept
                {
                    Display = "Staff ?",Description = "Staff ?",Rank = 1.0m,SectionId = sections[0].Id,
                    ConceptTypeId = conceptTypes[2].Id,LookupConceptId = lookups[0].Id
                },
                new Concept
                {
                    Display = "No of Partners ?",Description = "No of Partners ?",Rank = 2.0m,SectionId = sections[0].Id,
                    ConceptTypeId = conceptTypes[1].Id
                },
                new Concept
                {
                    Display = "Referral Services ?",Description = "Referral Services ?",Rank = 1.0m,SectionId = sections[1].Id,
                    ConceptTypeId = conceptTypes[2].Id,LookupConceptId = lookups[1].Id
                },
                new Concept
                {
                    Display = "Comments ?",Description = "Comments ?",Rank = 2.0m,SectionId = sections[1].Id,
                    ConceptTypeId = conceptTypes[0].Id
                }
            };
        }
        public static IEnumerable<ConceptType> ReadConceptTypes()
        {
            return new List<ConceptType>
            {
                new ConceptType {Name = "Text", Description = "Text"},
                new ConceptType {Name = "Numeric", Description = "Numeric"},
                new ConceptType {Name = "Coded", Description = "Coded"}
            };
        }
        private static IEnumerable<ConceptLookup> ReadConceptLookups()
        {
            return new List<ConceptLookup>
            {
                  new ConceptLookup { Name = "YesNo", Description = "YesNo" },
            new ConceptLookup { Name = "Referral", Description = "Referral" }
            };
        }
        public static IEnumerable<ConceptLookupItem> ReadConceptLookupItems()
        {
            var lookupYesNo = ReadConceptLookups().First(x => x.Name == "YesNo");
            var lookupReferral = ReadConceptLookups().First(x => x.Name == "Referral");

            return new List<ConceptLookupItem>
            {
              new ConceptLookupItem {Display = "Y",Description = "Yes",Rank = 1.0m,ConceptLookupId = lookupYesNo.Id},
                new ConceptLookupItem {Display = "N",Description = "No",Rank = 2.0m,ConceptLookupId = lookupYesNo.Id},
                new ConceptLookupItem {Display = "Family planning",Description = "Family planning",Rank = 1.0m,ConceptLookupId = lookupReferral.Id},
                new ConceptLookupItem {Display = "Spiritual support",Description = "Spiritual support",Rank = 2.0m,ConceptLookupId = lookupReferral.Id},
                new ConceptLookupItem {Display = "Legal services",Description = "Legal services",Rank = 3.0m,ConceptLookupId = lookupReferral.Id},
            };
        }
    }
}
