using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientContactAddressDTO:IPersonContact, IPersonAddress
    {
        public string PersonId { get; set; }

        public int? Phone { get; set; }
        public int? CountyId { get; set; }
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
                    }

                    //Person Addresses


                    if (client.Person.Addresses.Any())
                    {
                        var address = client.Person.Addresses.First();
                        addressDTO.Landmark = address.Landmark;
                    }

                    addressDTO.PersonId = client.PersonId.ToString();
                }
            }

            return addressDTO;
        }
    }
}