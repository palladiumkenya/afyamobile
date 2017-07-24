using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
  public  class ClientJson : ISeedJson<Client>
    {
        public  List<Client> Read()
        {
            string raw = @"
[
 {
   ^PracticeId^: ^7e51629e-6b99-11e7-907b-a6006ad3dba0^,
   ^PersonId^: ^82dfdc68-6c3c-4a39-8f1f-a7b7016df22e^,
   ^Id^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df232^,
   ^Voided^: 0
 },
 {
   ^PracticeId^: ^7e51629e-6b99-11e7-907b-a6006ad3dba0^,
   ^PersonId^: ^1fa07f17-d5fe-4daf-9eee-a7b7016df234^,
   ^Id^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df234^,
   ^Voided^: 0
 },
 {
   ^PracticeId^: ^7e51629e-6b99-11e7-907b-a6006ad3dba0^,
   ^PersonId^: ^e8d87aa0-3970-4467-b2f4-a7b7016df22e^,
   ^Id^: ^d3bf79fe-a049-49fa-b83c-a7b7016df233^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<Client>>(raw.Replace("^","\""));
        }
    }
}
