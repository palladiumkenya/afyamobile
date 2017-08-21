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
    ^Id^: ^b262de32-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^SelfQuestionId^: ^^,
    ^ResponseType^: ^!=^,
    ^Response^: ^b25f017c-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: 1,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b262dff4-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^^,
    ^SelfQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseType^: ^!=^,
    ^Response^: ^b25f017c-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: 1,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b262e3be-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^SelfQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: 2,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b262e562-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^^,
    ^SelfQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: 2,
    ^ActionId^: ^Set^,
    ^Content^: ^25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^AltContent^: ^25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b262e6b6-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^SelfQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: 3,
    ^ActionId^: ^^,
    ^Content^: ^^,
    ^AltContent^: ^^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b262e828-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ClientAttributeId^: ^Partner^,
    ^RemoteQuestionId^: ^^,
    ^SelfQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: 3,
    ^ActionId^: ^Set^,
    ^Content^: ^25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^AltContent^: ^25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionRemoteTransformation>>(raw.Replace("^","\""));
        }
    }
}
