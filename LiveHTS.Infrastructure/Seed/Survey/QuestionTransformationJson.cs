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
    ^Id^: ^b261ac60-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25ede36-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^RefQuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^ActionId^: ^Block^,
    ^Rank^: 2,
    ^Content^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261aed6-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25ede36-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^RefQuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^ActionId^: ^Set^,
    ^Rank^: 1,
    ^Content^: ^b25ed1c0-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261b16a-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2604ce4-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^RefQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^ActionId^: ^Set^,
    ^Rank^: 1,
    ^Content^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261b3cc-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b2604ce4-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Post^,
    ^ResponseType^: ^=^,
    ^Response^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^ResponseComplex^: ^^,
    ^Group^: ^^,
    ^RefQuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^ActionId^: ^Block^,
    ^Rank^: 2,
    ^Content^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261b606-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ResponseType^: ^^,
    ^Response^: ^^,
    ^ResponseComplex^: ^Client.Partner.No^,
    ^Group^: ^^,
    ^RefQuestionId^: ^^,
    ^ActionId^: ^Set^,
    ^Rank^: 1,
    ^Content^: ^b25ede36-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261b8ae-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ResponseType^: ^^,
    ^Response^: ^^,
    ^ResponseComplex^: ^Client.Partner.Yes^,
    ^Group^: ^^,
    ^RefQuestionId^: ^^,
    ^ActionId^: ^Set^,
    ^Rank^: 1,
    ^Content^: ^b25ee0a2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261bb38-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ResponseType^: ^^,
    ^Response^: ^^,
    ^ResponseComplex^: ^Client.Partner.No^,
    ^Group^: ^^,
    ^RefQuestionId^: ^^,
    ^ActionId^: ^Block^,
    ^Rank^: 2,
    ^Content^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b261bd86-852f-11e7-bb31-be2e44b06b34^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConditionId^: ^Pre^,
    ^ResponseType^: ^^,
    ^Response^: ^^,
    ^ResponseComplex^: ^Client.Partner.Yes^,
    ^Group^: ^^,
    ^RefQuestionId^: ^^,
    ^ActionId^: ^Block^,
    ^Rank^: 2,
    ^Content^: ^^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionTransformation>>(raw.Replace("^","\""));
        }
    }
}
