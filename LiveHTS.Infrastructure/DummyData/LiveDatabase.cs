using LiveHTS.Core.Model.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Repository;

namespace LiveHTS.Infrastructure.DummyData
{
    public  class LiveDatabase : ILiveDatabase
    {
        private static List<Module> _modules=new List<Module>();
        private static List<Form> _forms = new List<Form>();
        private static List<Section> _sections = new List<Section>();
        private static List<Concept> _concepts = new List<Concept>();
        private static List<ConceptType> _conceptTypes=new List<ConceptType>();
        private static List<ConceptLookup> _conceptLookups=new List<ConceptLookup>();
        private static List<ConceptLookupItem> _conceptLookupItems=new List<ConceptLookupItem>();

        public LiveDatabase()
        {
            Create();
        }

        private void Create()
        {
            _sections = ReadSections();
            _concepts = ReadConcepts();
            foreach (var section in _sections)
            {
                section.AddConcepts(_concepts.Where(x => x.SectionId == section.Id).ToList());
            }
            _forms = ReadForms().ToList();
            foreach (var form  in _forms)
            {
                form.AddSections(_sections);
            }
            _modules = ReadModules();
            foreach (var module in _modules)
            {
                module.AddForms(_forms);
            }
        }
        public  List<Module> Read()
        {
            return _modules;
        }

        private  List<Module> ReadModules()
        {
            return _modules.Count > 0
                ? _modules
                : _modules = new List<Module>
                {
                    new Module {Name = "HTS Module", Description = "HTS Module for CBS"}
                };
        }
        private  List<Form> ReadForms()
        {
            return _forms.Count > 0
                ? _forms
                : _forms = new List<Form>
                {
                    new Form {Name = "HTS Form", Description = "HTS Form for CBS"}
                };
        }
        private  List<Section> ReadSections()
        {
            return _sections.Count > 0
                ? _sections
                : _sections = new List<Section>
                {
                    new Section {Name = "Section A", Description = "Section A"},
                    new Section {Name = "Section B", Description = "Section B"},
                };
        }
        private  List<Concept> ReadConcepts()
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
        public  List<ConceptType> ReadConceptTypes()
        {
           return _conceptTypes.Count > 0
               ? _conceptTypes
               : _conceptTypes = new List<ConceptType>
            {
                new ConceptType {Name = "Text", Description = "Text"},
                new ConceptType {Name = "Numeric", Description = "Numeric"},
                new ConceptType {Name = "Coded", Description = "Coded"}
            };
        }
        private  List<ConceptLookup> ReadConceptLookups()
        {
            return _conceptLookups.Count > 0
                ? _conceptLookups
                : _conceptLookups = new List<ConceptLookup>
                {
                    new ConceptLookup {Name = "YesNo", Description = "YesNo"},
                    new ConceptLookup {Name = "Referral", Description = "Referral"}
                };
        }
        public  List<ConceptLookupItem> ReadConceptLookupItems()
        {
            var lookupYesNo = ReadConceptLookups().First(x => x.Name == "YesNo");
            var lookupReferral = ReadConceptLookups().First(x => x.Name == "Referral");

           return _conceptLookupItems.Count > 0
               ? _conceptLookupItems
               : _conceptLookupItems = new List<ConceptLookupItem>
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
