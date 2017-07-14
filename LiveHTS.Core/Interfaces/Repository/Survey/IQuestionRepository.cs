using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Repository.Survey
{
    public interface IQuestionRepository:IRepository<Question,Guid>
    {
        IEnumerable<Question> GetWithConcepts(Guid? questionId = null,Guid ? formId=null);
        IEnumerable<Question> GetWithMetadata(Guid? questionId=null, Guid? formId = null);
    }
}