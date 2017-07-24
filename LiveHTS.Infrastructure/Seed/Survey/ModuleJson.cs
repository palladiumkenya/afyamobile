using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class ModuleJson : ISeedJson<Module>
    {
        public  List<Module> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS Module^,
    ^Display^: ^Hiv Testing Services Module^,
    ^Description^: ^Hiv Testing Services Module^,
    ^Rank^: 1,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Module>>(raw.Replace("^","\""));
        }
    }
}
