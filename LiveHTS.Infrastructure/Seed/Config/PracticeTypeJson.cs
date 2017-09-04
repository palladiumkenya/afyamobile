using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class PracticeTypeJson : ISeedJson<PracticeType>
    {
        public  List<PracticeType> Read()
        {
            string raw = @"
[
 {
   ^Name^: ^Facility Based^,
   ^Id^: ^Facility^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<PracticeType>>(raw.Replace("^","\""));
        }
    }
}
