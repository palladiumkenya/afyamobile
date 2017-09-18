using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHTS.Presentation.DTO
{
    public class SessionDTO
    {
        public Guid UserId { get;}
        public string UserName { get; }
        public string PersonName { get; }
        public Guid PersonId { get; }
        public Guid ProviderId { get; }
        public Guid DeviceId { get; }
        public Guid PracticeId { get;  }
        public string PracticeName { get;  }
    }
}
