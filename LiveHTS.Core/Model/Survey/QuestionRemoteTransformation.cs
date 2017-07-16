using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionRemoteTransformation : Entity<Guid>
    {
        [Indexed]
        public string ConditionId { get; set; }
        [Indexed]
        public string SubjectAttributeId { get; set; }
        [Indexed]
        public Guid? RemoteQuestionId { get; set; }
        [Indexed]
        public Guid? SelfQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        [Indexed]
        public string ActionId { get; set; }
        public string Content { get; set; }
        public string  AltContent { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }

        public QuestionRemoteTransformation()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{ConditionId} {SubjectAttributeId} {ActionId}";
        }
    }
}