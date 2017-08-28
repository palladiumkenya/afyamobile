using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
    public class ProgramJson : ISeedJson<Program>
    {
        public  List<Program> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b262ff02-852f-11e7-bb31-be2e44b06b34^,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^EncounterTypeId^: ^7e5164a6-6b99-11e7-907b-a6006ad3dba0^,
    ^Display^: ^Lab Detail^,
    ^Description^: ^Lab Detail^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b263006a-852f-11e7-bb31-be2e44b06b34^,
    ^FormId^: ^b25ec568-852f-11e7-bb31-be2e44b06b34^,
    ^EncounterTypeId^: ^b262f4ee-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^Initial Test^,
    ^Description^: ^Initial Test^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b26301d2-852f-11e7-bb31-be2e44b06b34^,
    ^FormId^: ^b25ec568-852f-11e7-bb31-be2e44b06b34^,
    ^EncounterTypeId^: ^b262faac-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^Confirmatory Test^,
    ^Description^: ^Confirmatory Test^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b263063c-852f-11e7-bb31-be2e44b06b34^,
    ^FormId^: ^b25ec112-852f-11e7-bb31-be2e44b06b34^,
    ^EncounterTypeId^: ^b262fc32-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^Linkage^,
    ^Description^: ^Linkage^,
    ^Rank^: 4,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Program>>(raw.Replace("^", "\""));
        }
    }
}

/*
 */
