using LiveHTS.Core.Model.Subject;
using SQLite;

namespace LiveHTS.Core.Model.SmartCard
{
    public class PATIENTNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public void UpdateTo(Person person)
        {
            FIRST_NAME = person.FirstName;
            MIDDLE_NAME = person.MiddleName;
            LAST_NAME = person.LastName;
        }
    }
}