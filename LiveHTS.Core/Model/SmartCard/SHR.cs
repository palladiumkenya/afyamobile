using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.SmartCard
{

    public class SHR
    {
        public string VERSION { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<NEXTOFKIN> NEXT_OF_KIN { get; set; }=new List<NEXTOFKIN>();
        public List<HIVTEST> HIV_TEST { get; set; } = new List<HIVTEST>();
        public List<IMMUNIZATION> IMMUNIZATION { get; set; } = new List<IMMUNIZATION>();
        public CARDDETAILS CARD_DETAILS { get; set; }

        public Client GetCliento()
        {
            var id = LiveGuid.NewGuid();
            var clientDTO= SmartClientDTO.Create(this);
            var person=Person.Create(clientDTO.FirstName,clientDTO.LastName,clientDTO.LastName,clientDTO.Sex,clientDTO.BirthDate,null,String.Empty);

            var ids = ClientIdentifier.Create("Serial", clientDTO.HtsNumber, DateTime.Now, true, id);

        }

    }
}