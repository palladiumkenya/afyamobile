using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionReValidationJson : ISeedJson<QuestionReValidation>
    {
        public  List<QuestionReValidation> Read()
        {
            string raw = @"
[
  {
    ^ConditionId^: ^Pre^,
    ^RefQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2ad58-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^Rng^,
    ^QuestionValidationId^: ^6206a2d0-6260-11e7-907b-a6006ad3dba0^,
    ^QuestionId^: ^6206ab4a-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6203e068-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionReValidation>>(raw.Replace("^","\""));
        }
    }
}
