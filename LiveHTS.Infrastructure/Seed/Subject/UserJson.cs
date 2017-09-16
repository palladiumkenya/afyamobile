using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
    public class UserJson :ISeedJson<User>
    {
        public  List<User> Read()
        {
            string raw = @"
            [
             {
               ^UserName^: ^admin^,
               ^Password^: ^maun2806^,
               ^PracticeId^: ^AB054358-98B9-11E7-ABC4-CEC278B6B50A^,
               ^PersonId^: ^b4d18679-ed7e-4e02-8cc5-a7b7016df233^,
               ^Id^: ^61a9e04c-2ed0-414a-9387-a7b7016df233^,
               ^Voided^: 0
             }
            ]
         ";

            return JsonConvert.DeserializeObject<List<User>>(raw.Trim().Replace("^", "\""));
        }
    }
}
