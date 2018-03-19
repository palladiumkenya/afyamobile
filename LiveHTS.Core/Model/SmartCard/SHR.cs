using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public SmartClientDTO GetSmartClientDto()
        {
            return SmartClientDTO.Create(this);
        }
    }
}