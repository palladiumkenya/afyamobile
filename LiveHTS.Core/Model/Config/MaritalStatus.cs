using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class MaritalStatus:Entity<string>
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}