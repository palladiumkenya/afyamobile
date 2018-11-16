using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientContactAddressDTO:IPersonContact, IPersonAddress
    {
        public string ContactId { get; set; }
        public string AddressId { get; set; }
        public string PersonId { get; set; }

        public int? Phone { get; set; }
        public int? CountyId { get; set; }
        public int? SubCountyId { get; set; }
        public int? WardId { get; set; }
        public string Landmark { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public bool Preferred { get; set; }

        public bool HasAnyData
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Landmark) ||
                       null!= Phone;
            }
        }

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
            var addressDTO= new ClientContactAddressDTO(clientContactViewModel.Telephone,clientContactViewModel.Landmark);
            addressDTO.PersonId = clientContactViewModel.PersonId;
            addressDTO.ContactId = clientContactViewModel.ContactId;
            addressDTO.AddressId = clientContactViewModel.AddressId;
            addressDTO.CountyId = clientContactViewModel.SelectedCounty?.Id;
            addressDTO.SubCountyId = clientContactViewModel.SelectedSubCounty?.Id;
            addressDTO.WardId = clientContactViewModel.SelectedWard?.Id;
            return addressDTO;
        }

        public static ClientContactAddressDTO CreateFromClient(Client client)
        {
            var addressDTO = new ClientContactAddressDTO();

            
            if (null != client)
            {
                if (null != client.Person)
                {
                    //Person Contacts

                    if (client.Person.Contacts.Any())
                    {
                        var contact = client.Person.Contacts.First();
                        addressDTO.Phone = contact.Phone;
                        addressDTO.ContactId = contact.Id.ToString();
                    }

                    //Person Addresses


                    if (client.Person.Addresses.Any())
                    {
                        var address = client.Person.Addresses.First();
                        addressDTO.Landmark = address.Landmark;
                        addressDTO.AddressId = address.Id.ToString();
                        addressDTO.CountyId = address.CountyId;
                        addressDTO.SubCountyId = address.SubCountyId;
                        addressDTO.WardId = address.WardId;
                    }

                    addressDTO.PersonId = client.PersonId.ToString();
                }
            }

            return addressDTO;
        }
    }
}