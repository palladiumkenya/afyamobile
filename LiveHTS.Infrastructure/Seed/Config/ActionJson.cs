using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ActionJson : ISeedJson<Action>
    {
        public  List<Action> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^None^,
    ^Name^: ^None^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Rng^,
    ^Name^: ^Rng^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Rm^,
    ^Name^: ^Rm^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Set^,
    ^Name^: ^Set^,
    ^Voided^: 0
  }
]
";

   

            return JsonConvert.DeserializeObject<List<Action>>(raw.Replace("^","\""));
        }
    }
}
