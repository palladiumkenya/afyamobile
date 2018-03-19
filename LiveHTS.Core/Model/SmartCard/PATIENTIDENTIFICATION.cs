using System.Collections.Generic;

namespace LiveHTS.Core.Model.SmartCard
{
    public class PATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }=new List<INTERNALPATIENTID>();
        public PATIENTNAME PATIENT_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public string SEX { get; set; }
        public string DEATH_DATE { get; set; }
        public string DEATH_INDICATOR { get; set; }
        public PATIENTADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public MOTHERDETAILS MOTHER_DETAILS { get; set; }
    }
}