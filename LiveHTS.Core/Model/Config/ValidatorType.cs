using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class ValidatorType:Entity<string>
    {
        public string Name { get; set; }

        public ValidatorType()
        {
        }
    }
}