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
    ^Id^: ^b260c688-852f-11e7-bb31-be2e44b06b34^,
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
