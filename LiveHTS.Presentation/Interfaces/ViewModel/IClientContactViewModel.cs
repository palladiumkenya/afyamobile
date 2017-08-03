using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientContactViewModel : IStepViewModel
    {
        ClientContactAddressDTO ContactAddress { get; set; }
        string ClientInfo { get; set; }
        int? Telephone { get; set; }
        string Landmark { get; set; }
        string PersonId { get; set; }
    }
}