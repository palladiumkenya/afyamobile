using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Lookup;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Lookup
{
  public  class CategoryJson : ISeedJson<Category>
    {
        public  List<Category> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^62040a3e-6260-11e7-907b-a6006ad3dba0^,
    ^Code^: ^YesNo^,
    ^Voided^: 0
  },
  {
    ^Id^: ^62040b24-6260-11e7-907b-a6006ad3dba0^,
    ^Code^: ^Result^,
    ^Voided^: 0
  },
  {
    ^Id^: ^62040c00-6260-11e7-907b-a6006ad3dba0^,
    ^Code^: ^Services^,
    ^Voided^: 0
  }
]";
            return JsonConvert.DeserializeObject<List<Category>>(raw.Replace("^","\""));
        }
    }
}
