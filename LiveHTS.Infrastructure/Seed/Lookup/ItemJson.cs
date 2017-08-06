using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Lookup;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Lookup
{
  public  class ItemJson : ISeedJson<Item>
    {
        public  List<Item> Read()
        {
            string raw = @"
[
  {
    ^Code^: ^Y^,
    ^Display^: ^Y^,
    ^Id^: ^00c2a902-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^N^,
    ^Display^: ^N^,
    ^Id^: ^00c2aae2-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^Pos^,
    ^Display^: ^Pos^,
    ^Id^: ^00c2abb4-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^Neg^,
    ^Display^: ^Neg^,
    ^Id^: ^00c2ac90-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^Inc^,
    ^Display^: ^Inc^,
    ^Id^: ^00c2ad58-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^PrEP^,
    ^Display^: ^PrEP^,
    ^Id^: ^00c2b2f8-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^CCC^,
    ^Display^: ^CCC^,
    ^Id^: ^00c2b3c0-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^Compulsory^,
    ^Display^: ^Compulsory^,
    ^Id^: ^00c2b488-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Code^: ^Counselling^,
    ^Display^: ^Counselling^,
    ^Id^: ^00c2b4f8-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Item>>(raw.Replace("^","\""));
        }
    }
}
