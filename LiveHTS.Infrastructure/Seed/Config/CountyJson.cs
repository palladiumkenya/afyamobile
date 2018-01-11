using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class CountyJson : ISeedJson<County>
    {
        public  List<County> Read()
        {
            string raw = @"
[
  {
    ^Id^: 47,
    ^Name^: ^Nairobi^,
    ^Voided^: 0
  },
  {
    ^Id^: 41,
    ^Name^: ^Siaya^,
    ^Voided^: 0
  }
]

";

   

            return JsonConvert.DeserializeObject<List<County>>(raw.Replace("^","\""));
        }
    }
}
