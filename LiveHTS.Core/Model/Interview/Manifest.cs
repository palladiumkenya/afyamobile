using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.Interview
{
    public class Manifest
    {
        private Guid _formId;
        private Guid _encounterTypeId;
        private Guid _clientId;
        private readonly Guid _practiceId;

        public Guid FormId
        {
            get { return _formId; }
        }

        public Guid EncounterTypeId
        {
            get { return _encounterTypeId; }
        }

        public Guid ClientId
        {
            get { return _clientId; }
        }

        public Guid PracticeId
        {
            get { return _practiceId; }
        }

        public List<Question> QuestionStore { get; set; } = new List<Question>();
        public List<AnsweredQuestion> AnsweredQuestionStore { get; set; } = new List<AnsweredQuestion>();

        public bool HasQuestionStore()
        {
            return null != QuestionStore && QuestionStore.Count > 0;
        }

        public bool HasAnsweredQuestionStore()
        {
            return null != AnsweredQuestionStore && AnsweredQuestionStore.Count > 0;
        }

        public Manifest()
        {
        }

        private Manifest(List<Question> questionStore, List<AnsweredQuestion> answeredQuestionStore,  Guid formId, Guid encounterTypeId, Guid clientId, Guid practiceId)
        {
            QuestionStore = questionStore;
            AnsweredQuestionStore = answeredQuestionStore;
            _formId = formId;
            _encounterTypeId = encounterTypeId;
            _clientId = clientId;
            _practiceId = practiceId;
        }

        public static Manifest Create(Form form, Encounter encounter, Guid formId, Guid encounterTypeId, Guid clientId, Guid practiceId)
        {
            var formWithQuestions = form ?? new Form();
            var sessions = Generate(encounter);
            return new Manifest(formWithQuestions.Questions, sessions, formId, encounterTypeId, clientId,practiceId);
        }
        public  void UpdateEncounter(Encounter encounter)
        {
            AnsweredQuestionStore = Generate(encounter);
        }

        private static List<AnsweredQuestion> Generate(Encounter encounter)
        {
            var sessions = new List<AnsweredQuestion>();

            if (null != encounter)
            {
                sessions = encounter.Obses.Select(x => new AnsweredQuestion
                    {
                        EncounterId = x.EncounterId,
                        ObsId = x.Id,
                        QuestionId = x.QuestionId
                    })
                    .ToList();
            }

            return sessions;
        }

        public Encounter GetEncounter()
        {
            if (HasAnsweredQuestionStore())
            {
                var encounterId = AnsweredQuestionStore.Select(x => x.EncounterId).FirstOrDefault();

                if (encounterId.IsNullOrEmpty())
                    return null;
                return new Encounter() {Id = encounterId};
            }

            return null;

        }

        public bool IsStarted()
        {
            return null != GetEncounter();
        }

        public override string ToString()
        {
            return $"Form:{FormId},EncounterType:{EncounterTypeId},Client:{ClientId},Practice:{PracticeId}, Qs:{QuestionStore.Count}| Completed:{AnsweredQuestionStore.Count}";
        }

    }
}