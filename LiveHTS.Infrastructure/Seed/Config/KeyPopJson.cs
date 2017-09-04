using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class KeyPopJson : ISeedJson<KeyPop>
    {
        public  List<KeyPop> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^NA^,
    ^Name^: ^Not applicable^,
    ^Voided^: 0
  },
  {
    ^Id^: ^SW^,
    ^Name^: ^Sex worker^,
    ^Voided^: 0
  },
  {
    ^Id^: ^MSM^,
    ^Name^: ^Men who have sex with men^,
    ^Voided^: 0
  },
  {
    ^Id^: ^IDU^,
    ^Name^: ^Intravenous drug users^,
    ^Voided^: 0
  },
  {
    ^Id^: ^O^,
    ^Name^: ^Others^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<KeyPop>>(raw.Replace("^","\""));
        }
    }
}
