using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class FormRepository:BaseRepository<Form>,IFormRepository
    {
        private readonly Module _module;

        public FormRepository()
        {
            //module
            _module = new Module {Name = "HTS", Description = "HTS"};

            //form
            var form = new Form { Name = "HTS Form", Description = "HTS Form", ModuleId = _module.Id };

            //sections
            var sections = new List<Section>
            {
                new Section {Name = "Section A", Description = "Section A", FormId = form.Id},
                new Section {Name = "Section B", Description = "Section B", FormId = form.Id},
            };

            /*
                new ConceptType {Name = "Text", Description = "Text"},
                new ConceptType {Name = "Numeric", Description = "Numeric"},
                new ConceptType {Name = "Coded", Description = "Coded"}

                new ConceptLookup { Name = "YesNo", Description = "YesNo" };
                new ConceptLookup { Name = "Referral", Description = "Referral" };
             */
            var conceptTypes = new ConceptTypeRepository().GetAll().ToList();
            var lookups = new ConceptLookupRepository().GetAll().ToList();

            //concepts
            var conceptsA = new List<Concept>
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
                }
            };          

            var conceptsB = new List<Concept>
            {
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

            sections[0].Concepts.ToList().AddRange(conceptsA);
            sections[1].Concepts.ToList().AddRange(conceptsB);
            form.Sections.ToList().AddRange(sections);

            var form2 = new Form { Name = "HTS Form II", Description = "HTS Form II", ModuleId = _module.Id };
            _entities.AddRange(new List<Form>() { form ,form2});
        }

        public Module GetModule()
        {
            return _module;
        }
    }
}