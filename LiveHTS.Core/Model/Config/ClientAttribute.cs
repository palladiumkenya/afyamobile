using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class ClientAttribute:Entity<string>
    {
        public string Name { get; set; }
    }
}