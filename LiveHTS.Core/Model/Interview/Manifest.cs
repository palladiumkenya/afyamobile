using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Model.Interview
{
    public class Manifest
    {
        public List<Question> QuestionStore { get; set; }=new List<Question>();
        public List<LiveSession> SessionStore { get; set; }=new List<LiveSession>();

        private Manifest(List<Question> questionStore, List<LiveSession> sessionStore)
        {
            QuestionStore = questionStore;
            SessionStore = sessionStore;
        }


        public static Manifest Create(Form form, Encounter encounter)
        {
            var obs = encounter.Obses.Select(x => new LiveSession
            {

                EncounterId = x.EncounterId,
                ObsId = x.Id,
                QuestionId = x.QuestionId
            }).ToList();
            
            return new Manifest(form.Questions,obs);
        }

        public override string ToString()
        {
            return $"Qs:{QuestionStore.Count}| Completed:{SessionStore.Count}";
        }
    }
}