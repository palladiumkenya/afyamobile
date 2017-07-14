using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionRemoteTransformation : Entity<Guid>
    {
        [Indexed]
        public int ConditionId { get; set; }
        [Indexed]
        public int SubjectAttributeId { get; set; }
        [Indexed]
        public Guid? RemoteQuestionId { get; set; }
        [Indexed]
        public Guid? SelfQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        [Indexed]
        public int ActionId { get; set; }
        public int Content { get; set; }
        public int  AltContent { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }

        public QuestionRemoteTransformation()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}