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
            /*
Id|Name|Voided
Single|Single|0
Numeric|Numeric|0
Multi|Multi|0
Text|Text|0
             */

            if (Question.Concept.ConceptTypeId == "Single")
                return new ObsValue(typeof(Guid?), Obs.ValueCoded);
            if (Question.Concept.ConceptTypeId == "Numeric")
                return new ObsValue(typeof(decimal), Obs.ValueNumeric);
            if (Question.Concept.ConceptTypeId == "Multi")
                return new ObsValue(typeof(Guid[]), Obs.ValueMultiCoded);
            if (Question.Concept.ConceptTypeId == "DateTime")
                return new ObsValue(typeof(DateTime), Obs.ValueDateTime);


            return new ObsValue(typeof(string), Obs.ValueText);
        }
    }
}