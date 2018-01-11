using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
    public class ProviderJson :ISeedJson<Provider>
    {
        public  List<Provider> Read()
        {
            string raw = @"
[
  {
    ^ProviderTypeId^: ^HW^,
    ^Code^: ^Code1^,
    ^PracticeId^: ^AB054358-98B9-11E7-ABC4-CEC278B6B50A^,
    ^PersonId^: ^b4d18679-ed7e-4e02-8cc5-a7b7016df233^,
    ^Id^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
    ^Voided^: 0
  }
]
         ";

            return JsonConvert.DeserializeObject<List<Provider>>(raw.Trim().Replace("^", "\""));
        }
    }
}
