using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Lookup;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Lookup
{
  public  class CategoryItemJson : ISeedJson<CategoryItem>
    {
        public  List<CategoryItem> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^6206b720-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040a3e-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2a902-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206b8b0-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040a3e-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2aae2-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206bd06-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040b24-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2abb4-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206be1e-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040b24-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2ac90-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206befa-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040b24-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2ad58-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206bfcc-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040c00-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2b2f8-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206c0a8-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040c00-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2b3c0-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206c184-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040c00-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2b4f8-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^6206c256-6260-11e7-907b-a6006ad3dba0^,
    ^CategoryId^: ^62040c00-6260-11e7-907b-a6006ad3dba0^,
    ^ItemId^: ^00c2b488-6246-11e7-907b-a6006ad3dba0^,
    ^Display^: ^^,
    ^Rank^: 4,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<CategoryItem>>(raw.Replace("^","\""));
        }
    }
}
