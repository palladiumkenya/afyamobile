using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientContactViewModel : IStepViewModel
    {
        int? Telephone { get; set; }
    }
}