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
        public string HtsNumber { get; set; }
        public string Facility { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        public SmartClientDTO()
        {
        }

        private SmartClientDTO(string firstName, string middleName, string lastName, string sex, DateTime birthDate, string htsNumber, string facility)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Sex = sex;
            BirthDate = birthDate;
            HtsNumber = htsNumber;
            Facility = facility;
        }

        public static SmartClientDTO Create(SHR shr)
        {
            return new SmartClientDTO(shr.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME,
                shr.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME,
                shr.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME,
                shr.PATIENT_IDENTIFICATION.SEX,
                GetBirthDate(shr.PATIENT_IDENTIFICATION.DATE_OF_BIRTH),
                GetHtsNumber(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID),
                GetFacility(shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID));
        }

        private static DateTime GetBirthDate(string dob)
        {
            if (DateTime.TryParseExact(dob,"yyyyMMdd",CultureInfo.InvariantCulture, DateTimeStyles.None,out var dateValue))
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

    }
}