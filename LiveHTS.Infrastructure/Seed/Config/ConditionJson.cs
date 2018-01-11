using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ConditionJson : ISeedJson<Condition>
    {
        public  List<Condition> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^Post^,
    ^Name^: ^Post^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Pre^,
    ^Name^: ^Pre^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Condition>>(raw.Replace("^","\""));
        }
    }
}
