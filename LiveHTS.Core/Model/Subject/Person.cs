using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class Person:Entity<Guid>, IPerson
    {
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Gender { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual bool? BirthDateEstimated { get; set; }
        public virtual string Email { get; set; }

        [Ignore]
        public virtual string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }
        [Ignore]
        public virtual IEnumerable<PersonAddress> Addresses { get; set; }=new List<PersonAddress>();
        [Ignore]
        public virtual IEnumerable<PersonContact> Contacts { get; set; }=new List<PersonContact>();

        [Ignore]
        public virtual string AgeInfo
        {
            get
            {
                if (null != BirthDate)
                {
                    var personAge = SharedKernel.Custom.Utils.CalculateAge(BirthDate.Value);
                    return personAge.ToFullAgeString();
                }
                return string.Empty;
            }
        }

        public Person()
        {
            Id = LiveGuid.NewGuid();
        }

        private Person(string firstName, string middleName, string lastName, string gender, DateTime? birthDate, bool? birthDateEstimated, string email):this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            BirthDateEstimated = birthDateEstimated;
            Email = email;
        }

        public static Person Create(string firstName, string middleName, string lastName, string gender,DateTime? birthDate, bool? birthDateEstimated, string email)
        {
            return new Person(firstName, middleName, lastName, gender, birthDate, birthDateEstimated, email);
        }
        public static Person Create(string firstName, string middleName, string lastName, string gender, DateTime? birthDate, bool? birthDateEstimated, string email,Guid personId)
        {
            var person=Create(firstName, middleName, lastName, gender, birthDate, birthDateEstimated, email);
            person.Id = personId;
            return person;
        }
        public void AddAddress(string landmark, int? countyId, bool preferred, decimal? lat, decimal? lng)
        {
            var addressList = Addresses.ToList();
            var address = PersonAddress.Create(landmark, countyId, preferred, lat, lng, Id);
            addressList.Add(address);

            Addresses = addressList;
        }

        public void AddContact(int? phone, bool preferred)
        {
            var contactList = Contacts.ToList();
            var contact = PersonContact.Create(phone, preferred, Id);
            contactList.Add(contact);

            Contacts = contactList;
        }

        public override string ToString()
        {
            return $"{FullName}";
        }
    }
}