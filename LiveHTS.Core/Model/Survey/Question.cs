using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Question : Entity<Guid>
    {
        [Indexed]
        public Guid ConceptId { get; set; }

        [Ignore]
        public Concept Concept { get; set; }

        public string Ordinal { get; set; }
        public string Display { get; set; }
        public Decimal Rank { get; set; }

        [Ignore]
        public List<QuestionValidation> Validations { get; set; } = new List<QuestionValidation>();

        [Ignore]
        public List<QuestionReValidation> ReValidations { get; set; } = new List<QuestionReValidation>();

        [Ignore]
        public List<QuestionBranch> Branches { get; set; } = new List<QuestionBranch>();

        [Ignore]
        public List<QuestionTransformation> Transformations { get; set; } = new List<QuestionTransformation>();

        [Ignore]
        public List<QuestionRemoteTransformation> RemoteTransformations { get; set; } =
            new List<QuestionRemoteTransformation>();

        [Indexed]
        public Guid FormId { get; set; }

        [Ignore]
        public bool HasValidations
        {
            get { return null != Validations && Validations.Count > 0; }
        }

        public bool HasReValidations
        {
            get { return null != ReValidations && ReValidations.Count > 0; }
        }

        public bool HasBranches
        {
            get { return null != Branches && Branches.Count > 0; }
        }

        public bool HasTransform
        {
            get { return null != Transformations && Transformations.Count > 0; }
        }

        public bool HasRemoteTreans
        {
            get { return null != RemoteTransformations && RemoteTransformations.Count > 0; }
        }

        public Question()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Ordinal}. {Display}";
        }
    }
}