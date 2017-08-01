using System;
using System.Collections.Generic;
using LiveHTS.Presentation.DTO;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDemographicViewModel:IStepViewModel
    {
        ClientDemographicDTO Demographic { get; set; }

        List<CustomItem> GenderOptions { get; set; }
        List<CustomItem> AgeUnitOptions { get; set; }

        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        CustomItem SelectedGender { get; set; }
        decimal Age { get; set; }
        CustomItem SelectedAgeUnit { get; set; }
        string BirthDateError { get; set; }
        DateTime? BirthDate { get; set; }

        void CalculateBirthDate();
        void CalculateAge();
    }
}