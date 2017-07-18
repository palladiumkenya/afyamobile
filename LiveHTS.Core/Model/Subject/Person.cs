using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
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
        [Ignore]
        public virtual IEnumerable<PersonAddress> Addresses { get; set; }=new List<PersonAddress>();
        [Ignore]
        public virtual IEnumerable<PersonContact> Contacts { get; set; }=new List<PersonContact>();

        public Person()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}