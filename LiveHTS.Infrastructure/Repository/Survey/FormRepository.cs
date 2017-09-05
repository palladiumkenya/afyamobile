using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;


namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class FormRepository : BaseRepository<Form, Guid>, IFormRepository
    {
        private readonly IQuestionRepository _questionRepository;

        public FormRepository(ILiveSetting liveSetting, IQuestionRepository questionRepository) : base(liveSetting)
        {
            _questionRepository = questionRepository;
        }
        
        public Form GetWithQuestions(Guid formId, bool includeMetadata = false)
        {
            var form= GetAll(x => x.Id == formId).FirstOrDefault();

            if (null != form)
            {
                var questions = new List<Question>();

                if (includeMetadata)
                {
                    questions = _questionRepository
                        .GetWithMetadata(null, form.Id)
                        .OrderBy(x => x.Rank)
                        .ToList();
                }
                else
                {
                    questions = _questionRepository
                        .GetWithConcepts(null, form.Id)
                        .OrderBy(x => x.Rank)
                        .ToList();
                }
                
                form.Questions = questions;
            }
            return form;
        }
    }
}