using System;
using System.Collections.Generic;
using System.Reflection;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model
{
    public class Person:Entity<Guid>
    {
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Gender { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual bool? BirthDateEstimated { get; set; }
        public virtual string Email { get; set; }
        public virtual IEnumerable<PersonAddress> Addresses { get; set; }
        public virtual IEnumerable<PersonContact> Contacts { get; set; }
    }
}