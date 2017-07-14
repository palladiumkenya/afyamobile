using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;


namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ConceptRepository : BaseRepository<Concept, Guid>, IConceptRepository
    {
        private readonly ICategoryRepository _categoryRepository;

        public ConceptRepository(ILiveSetting liveSetting, ICategoryRepository categoryRepository) : base(liveSetting)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Concept> GetWithLookups()
        {
            //cat
            var concepts = _db.Table<Concept>().ToList();

            foreach (var concept in concepts)
            {
                try
                {
                    if (!concept.CategoryId.IsNullOrEmpty())
                    {
                        var category = _categoryRepository.GetAllWithItems(concept.CategoryId).FirstOrDefault();
                        concept.Category = category;
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return concepts;
        }
    }
}