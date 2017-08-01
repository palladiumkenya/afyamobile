using System.Collections.Generic;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
    {        
        IEnumerable<IStepViewModel> ViewModels { get; }
        void ShowStep(int step);
    }
}