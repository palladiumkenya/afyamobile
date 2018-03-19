using System;
using System.Collections.Generic;
using System.Globalization;
using LiveHTS.Core.Model.SmartCard;

namespace LiveHTS.Core.Model
{
    public class HIVTestHistoryDTO
    {
        public DateTime Date { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }

        public HIVTestHistoryDTO()
        {
        }

        private HIVTestHistoryDTO(DateTime date, string result, string type)
        {
            Date = date;
            Result = result;
            Type = type;
        }

        public static List<HIVTestHistoryDTO> Create(SHR shr)
        {
            var list=new List<HIVTestHistoryDTO>();

            foreach (var hivtest in shr.HIV_TEST)
            {
                list.Add(new HIVTestHistoryDTO(GetTestDate(hivtest.DATE), hivtest.RESULT, hivtest.TYPE));
            }

            return list;
        }

        private static DateTime GetTestDate(string testDate)
        {
            if (DateTime.TryParseExact(testDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue))
                return dateValue;

            return new DateTime(1900, 1, 1);
        }
    }
}