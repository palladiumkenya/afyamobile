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
        CustomList CustomList { get; }
        ClientDemographicDTO ClientDemographicDTO { get; set; }
    }
}