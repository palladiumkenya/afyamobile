using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEnrollmentViewModel : IStepViewModel
    {
        string Serial { get; set; }
    }
}