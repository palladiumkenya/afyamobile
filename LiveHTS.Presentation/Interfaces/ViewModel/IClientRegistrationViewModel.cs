using System.Collections.Generic;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
   {
        int CurrentStep { get; }
        string MoveNextLabel { get; }
        IEnumerable<IStepViewModel> ViewModels { get; }
        IMvxCommand MoveNextCommand { get; }
        IMvxCommand MovePreviousCommand { get; }
    }
}