using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Subject;
using SQLite;

namespace LiveHTS.Presentation.DTO
{
   public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? PracticeId { get; set; }
        public Guid PersonId { get; set; }
        public int? UserId { get; set; }
        bool Voided { get; set; }
        public ProviderDTO Provider { get; set; }
        public PersonDTO Person { get; set; }

    }
}
