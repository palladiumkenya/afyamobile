using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model
{
    public class SmartClientDTO
    {
        public string Serial { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public bool BirthDateEstimated { get; set; }
        public string HtsNumber { get; set; }
        public string Facility { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        public string SmartCardSerial { get; set; }
        public string Landmark { get; set; }
        public string Phone { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        public SmartClientDTO()
        {
        }

        private SmartClientDTO(string firstName, string middleName, string lastName, string sex, DateTime birthDate,
            bool birthDateEstimated, string htsNumber, string facility, string maritalStatus, string smartCardSerial,
            string landmark, string phone)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Sex = sex;
            BirthDate = birthDate;
            BirthDateEstimated = birthDateEstimated;
            HtsNumber = htsNumber;
            Facility = facility;
            MaritalStatus = maritalStatus;
            KeyPop = "NA";
            OtherKeyPop = string.Empty;
            SmartCardSerial = smartCardSerial;
            Landmark = landmark;
            Phone = phone;
        }

        public static SmartClientDTO Create(SHR shr)
        {
            return new SmartClientDTO(shr.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                shr.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME,
                shr.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
                shr.PATIENT_IDENTIFICATION.SEX,
                GetBirthDate(shr.PATIENT_IDENTIFICATION.DATE_OF_BIRTH),
                GetBirthDateEstimated(shr.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION),
                GetHtsNumber(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetFacility(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetMaritalStatus(shr.PATIENT_IDENTIFICATION.MARITAL_STATUS),
                GetCardSerial(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID), GetLandmark(shr.PATIENT_IDENTIFICATION),
                GetPhone(shr.PATIENT_IDENTIFICATION));
        }



        private static string GetMaritalStatus(string maritalStatus)
        {
            string code = "O";

            if (string.IsNullOrWhiteSpace(maritalStatus))
                return "NA";

            if (maritalStatus.IsSameAs("Single"))
                code = "S";

            if (maritalStatus.IsSameAs("Married Monogamous"))
                code = "MM";

            if (maritalStatus.IsSameAs("Married Polygamous"))
                code = "MP";

            if (maritalStatus.IsSameAs("Divorced"))
                code = "D";

            if (maritalStatus.IsSameAs("Widowed"))
                code = "W";

            return code;
        }

        private static bool GetBirthDateEstimated(string dateOfBirthPrecision)
        {
            return !string.IsNullOrWhiteSpace(dateOfBirthPrecision) && dateOfBirthPrecision.IsSameAs("ESTIMATED");
        }

        private static DateTime GetBirthDate(string dob)
        {
            if (DateTime.TryParseExact(dob, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateValue))
                return dateValue;

            return new DateTime(1900, 1, 1);
        }

        private static string GetHtsNumber(List<INTERNALPATIENTID> internalPatientId)
        {
            var hts = internalPatientId.FirstOrDefault(x => x.IDENTIFIER_TYPE.IsSameAs("HTS_NUMBER"));

            if (null != hts)
                return hts.ID;

            return string.Empty;
        }

        private static string GetFacility(List<INTERNALPATIENTID> internalPatientId)
        {
            var hts = internalPatientId.FirstOrDefault(x => x.IDENTIFIER_TYPE.IsSameAs("HTS_NUMBER"));

            if (null != hts)
                return hts.ASSIGNING_FACILITY;

            return string.Empty;
        }

        private static string GetCardSerial(List<INTERNALPATIENTID> internalPatientId)
        {
            var serial = internalPatientId.FirstOrDefault(x => x.IDENTIFIER_TYPE.IsSameAs("CARD_SERIAL_NUMBER"));

            if (null != serial)
                return serial.ID;

            return string.Empty;
        }

        private static string GetPhone(PATIENTIDENTIFICATION shrPatientIdentification)
        {
            if (!string.IsNullOrWhiteSpace(shrPatientIdentification.PHONE_NUMBER))
            {
                return shrPatientIdentification.PHONE_NUMBER;
            }

            return null;
        }

        private static string GetLandmark(PATIENTIDENTIFICATION shrPatientIdentification)
        {
            if (null != shrPatientIdentification.PATIENT_ADDRESS)
            {
                if (null != shrPatientIdentification.PATIENT_ADDRESS.PHYSICAL_ADDRESS)
                {
                    return shrPatientIdentification.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK;
                }
            }

            return string.Empty;
        }
    }
}
