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
        int? Telephone { get; set; }
        string Landmark { get; set; }
        string PersonId { get; set; }
        string ContactId { get; set; }
        string AddressId { get; set; }
        double? Lat { get; set; }
        double? Lng { get; set; }

        List<Region> Counties { get; set; }
        Region SelectedCounty { get; set; }

        List<Region> SubCounties { get; set; }
        Region SelectedSubCounty { get; set; }

        List<Region> Wards { get; set; }
        Region SelectedWard { get; set; }

        void LoadSubCounties(int postion = 0);
        void LoadSubWards(int postion = 0);
    }
}