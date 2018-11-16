using LiveHTS.Core.Model.Subject;
using SQLite;

namespace LiveHTS.Core.Model.SmartCard
{
    public class PATIENTNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public PATIENTNAME()
        {
            FIRST_NAME = MIDDLE_NAME = LAST_NAME = string.Empty;
        }

        public static PATIENTNAME Create()
        {
            return new PATIENTNAME();
        }

        public void UpdateTo(Person person)
        {
            FIRST_NAME = person.FirstName;
            MIDDLE_NAME = person.MiddleName;
            LAST_NAME = person.LastName;
        }
    }
}