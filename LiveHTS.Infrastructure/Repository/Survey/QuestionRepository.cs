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

        private IEnumerable<Question> GetQuestions(Guid? questionId = null, Guid? formId = null)
        {
            var questions = new List<Question>();
            if (formId.IsNullOrEmpty())
            {
                if (questionId.IsNullOrEmpty())
                {
                    questions = _db.Table<Question>().ToList();
                }
                else
                {
                    questions = GetAll(x => x.Id == questionId.Value).ToList();
                }
            }
            else
            {
                if (questionId.IsNullOrEmpty())
                {
                    questions = GetAll(x => x.FormId == formId.Value).ToList();
                }
                else
                {
                    questions = GetAll(
                            x => x.FormId == formId.Value &&
                                 x.Id == questionId.Value)
                        .ToList();
                }
            }
            return questions;
        }


        public IEnumerable<Question> GetWithConcepts(Guid? questionId = null,Guid ? formId = null)
        {
            var questions = GetQuestions(questionId, formId);

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

        public IEnumerable<Question> GetWithMetadata(Guid? questionId = null, Guid? formId = null)
        {
            var questions = GetWithConcepts(questionId, formId).ToList();

            foreach (var question in questions)
            {
                try
                {
                    var validations = _db.Table<QuestionValidation>().Where(x => x.QuestionId == question.Id).ToList();
                    question.Validations = validations;
                    
                    var reValidations = _db.Table<QuestionReValidation>().Where(x => x.QuestionId == question.Id).ToList();
                    question.ReValidations = reValidations;

                    var branches = _db.Table<QuestionBranch>().Where(x => x.QuestionId == question.Id).ToList();
                    question.Branches = branches;

                    var transformations = _db.Table<QuestionTransformation>().Where(x => x.QuestionId == question.Id).ToList();
                    question.Transformations = transformations;

                    var remoteTransformations = _db.Table<QuestionRemoteTransformation>().Where(x => x.QuestionId == question.Id).ToList();
                    question.RemoteTransformations = remoteTransformations;
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