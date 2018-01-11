using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ValidatorJson : ISeedJson<Validator>
    {
        public  List<Validator> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^Required^,
    ^Name^: ^Required^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Range^,
    ^Name^: ^Range^,
    ^Voided^: 0
  }
]
";

   

            return JsonConvert.DeserializeObject<List<Validator>>(raw.Replace("^","\""));
        }
    }
}
