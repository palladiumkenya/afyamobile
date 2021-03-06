﻿using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ServerConfigJson : ISeedJson<ServerConfig>
    {
        public  List<ServerConfig> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^hapi.central^,    
    ^Address^: ^http://data.kenyahmis.org:4747/^,
    ^Code^: ^1^,
    ^Name^: ^^,
    ^Instance^: ^00000000-0000-0000-0000-000000000000^,
    ^IsSetup^: 0,
    ^Voided^: 0
  },
  {
    ^Id^: ^hapi.local^,    
    ^Address^: ^http://52.178.24.227:4747/^,
    ^Code^: ^^,
    ^Name^: ^^,
    ^Instance^: ^00000000-0000-0000-0000-000000000000^,
    ^IsSetup^: 0,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<ServerConfig>>(raw.Replace("^","\""));
        }
    }
}
