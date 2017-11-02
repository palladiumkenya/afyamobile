using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
    {
        string IndexClientId { get; set; }
        Client Client { get; set; }
        void LoadCache();
        void ClearCache();
    }
}