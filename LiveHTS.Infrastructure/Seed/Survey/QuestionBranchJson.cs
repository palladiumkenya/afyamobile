using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionBranchJson : ISeedJson<QuestionBranch>
    {
        public  List<QuestionBranch> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b2614234-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2603772-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^b2603c5e-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261448c-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2603dc6-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^b260665c-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b26146e4-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2604ce4-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2614946-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b26060bc-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^b26062e2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2615b98-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2605ab8-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^b26067b0-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionBranch>>(raw.Replace("^","\""));
        }
    }
}
