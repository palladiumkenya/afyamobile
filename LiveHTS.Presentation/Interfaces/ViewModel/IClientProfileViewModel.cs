using LiveHTS.Presentation.DTO;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientProfileViewModel : IStepViewModel
    {
        ClientDemographicDTO Demographic { get; }
        ClientContactAddressDTO ContactAddress { get; }
        string ClientInfo { get; set; }
        string KeyPop { get; set; }
    }
}