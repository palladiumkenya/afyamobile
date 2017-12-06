using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class MaritalStatus:Entity<string>
    {
        public string Name { get; set; }

        public MaritalStatus()
        {
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public static MaritalStatus CreateInitial(string selectOption)
        {
            var maritalStatus = new MaritalStatus();

            maritalStatus.Id = "";
            maritalStatus.Name = selectOption;

            return maritalStatus;
        }
    }
}