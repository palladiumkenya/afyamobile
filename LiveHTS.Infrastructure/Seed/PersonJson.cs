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
  public  class PersonJson
    {
        public static List<Person> Read()
        {
            string raw = @"
[
 {
   ^FirstName^: ^HTS^,
   ^MiddleName^: ^^,
   ^LastName^: ^Admin^,
   ^Gender^: ^M^,
   ^BirthDate^: ^2017-07-04^,
   ^BirthDateEstimated^: 0,
   ^Email^: ^koskedk@gmail.com^,
   ^Id^: ^b4d18679-ed7e-4e02-8cc5-a7b7016df233^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<Person>>(raw.Replace("^","\""));
        }
    }
}
