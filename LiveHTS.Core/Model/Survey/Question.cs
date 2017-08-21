using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Description { get; set; }
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
            get { return null != Validations && Validations.Any(x=>x.Revision==0); }
        }
        [Ignore]
        public bool IsRequired
        {
            get { return HasValidations && Validations.Any(x => x.ValidatorId == "Required"); }
        }
        [Ignore]
        public bool HasReValidations
        {
            get { return null != ReValidations && ReValidations.Any(); }
        }
        [Ignore]
        public bool HasBranches
        {
            get { return null != Branches && Branches.Any(); }
        }
        [Ignore]
        public bool HasTransformations
        {
            get { return null != Transformations && Transformations.Any(); }
        }
        [Ignore]
        public bool HasRemoteTransformations
        {
            get { return null != RemoteTransformations && RemoteTransformations.Any(); }
        }
        [Ignore]
        public List<Guid> SkippedQuestionIds { get; set; } =new List<Guid>();


        public Question()
        {
            Id = LiveGuid.NewGuid();
        }

        public bool HasConditionalBranches(string condition)
        {
            return null != Branches && Branches.Any(x => x.ConditionId.ToLower() == condition.ToLower());
        }

        public override string ToString()
        {
            return $"{Ordinal}. {Display}";
        }
    }
}