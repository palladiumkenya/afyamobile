using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionRemoteTransformationJson : ISeedJson<QuestionRemoteTransformation>
    {
        public  List<QuestionRemoteTransformation> Read()
        {
            string raw = @"
[
  {
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^SelfQuestionId^: ^^,
    ^ResponseType^: ^!=^,
    ^Response^: ^00c2ad58-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 1,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^9f6e1b46-67a9-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^^,
    ^SelfQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^ResponseType^: ^!=^,
    ^Response^: ^00c2ad58-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 1,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^9f6e1c72-67a9-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^SelfQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2abb4-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 2,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^9f6e1d4e-67a9-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^^,
    ^SelfQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2ac90-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 2,
    ^ActionId^: ^Set^,
    ^Content^: ^00c2a902-6246-11e7-907b-a6006ad3dba0^,
    ^AltContent^: ^00c2aae2-6246-11e7-907b-a6006ad3dba0^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^9f6e1e16-67a9-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^SelfQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2ac90-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 3,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^9f6e1ede-67a9-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^^,
    ^SelfQuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2abb4-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 3,
    ^ActionId^: ^Set^,
    ^Content^: ^00c2a902-6246-11e7-907b-a6006ad3dba0^,
    ^AltContent^: ^00c2aae2-6246-11e7-907b-a6006ad3dba0^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^9f6e1f9c-67a9-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionRemoteTransformation>>(raw.Replace("^","\""));
        }
    }
}
