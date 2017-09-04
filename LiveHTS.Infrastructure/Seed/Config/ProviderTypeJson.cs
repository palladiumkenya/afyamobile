using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ProviderTypeJson : ISeedJson<ProviderType>
    {
        public  List<ProviderType> Read()
        {
            string raw = @"
[
 {
   ^Name^: ^Health Worker^,
   ^Id^: ^HW^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<ProviderType>>(raw.Replace("^","\""));
        }
    }
}
