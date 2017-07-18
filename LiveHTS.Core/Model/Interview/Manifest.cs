using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Model.Interview
{
    public class Manifest
    {
        public List<Question> QuestionStore { get; set; }
        public List<Session> SessionStore { get; set; }
    }
}