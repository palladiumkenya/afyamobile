using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientContactAddressDTO:IPersonContact, IPersonAddress
    {
        public int? Phone { get; set; }
        public int? CountyId { get; set; }
        public string Landmark { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public Guid PersonId { get; set; }
        public bool Preferred { get; set; }

        public ClientContactAddressDTO()
        {
        }

        private ClientContactAddressDTO(int? phone, string landmark)
        {
            Phone = phone;
            Landmark = landmark;
        }

        public static ClientContactAddressDTO CreateFromView(ClientContactViewModel clientContactViewModel)
        {
            return new ClientContactAddressDTO(clientContactViewModel.Telephone,clientContactViewModel.Landmark);
        }
    }
}