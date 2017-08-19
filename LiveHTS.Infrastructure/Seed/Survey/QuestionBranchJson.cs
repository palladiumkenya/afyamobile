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
    ^ConditionId^: ^Post^,
    ^RefQuestionId^: ^^,
    ^ResponseType^: ^=^,
    ^Response^: ^00c2aae2-6246-11e7-907b-a6006ad3dba0^,
    ^ResponseComplex^: ^^,
    ^Group^: 0,
    ^ActionId^: ^None^,
    ^GotoQuestionId^: ^6206ac1c-6260-11e7-907b-a6006ad3dba0^,
    ^QuestionId^: ^6206a9a6-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6203d8de-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<QuestionBranch>>(raw.Replace("^","\""));
        }
    }
}
