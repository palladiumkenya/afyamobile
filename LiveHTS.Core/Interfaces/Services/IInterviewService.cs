using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IInterviewService
    {
        void StartOrResume();
        void MoveNext();
        void MovePrevious();
        void Stop();
        void Discard();
    }
}