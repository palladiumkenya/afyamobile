using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Interview
{
  public  class EncounterJson : ISeedJson<Encounter>
    {
        public  List<Encounter> Read()
        {
            string raw = @"
[
  {
    ^ClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df232^,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^EncounterTypeId^: ^7e5164a6-6b99-11e7-907b-a6006ad3dba0^,
    ^EncounterDate^: ^2017-07-22T00:00:00.000^,
    ^ProviderId^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
    ^DeviceId^: ^7e51658c-6b99-11e7-907b-a6006ad3dba0^,
    ^PracticeId^: ^7e51629e-6b99-11e7-907b-a6006ad3dba0^,
    ^Started^: ^2017-07-22T00:00:00.000^,
    ^Stopped^: ^2017-07-22T00:00:00.000^,
    ^UserId^: ^00000000-0000-0000-0000-000000000001^,
    ^IsComplete^: 0,
    ^Id^: ^afc9f878-c187-487d-bd82-a7b7016df23c^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Encounter>>(raw.Replace("^","\""));
        }
    }
}
