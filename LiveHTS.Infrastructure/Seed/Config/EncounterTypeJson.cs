using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class EncounterTypeJson : ISeedJson<EncounterType>
    {
        public  List<EncounterType> Read()
        {
            string raw = @"
[
 {
   ^Name^: ^HTS Initial^,
   ^Id^: ^7e5164a6-6b99-11e7-907b-a6006ad3dba0^,
   ^Voided^: 0
 },
 {
   ^Name^: ^HTS Initial Test^,
   ^Id^: ^b262f4ee-852f-11e7-bb31-be2e44b06b34^,
   ^Voided^: 0
 },
 {
   ^Name^: ^HTS Confirmatory Test^,
   ^Id^: ^b262faac-852f-11e7-bb31-be2e44b06b34^,
   ^Voided^: 0
 },
 {
   ^Name^: ^HTS Linkage^,
   ^Id^: ^b262fc32-852f-11e7-bb31-be2e44b06b34^,
   ^Voided^: 0
 },
 {
   ^Name^: ^HTS Trace^,
   ^Id^: ^b262fda4-852f-11e7-bb31-be2e44b06b34^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<EncounterType>>(raw.Replace("^","\""));
        }
    }
}

