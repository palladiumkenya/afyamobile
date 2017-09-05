using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class IdentifierTypeJson : ISeedJson<IdentifierType>
    {
        public  List<IdentifierType> Read()
        {
            string raw = @"
[
 {
   ^Name^: ^Serial^,
   ^Id^: ^Serial^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<IdentifierType>>(raw.Replace("^","\""));
        }
    }
}
