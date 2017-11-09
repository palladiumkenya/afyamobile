using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Model
{
    public class RemoteClientDTO
    {

        public Client Client { get; set; }
        public List<Encounter> Encounters { get; set; } = new List<Encounter>();

    }
}
