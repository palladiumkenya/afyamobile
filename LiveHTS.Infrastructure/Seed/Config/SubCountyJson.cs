using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class SubCountyJson : ISeedJson<SubCounty>
    {
        public  List<SubCounty> Read()
        {
            string raw = @"


[
  {
    ^Id^: ^ab0546b4-98b9-11e7-abc4-cec278b6b50a^,
    ^Name^: ^Kibera^,
    ^CountyId^: 47,
    ^Voided^: 0
  },
  {
    ^Id^: ^ab05479a-98b9-11e7-abc4-cec278b6b50a^,
    ^Name^: ^Gem^,
    ^CountyId^: 41,
    ^Voided^: 0
  }
]
";

   

            return JsonConvert.DeserializeObject<List<SubCounty>>(raw.Replace("^","\""));
        }
    }
}
