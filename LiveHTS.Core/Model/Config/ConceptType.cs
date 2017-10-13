using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class ConceptType:Entity<string>
    {
        public string Name { get; set; }

        public ConceptType()
        {
        }
    }
}