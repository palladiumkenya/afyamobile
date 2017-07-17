using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IInterviewService
    {
        IEnumerable<Question> Questions { get; set; }
        IEnumerable<Question> Manifest { get; set; }
    }
}