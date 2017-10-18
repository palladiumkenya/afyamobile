using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
  public  class PersonJson :ISeedJson<Person>
    {
        public  List<Person> Read()
        {
            string raw = @"
[
 {
   ^FirstName^: ^Bob^,
   ^MiddleName^: ^Lee^,
   ^LastName^: ^Swagger^,
   ^Gender^: ^M^,
   ^BirthDate^: ^1976-11-04^,
   ^BirthDateEstimated^: 0,
   ^Email^: ^bswagger@gmail.com^,
   ^Id^: ^1fa07f17-d5fe-4daf-9eee-a7b7016df234^,
   ^Voided^: 0
 },
 {
   ^FirstName^: ^Julie^,
   ^MiddleName^: ^^,
   ^LastName^: ^Swagger^,
   ^Gender^: ^F^,
   ^BirthDate^: ^1982-05-01^,
   ^BirthDateEstimated^: 0,
   ^Email^: ^jswagger@gmail.com^,
   ^Id^: ^82dfdc68-6c3c-4a39-8f1f-a7b7016df22e^,
   ^Voided^: 0
 },
 {
   ^FirstName^: ^HTS^,
   ^MiddleName^: ^^,
   ^LastName^: ^Provider^,
   ^Gender^: ^M^,
   ^BirthDate^: ^1983-07-03^,
   ^BirthDateEstimated^: 0,
   ^Email^: ^koskedk@gmail.com^,
   ^Id^: ^b4d18679-ed7e-4e02-8cc5-a7b7016df233^,
   ^Voided^: 0
 },
 {
   ^FirstName^: ^Frank^,
   ^MiddleName^: ^J^,
   ^LastName^: ^Underwood^,
   ^Gender^: ^M^,
   ^BirthDate^: ^1977-09-12^,
   ^BirthDateEstimated^: 1,
   ^Email^: ^funderwood@gmail.com^,
   ^Id^: ^e8d87aa0-3970-4467-b2f4-a7b7016df22e^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<Person>>(raw.Replace("^","\""));
        }
    }

    public class PersonUserJson : ISeedJson<Person>
    {
        public List<Person> Read()
        {
            string raw = @"
[
 {
   ^FirstName^: ^HTS^,
   ^MiddleName^: ^^,
   ^LastName^: ^Provider^,
   ^Gender^: ^M^,
   ^BirthDate^: ^1983-07-03^,
   ^BirthDateEstimated^: 0,
   ^Email^: ^koskedk@gmail.com^,
   ^Id^: ^b4d18679-ed7e-4e02-8cc5-a7b7016df233^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<Person>>(raw.Replace("^", "\""));
        }
    }
}

/*
   
 */
