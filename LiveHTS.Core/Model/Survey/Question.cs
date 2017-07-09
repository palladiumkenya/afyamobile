using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Question:Entity<Guid>
    {
        [Indexed]
        public Guid ConceptId { get; set; }
        public string Ordinal { get; set; }
        public string Display { get; set; }
        public Decimal Rank { get; set; }
        [Ignore]
        public List<QuestionValidation> Validations { get; set; }
        [Ignore]
        public List<QuestionReValidation> ReValidations { get; set; }
        [Ignore]
        public List<QuestionBranch> Branches { get; set; }
        [Ignore]
        public List<QuestionTransformation> Transformations { get; set; }
        [Ignore]
        public List<QuestionRemoteTransformation> RemoteTransformations { get; set; }
        [Indexed]
        public Guid FormId { get; set; }
    }
}