using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class KeyPop:Entity<string>
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public KeyPop()
        {
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public static KeyPop CreateInitial(string selectOption)
        {
            var keyPop = new KeyPop();
            keyPop.Id = "";
            keyPop.Name = selectOption;
            return keyPop;
        }
    }
}