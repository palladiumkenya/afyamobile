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
        
        public Form GetWithQuestions(Guid moduleId, Guid formId)
        {
            var form= GetAll(x => x.ModuleId == moduleId && x.Id == formId).FirstOrDefault();

            if (null != form)
            {
                var questions = _questionRepository
                    .GetWithConcepts(null, form.Id)
                    .OrderBy(x => x.Rank)
                    .ToList();
                form.Questions = questions;
            }
            return form;
        }
    }
}