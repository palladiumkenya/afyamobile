using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class QuestionRepository:BaseRepository<Question,Guid>,IQuestionRepository
    {
        private readonly IConceptRepository _conceptRepository;

        public QuestionRepository(ILiveSetting liveSetting, IConceptRepository conceptRepository) : base(liveSetting)
        {
            _conceptRepository = conceptRepository;
        }

        public IEnumerable<Question> GetWithConcepts(Guid? formId = null)
        {
            var questions = new List<Question>();


            if (formId.IsNullOrEmpty())
            {
                questions = _db.Table<Question>().ToList();
            }
            else
            {
                questions = new List<Question> { Get(formId.Value) };
            }

            foreach (var question in questions)
            {
                try
                {
                    var concept = _conceptRepository.GetWithLookups(question.ConceptId).FirstOrDefault();
                    question.Concept = concept;
                }
                catch
                {
                    // ignored
                }
            }
            return questions;
        }
    }
}