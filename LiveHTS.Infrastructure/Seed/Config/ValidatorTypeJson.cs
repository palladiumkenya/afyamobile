using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ValidatorTypeJson : ISeedJson<ValidatorType>
    {
        public  List<ValidatorType> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^None^,
    ^Name^: ^None^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Numeric^,
    ^Name^: ^Numeric^,
    ^Voided^: 0
  },
  {
    ^Id^: ^Count^,
    ^Name^: ^Count^,
    ^Voided^: 0
  }
]
";

   

            return JsonConvert.DeserializeObject<List<ValidatorType>>(raw.Replace("^","\""));
        }
    }
}
