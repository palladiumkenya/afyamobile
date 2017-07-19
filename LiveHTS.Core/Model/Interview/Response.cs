using System;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Interview
{
    public class Response
    {
        public Guid EncounterId { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid ObsId { get; set; }
        public Obs Obs { get; set; }

        public ObsValue GetValue()
        {
            //  Single | Numeric | Multi | DateTime | Text

            if (Question.Concept.ConceptTypeId == "Single")
                return new ObsValue(Obs.ValueCoded);
            if (Question.Concept.ConceptTypeId == "Numeric")
                return new ObsValue(Obs.ValueNumeric);
            if (Question.Concept.ConceptTypeId == "Multi")
                return new ObsValue(Obs.ValueMultiCoded);
            if (Question.Concept.ConceptTypeId == "DateTime")
                return new ObsValue(Obs.ValueDateTime);

            return new ObsValue(Obs.ValueText);
        }
    }
}