using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.SmartCard
{

    public class SHR
    {
        public string VERSION { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<NEXTOFKIN> NEXT_OF_KIN { get; set; }=new List<NEXTOFKIN>();
        public List<HIVTEST> HIV_TEST { get; set; } = new List<HIVTEST>();
        public List<IMMUNIZATION> IMMUNIZATION { get; set; } = new List<IMMUNIZATION>();
        public CARDDETAILS CARD_DETAILS { get; set; }

        public Client GetClient(Guid practiceId, Guid userId)
        {

            var smartClient = SmartClientDTO.Create(this);
            var smartPerson = Person.Create(smartClient.FirstName, smartClient.MiddleName, smartClient.LastName,
                smartClient.Sex, smartClient.BirthDate, smartClient.BirthDateEstimated, string.Empty,
                smartClient.Landmark, smartClient.Phone);

            var client = Client.Create(smartClient.MaritalStatus, smartClient.KeyPop, smartClient.OtherKeyPop,
                practiceId, smartPerson, userId);
            client.PersonId = smartPerson.Id;

            var clientIdentifier =
                ClientIdentifier.Create("Serial", smartClient.HtsNumber, DateTime.Now, true, client.Id);
            client.AddIdentifier(clientIdentifier);
            client.SmartCardSerial = smartClient.SmartCardSerial;

            return client;
        }

    }
}