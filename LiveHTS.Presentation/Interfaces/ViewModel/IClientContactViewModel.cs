using LiveHTS.Presentation.DTO;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientContactViewModel : IStepViewModel
    {
        ClientDemographicDTO Demographic { get; }
        ClientContactAddressDTO ContactAddress { get; }
        string ClientInfo { get; set; }
        int? Telephone { get; set; }
        string Landmark { get; set; }
    }
}