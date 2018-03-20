using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public class MaritalStatusJson : ISeedJson<MaritalStatus>
    {
        public  List<MaritalStatus> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^S^,
    ^Name^: ^Single^,
    ^Voided^: 0
  },
  {
    ^Id^: ^MM^,
    ^Name^: ^Married Monogamous^,
    ^Voided^: 0
  },
  {
    ^Id^: ^MP^,
    ^Name^: ^Married Polygamous^,
    ^Voided^: 0
  },
  {
    ^Id^: ^D^,
    ^Name^: ^Divorced /Separated^,
    ^Voided^: 0
  },
  {
    ^Id^: ^W^,
    ^Name^: ^Widowed^,
    ^Voided^: 0
  },
  {
    ^Id^: ^NA^,
    ^Name^: ^Not applicable^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<MaritalStatus>>(raw.Replace("^","\""));
        }
    }
}
