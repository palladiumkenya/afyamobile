using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDemographicViewModel:IStepViewModel
    {
        string Names { get; set; }
    }
}