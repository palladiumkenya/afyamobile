using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Model
{
   public class AppDashboard
    {
        public Module DefaultModule { get; set; }
        public User SignedInUser { get; set; }
        public Device Device { get; set; }

        public AppDashboard(Module defaultModule, User signedInUser,Device device)
        {
            DefaultModule = defaultModule;
            SignedInUser = signedInUser;
            Device = device;
        }
    }
}
