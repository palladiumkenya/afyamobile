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

        public void Write(Client client)
        {
            var person = client.Person;
            if (null != PATIENT_IDENTIFICATION)
            {
                if (null != PATIENT_IDENTIFICATION.PATIENT_NAME)
                {
                    PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = person.FirstName.ToUpper();
                    PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = person.MiddleName.ToUpper();
                    PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = person.LastName.ToUpper();
                    PATIENT_IDENTIFICATION.SEX = person.Gender.ToUpper();
                    PATIENT_IDENTIFICATION.DATE_OF_BIRTH = person.BirthDate.ToString("yyyyMMdd");
                    PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION = GetPrecision(person.BirthDateEstimated);

                    /*
                       GetHtsNumber(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetFacility(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetMaritalStatus(shr.PATIENT_IDENTIFICATION.MARITAL_STATUS),
                GetCardSerial(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID), GetLandmark(shr.PATIENT_IDENTIFICATION),
                GetPhone(shr.PATIENT_IDENTIFICATION));
                     */
                }

               
            }
        }

        private void WritePatientInfo(Client client)
        {
            var person = client.Person;
            if (null != PATIENT_IDENTIFICATION)
            {
                if (null != PATIENT_IDENTIFICATION.PATIENT_NAME)
                {
                    PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = person.FirstName.ToUpper();
                    PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = person.MiddleName.ToUpper();
                    PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = person.LastName.ToUpper();
                    PATIENT_IDENTIFICATION.SEX = person.Gender.ToUpper();
                    PATIENT_IDENTIFICATION.DATE_OF_BIRTH = person.BirthDate.ToString("yyyyMMdd");
                    PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION = GetPrecision(person.BirthDateEstimated);

                    /*
                       GetHtsNumber(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetFacility(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetMaritalStatus(shr.PATIENT_IDENTIFICATION.MARITAL_STATUS),
                GetCardSerial(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID), GetLandmark(shr.PATIENT_IDENTIFICATION),
                GetPhone(shr.PATIENT_IDENTIFICATION));
                     */
                }



            }
            else
            {
                VERSION = "1.0.0";

            }
        }

        private string GetPrecision(bool? estimated)
        {
            return null!=estimated && estimated.Value ? "ESTIMATED" : "EXACT";
        }
    }
}