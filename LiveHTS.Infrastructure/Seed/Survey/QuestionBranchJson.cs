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
    ^QuestionId^: ^Ever Tested?,  b2603772-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^Y,  b25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^Re-Testing (No. Months since last test),  b26039a2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261448c-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^Consent,  b2603dc6-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^N,  b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^Remarks,  b260665c-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b26146e4-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^Test Result,  b2604ce4-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^N,  b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^Final Result,  b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2614946-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^Linked to Care?,  b26060bc-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^Y,  b25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^(Enter CCC#),  b26062e2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2615b98-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^Final Result Given?,  b2605ab8-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^N,  b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^Reason Result Not Given,  b26067b0-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionBranch>>(raw.Replace("^","\""));
        }
    }
}
