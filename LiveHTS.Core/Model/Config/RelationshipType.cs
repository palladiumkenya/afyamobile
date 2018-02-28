using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class RelationshipType:Entity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public RelationshipType()
        {
        }

        public static bool IsPartner(string relation)
        {
            return relation.IsSameAs("Partner") ||
                   relation.IsSameAs("Cowife")||
                   relation.IsSameAs("Spouse");
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}