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
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^6206a9a6-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^62069a60-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^62069b3c-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^6206ab4a-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^62069fe2-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^6206ac1c-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a10e-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ValidatorId^: ^Required^,
    ^ValidatorTypeId^: ^None^,
    ^Revision^: 0,
    ^MinLimit^: ^^,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a1f4-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ValidatorId^: ^Range^,
    ^ValidatorTypeId^: ^Numeric^,
    ^Revision^: 0,
    ^MinLimit^: 1,
    ^MaxLimit^: 5,
    ^QuestionId^: ^6206ab4a-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a2d0-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ValidatorId^: ^Range^,
    ^ValidatorTypeId^: ^Count^,
    ^Revision^: 0,
    ^MinLimit^: 2,
    ^MaxLimit^: ^^,
    ^QuestionId^: ^6206ac1c-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a3a2-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionValidation>>(raw.Replace("^","\""));
        }
    }
}
