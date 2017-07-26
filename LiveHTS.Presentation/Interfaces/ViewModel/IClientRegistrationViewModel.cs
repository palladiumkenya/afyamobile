using System.Collections.Generic;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
    {
        IEnumerable<IStepViewModel> ViewModels { get; }
        void ShowStep(int step);
    }
}