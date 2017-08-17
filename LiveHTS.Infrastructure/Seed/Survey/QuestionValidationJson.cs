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
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionValidation>>(raw.Replace("^","\""));
        }
    }
}
