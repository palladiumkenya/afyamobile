using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Question:Entity<Guid>
    {
        public Guid ConceptId { get; set; }
        public string Ordinal { get; set; }
        public string Display { get; set; }
        public Decimal Rank { get; set; }
        public List<QuestionValidation> Validations { get; set; }
        public List<QuestionReValidation> ReValidations { get; set; }
        public List<QuestionBranch> Branches { get; set; }
        public List<QuestionTransformation> Transformations { get; set; }
        public List<QuestionRemoteTransformation> RemoteTransformations { get; set; }
        public Guid FormId { get; set; }
    }
}