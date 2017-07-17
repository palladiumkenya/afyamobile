using System;
using System.Collections.Generic;
using System.Reflection;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model
{
    public class Person:Entity<Guid>
    {
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<PersonIdentifier> Identifiers { get; set; }
    }
}