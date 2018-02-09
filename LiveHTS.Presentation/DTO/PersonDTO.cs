using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using Newtonsoft.Json;
using SQLite;

namespace LiveHTS.Presentation.DTO
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public  string FirstName { get; set; }
        public  string MiddleName { get; set; }
        public  string LastName { get; set; }
        public  string Gender { get; set; }
        public  DateTime BirthDate { get; set; }
        public  bool? BirthDateEstimated { get; set; }
        public  string Email { get; set; }
    }
}