using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
  public  class ClientIdentifierJson : ISeedJson<ClientIdentifier>
    {
        public  List<ClientIdentifier> Read()
        {
            string raw = @"
[
 {
   ^IdentifierTypeId^: ^Serial^,
   ^Identifier^: 201707001,
   ^Preferred^: 1,
   ^RegistrationDate^:^2017AUG01^,
   ^ClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df232^,
   ^Id^: ^7e61629e-6b99-11e7-907b-a6006ad4dba0^,
   ^Voided^: 0
 },
 {
   ^IdentifierTypeId^: ^Serial^,
   ^Identifier^: 201707002,
   ^Preferred^: 1,
   ^RegistrationDate^:^2017AUG01^,
   ^ClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df234^,
   ^Id^: ^7e62629e-6b99-11e7-907b-a6006ad4dba0^,
   ^Voided^: 0
 },
 {
   ^IdentifierTypeId^: ^Serial^,
   ^Identifier^: 201707003,
   ^Preferred^: 1,
    ^RegistrationDate^:^2017AUG01^,
   ^ClientId^: ^d3bf79fe-a049-49fa-b83c-a7b7016df233^,
   ^Id^: ^7e62639e-6b99-11e7-907b-a6006ad4dba0^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<ClientIdentifier>>(raw.Replace("^","\""));
        }
    }
}
