using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDemographicViewModel:IStepViewModel
    {
        List<CustomItem> GenderOptions { get; set; }
        List<CustomItem> AgeUnitOptions { get; set; }
        ClientDemographicDTO ClientDemographicDTO { get; set; }
        decimal Age { get; set; }
        CustomItem SelectedGender { get; set; }
        CustomItem SelectedAgeUnit { get; set; }
    }
}