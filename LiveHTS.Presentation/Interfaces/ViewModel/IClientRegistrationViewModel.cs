using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
    {
        Client Client { get; set; }
        void LoadCache();
        void ClearCache();
    }
}