using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using Newtonsoft.Json;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class Person:Entity<Guid>, IPerson
    {
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Gender { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual bool? BirthDateEstimated { get; set; }
        public virtual string Email { get; set; }

        [Ignore]
        [JsonIgnore]
        public virtual string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }
        [Ignore]
        public virtual List<PersonAddress> Addresses { get; set; }=new List<PersonAddress>();
        [Ignore]
        public virtual List<PersonContact> Contacts { get; set; }=new List<PersonContact>();

        [Ignore]
        [JsonIgnore]
        public virtual string AgeInfo
        {
            get
            {
                if (null != BirthDate)
                {
                    var personAge = SharedKernel.Custom.Utils.CalculateAge(BirthDate);
                    return personAge.ToString();
                }
                return string.Empty;
            }
        }

        [Ignore]
        [JsonIgnore]
        public bool IsPead
        {
            get
            {
                try
                {
                    var age = SharedKernel.Custom.Utils.CalculateAge(BirthDate);
                    return age.Age < 15;
                }
                catch
                {
                }

                return false;
            }
        }

        [Ignore]
        [JsonIgnore]
        public bool IsOverAge
        {
            get
            {
                try
                {
                  return  SharedKernel.Custom.Utils.CheckDateGreaterThanLimit(BirthDate,1,5);
                }
                catch
                {
                }

                return false;
            }
        }

        public Person()
        {
            Id = LiveGuid.NewGuid();
        }

        private Person(string firstName, string middleName, string lastName, string gender, DateTime birthDate, bool? birthDateEstimated, string email, string nickName):this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            BirthDateEstimated = birthDateEstimated;
            Email = email;
            NickName = nickName;
        }

        public static Person Create(string firstName, string middleName, string lastName, string gender,DateTime birthDate, bool? birthDateEstimated, string email, string nickName)
        {
            return new Person(firstName, middleName, lastName, gender, birthDate, birthDateEstimated, email,nickName);
        }

        public static Person Create(string firstName, string middleName, string lastName, string gender,
            DateTime birthDate, bool? birthDateEstimated, string email, string landmark, int? phone, string nickName, int? countyId, int? subCountyId, int? wardId)
        {
            var person = new Person(firstName, middleName, lastName, gender, birthDate, birthDateEstimated, email,nickName);

            if (!string.IsNullOrWhiteSpace(landmark))
                person.AddAddress(landmark, countyId, true, null, null,subCountyId,wardId);

            if (phone.HasValue)
                person.AddContact(phone, true);

            return person;
        }

        public static Person Create(string firstName, string middleName, string lastName, string gender, DateTime birthDate, bool? birthDateEstimated, string email,Guid personId, string nickName)
        {
            var person=Create(firstName, middleName, lastName, gender, birthDate, birthDateEstimated, email, nickName);
            person.Id = personId;
            return person;
        }

        public void AddAddress(string landmark, int? countyId, bool preferred, decimal? lat, decimal? lng, int? subCountyId, int? wardId)
        {
            var addressList = Addresses.ToList();
            var address = PersonAddress.Create(landmark, countyId, preferred, lat, lng, Id, subCountyId, wardId);
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
