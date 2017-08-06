using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionTransformationJson : ISeedJson<QuestionTransformation>
    {
        public  List<QuestionTransformation> Read()
        {
            string raw = @"
[
  {
    ^ConditionId^: ^Pre^,
    ^RefQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2abb4-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^Rm^,
    ^Content^: ^00c2b2f8-6246-11e7-907b-a6006ad3dba0^,
    ^QuestionId^: ^6206ac1c-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6203de42-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConditionId^: ^Pre^,
    ^RefQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2ac90-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^Rm^,
    ^Content^: ^00c2b3c0-6246-11e7-907b-a6006ad3dba0^,
    ^QuestionId^: ^6206ac1c-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6203df82-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionTransformation>>(raw.Replace("^","\""));
        }
    }
}
