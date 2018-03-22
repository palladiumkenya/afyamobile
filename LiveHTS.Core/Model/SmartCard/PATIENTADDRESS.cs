using System.Linq;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Model.SmartCard
{
    public class PATIENTADDRESS
    {
        public PHYSICALADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }

        public PATIENTADDRESS()
        {
            PHYSICAL_ADDRESS=new PHYSICALADDRESS();
        }

        public static PATIENTADDRESS Create()
        {
            return new PATIENTADDRESS();
        }

        public void UpdateTo(Person person)
        {
            if (person.Addresses.Any())
            {
                var landmark = person.Addresses.First().Landmark;
                if (null != PHYSICAL_ADDRESS)
                    PHYSICAL_ADDRESS.UpdateTo(landmark);
            }
        }
    }
}