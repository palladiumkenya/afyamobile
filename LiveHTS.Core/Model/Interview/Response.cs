using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Interview
{
    public class Response
    {
        public Guid EncounterId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ObsId { get; set; }
    }
}