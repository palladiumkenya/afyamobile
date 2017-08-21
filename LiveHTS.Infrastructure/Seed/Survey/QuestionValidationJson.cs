using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionValidationJson : ISeedJson<QuestionValidation>
    {
        public  List<QuestionValidation> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b260c818-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2603772-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260c96c-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b26039a2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260cab6-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2603c5e-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260cd9a-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2603dc6-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260cf0c-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260d060-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260417c-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260d1aa-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2604442-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260d420-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b26045aa-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260d6fa-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260487a-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260d86c-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b26049f6-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260d9c0-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2604ce4-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260dc86-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2604fb4-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260ddd0-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260511c-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260e1fe-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260525c-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260e3fc-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2605540-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260e56e-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2605694-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260e6b8-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260ea3c-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2605ab8-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260edd4-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260ef64-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b2605f54-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260f0b8-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b26060bc-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260f360-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b26062e2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260f4d2-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260644a-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260f626-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b260665c-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260f810-852f-11e7-bb31-be2e44b06b34^,
    ^ValidatorId^: ^Range^,
    ^ValidatorTypeId^: ^Numeric^,
    ^Revision^: 0,
    ^MinLimit^: 1,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^b26039a2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionValidation>>(raw.Replace("^","\""));
        }
    }
}
