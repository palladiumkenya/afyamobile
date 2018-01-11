using System;
using System.Threading.Tasks;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class ServerConfig:Entity<string>
    {
        public string Address { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid Instance { get; set; }

        public ServerConfig()
        {
        }

        public ServerConfig(string id)
        {
            Id = id;
        }

        private ServerConfig(string id, string address, string code, string name, Guid instance) : base(id)
        {
            Address = address;
            Code = code;
            Name = name;
            Instance = instance;
        }

        public static ServerConfig CreateCentral(Practice practice,string url)
        {
            return new ServerConfig("hapi.central", url, practice.Code, practice.Name, practice.Id);
        }
        public static ServerConfig CreateLocal(Practice practice, string url)
        {
            return new ServerConfig("hapi.local", url, practice.Code, practice.Name, practice.Id);
        }
    }
}