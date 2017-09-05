using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
  public  class PersonAddressJson : ISeedJson<PersonAddress>
    {
        public  List<PersonAddress> Read()
        {
            string raw = @"
[
  {
    ^Landmark^: ^Kibera School^,
    ^CountyId^: 47,
    ^Preferred^: 1,
    ^PersonId^: ^1fa07f17-d5fe-4daf-9eee-a7b7016df234^,
    ^Id^: ^a2125fa6-7776-11e7-b5a5-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Landmark^: ^Langata^,
    ^CountyId^: 47,
    ^Preferred^: 1,
    ^PersonId^: ^e8d87aa0-3970-4467-b2f4-a7b7016df22e^,
    ^Id^: ^a2126686-7776-11e7-b5a5-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Landmark^: ^Kibera School^,
    ^CountyId^: 47,
    ^Preferred^: 1,
    ^PersonId^: ^82dfdc68-6c3c-4a39-8f1f-a7b7016df22e^,
    ^Id^: ^a2127fa6-7776-11e7-b5a5-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<PersonAddress>>(raw.Replace("^","\""));
        }
    }
}
