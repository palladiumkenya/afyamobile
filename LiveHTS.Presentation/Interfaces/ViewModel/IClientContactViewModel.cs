using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Meta;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientContactViewModel : IStepViewModel
    {
        IndexClientDTO IndexClientDTO { get; set; }
        ClientContactAddressDTO ContactAddress { get; set; }
        string ClientInfo { get; set; }
        long? Telephone { get; set; }
        string Landmark { get; set; }
        string PersonId { get; set; }
        string ContactId { get; set; }
        string AddressId { get; set; }
        double? Lat { get; set; }
        double? Lng { get; set; }

        List<RegionItem> Counties { get; set; }
        RegionItem SelectedCounty { get; set; }

        List<RegionItem> SubCounties { get; set; }
        RegionItem SelectedSubCounty { get; set; }

        List<RegionItem> Wards { get; set; }
        RegionItem SelectedWard { get; set; }
    }
}
