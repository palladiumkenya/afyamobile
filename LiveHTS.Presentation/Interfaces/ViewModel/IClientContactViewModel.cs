using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientContactViewModel : IStepViewModel
    {
        IndexClientDTO IndexClientDTO { get; set; }
        ClientContactAddressDTO ContactAddress { get; set; }
        string ClientInfo { get; set; }
        int? Telephone { get; set; }
        string Landmark { get; set; }
        string PersonId { get; set; }
        string ContactId { get; set; }
        string AddressId { get; set; }
    }
}