using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed
{
  public  class UserJson
    {
        public static List<User> Read()
        {
            string raw = @"
[
 {
   ^UserName^: ^admin^,
   ^Password^: ^maun2806^,
   ^PracticeId^: ^7e51629e-6b99-11e7-907b-a6006ad3dba0^,
   ^PersonId^: ^b4d18679-ed7e-4e02-8cc5-a7b7016df233^,
   ^Id^: ^61a9e04c-2ed0-414a-9387-a7b7016df233^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<User>>(raw.Replace("^","\""));
        }
    }
}
