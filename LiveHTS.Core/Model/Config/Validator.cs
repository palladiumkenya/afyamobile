using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class Validator:Entity<string>
    {
        public string Name { get; set; }
        public decimal Rank { get; set; }

        public Validator()
        {
        }
    }
}