using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.SmartCard
{
    public class SHR
    {
        public string VERSION { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public CARDDETAILS CARD_DETAILS { get; set; }
        public List<HIVTEST> HIV_TEST { get; set; } = new List<HIVTEST>();
        public List<IMMUNIZATION> IMMUNIZATION { get; set; } = new List<IMMUNIZATION>();
        public List<NEXTOFKIN> NEXT_OF_KIN { get; set; } = new List<NEXTOFKIN>();

        public SHR()
        {
            VERSION = "1.0.0";
        }

        public static SHR CreateBlank()
        {
            var shr=new SHR();
            shr.PATIENT_IDENTIFICATION=PATIENTIDENTIFICATION.Create();
            shr.CARD_DETAILS=new CARDDETAILS();
            //shr.HIV_TEST = HIVTEST.Create();
            //shr.IMMUNIZATION = SmartCard.IMMUNIZATION.Create();
            //shr.NEXT_OF_KIN = NEXTOFKIN.Create();
            return shr;
        }

        public static SHR CreateBlank(Client client, string code)
        {
            var shr = CreateBlank();
            shr.CreateFrom(client, code);
            return shr;
        }

        public bool HasHtsIds()
        {
            return null != PATIENT_IDENTIFICATION &&
                   null != PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID &&
                   PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Any(x => x.IDENTIFIER_TYPE.IsSameAs("HTS_NUMBER"));

        }

        public bool IsBlank()
        {
            return CARD_DETAILS.IsNew();
        }
        public bool HasHtsNumber()
        {
            if (HasHtsIds())
            {
                var htsNumber = PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.FirstOrDefault(x =>
                    x.IDENTIFIER_TYPE.IsSameAs("HTS_NUMBER") && !string.IsNullOrWhiteSpace(x.ID));
                return null != htsNumber;
            }

            return false;
        }

        public List<INTERNALPATIENTID> GetHtsInternalpatientids()
        {
            if(HasHtsNumber())
                return PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Where(x => x.IDENTIFIER_TYPE.IsSameAs("HTS_NUMBER")).ToList();

            return new List<INTERNALPATIENTID>();
        }
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
                ClientIdentifier.Create("Serial", smartClient.HtsNumber, DateTime.Today, true, client.Id);
            client.AddIdentifier(clientIdentifier);
            client.SmartCardSerial = smartClient.SmartCardSerial;

            return client;
        }

        public void UpdateFrom(Client client,string code)
        {
            //  PATIENTIDENTIFICATION
            var person = client.Person;

            PATIENT_IDENTIFICATION.DATE_OF_BIRTH = person.BirthDate.ToString("yyyyMMdd");
            PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION = GetPrecision(person.BirthDateEstimated);
            PATIENT_IDENTIFICATION.SEX = person.Gender.ToUpper();
            PATIENT_IDENTIFICATION.PHONE_NUMBER = GetPhone(person);
            PATIENT_IDENTIFICATION.MARITAL_STATUS = GetMaritalStatus(client.MaritalStatus);

            PATIENT_IDENTIFICATION.PATIENT_NAME.UpdateTo(person);
            PATIENT_IDENTIFICATION.PATIENT_ADDRESS.UpdateTo(person);

            //  CARD_DETAILS
            CARD_DETAILS.LAST_UPDATED = DateTime.Today.ToString("yyyyMMdd");
            CARD_DETAILS.LAST_UPDATED_FACILITY = code;
        }

        public void CreateFrom(Client client, string code)
        {
            UpdateFrom(client,code);
            
            //PIDs
            if (client.Identifiers.Any())
            {
                var htsId = client.Identifiers.First();
                AssignHtsNumber(code, htsId.Identifier);
            }
            else
            {
                AssignHtsNumber(code);
            }
        }

        public void UpdateTesting(DateTime testDate, ObsFinalTestResult finalTestResult,string code)
        {
            //  HIV_TEST
           AppendTest(HIVTEST.Create(testDate, finalTestResult,code));
        }

        private string GetPhone(Person person)
        {
            if (person.Contacts.Any())
            {
                var phone = person.Contacts.First().Phone;
                if (phone.HasValue)
                    return phone.Value.ToString();
            }

            return string.Empty;
        }

        private string GetMaritalStatus(string clientMaritalStatus)
        {
            if (clientMaritalStatus.IsSameAs("NA"))
                return string.Empty;

            return string.Empty;
        }

        private void AppendTest(HIVTEST hivtest)
        {
            if (!HIV_TEST.Any())
            {
                HIV_TEST.Add(hivtest);
                return;
            }

            var existing = HIV_TEST.FirstOrDefault(x => x.Equals(hivtest));
            if(null==existing)
                HIV_TEST.Add(hivtest);
        }

        public static SHR Create()
        {
            var shr = new SHR();

            return shr;
        }


        private string GetPrecision(bool? estimated)
        {
            return null!=estimated && estimated.Value ? "ESTIMATED" : "EXACT";
        }


        public void AssignHtsNumber(string practiceCode)
        {
           AssignHtsNumber(practiceCode, DateTime.Now.Ticks.ToString());
        }
        public void AssignHtsNumber(string practiceCode,string number)
        {
            if (!HasHtsNumber())
            {
                // assign HTS
                var hts = INTERNALPATIENTID.Create(number, practiceCode);
                PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(hts);
            }
        }
    }
}