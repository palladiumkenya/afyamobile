using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Model.SmartCard
{
    public class PATIENTIDENTIFICATION
    {
        public string DATE_OF_BIRTH { get; set; }
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public string SEX { get; set; }
        public string DEATH_DATE { get; set; }
        public string DEATH_INDICATOR { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MARITAL_STATUS { get; set; }

        public PATIENTNAME PATIENT_NAME { get; set; }
        public PATIENTADDRESS PATIENT_ADDRESS { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }=new List<INTERNALPATIENTID>();
       
        public MOTHERDETAILS MOTHER_DETAILS { get; set; }
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }

        public PATIENTIDENTIFICATION()
        {
        }

        private PATIENTIDENTIFICATION(PATIENTNAME patientName, string dateOfBirth, string dateOfBirthPrecision, string sex, PATIENTADDRESS patientAddress, string phoneNumber, string maritalStatus, INTERNALPATIENTID internalpatientid)
        {
            PATIENT_NAME = patientName;
            DATE_OF_BIRTH = dateOfBirth;
            DATE_OF_BIRTH_PRECISION = dateOfBirthPrecision;
            SEX = sex;
            PATIENT_ADDRESS = patientAddress;
            PHONE_NUMBER = phoneNumber;
            MARITAL_STATUS = maritalStatus;
            AddId(internalpatientid);
        }

        public static PATIENTIDENTIFICATION Create(PATIENTNAME patientName, string dateOfBirth, string dateOfBirthPrecision, string sex, PATIENTADDRESS patientAddress, string phoneNumber, string maritalStatus, INTERNALPATIENTID internalpatientid)
        {
            return new PATIENTIDENTIFICATION(patientName,dateOfBirth,dateOfBirthPrecision,sex,patientAddress,phoneNumber,maritalStatus,internalpatientid);
        }

       

       

        private void AddId(INTERNALPATIENTID internalpatientid)
        {
            var existing = INTERNAL_PATIENT_ID.FirstOrDefault(x => x.IDENTIFIER_TYPE == internalpatientid.IDENTIFIER_TYPE);
            if (null != existing)
                INTERNAL_PATIENT_ID.Remove(existing);

            INTERNAL_PATIENT_ID.Add(internalpatientid);
        }
    }
}