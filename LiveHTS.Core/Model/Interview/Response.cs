using System;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Model.Interview
{
    public class Response
    {
        public Guid EncounterId { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid ObsId { get; set; }
        public Obs Obs { get; set; }

        public Response()
        {
        }

        public Response(Guid encounterId)
        {
            EncounterId = encounterId;
        }

        public Response(Guid encounterId, Question question, Obs obs)
        {
            EncounterId = encounterId;
            SetQuestion(question);
            SetObs(obs);
        }

        public ObsValue GetValue()
        {
            //  Single | Numeric | Multi | DateTime | Text

            if (Question.Concept.ConceptTypeId == "Single")
                return new ObsValue(typeof(Guid?), Obs.ValueCoded);
            if (Question.Concept.ConceptTypeId == "Numeric")
                return new ObsValue(typeof(decimal?), Obs.ValueNumeric);
            if (Question.Concept.ConceptTypeId == "Multi")
                return new ObsValue(typeof(Guid?[]), Obs.ValueMultiCoded);
            if (Question.Concept.ConceptTypeId == "DateTime")
                return new ObsValue(typeof(DateTime?), Obs.ValueDateTime);

            return new ObsValue(typeof(string), Obs.ValueText);
        }

        public void SetQuestion(Question question)
        {
            if (null != question)
            {
                QuestionId = question.Id;
                Question = question;
            }
        }

        public void SetObs(Obs obs)
        {
            if (null != obs)
            {
                ObsId = obs.Id;
                Obs = obs;
            }
        }

        public void SetObs(Guid encounterId, Guid questionId, string type, object response)
        {
            SetObs(Obs.Create(questionId,encounterId,type,response));
        }
    }
}