using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {

        public ClientDemographicViewModel Demographic { get; }
        public ClientContactViewModel Contact { get; }
        public ClientProfileViewModel Profile { get; }
        public ClientEnrollmentViewModel Enrollment { get; }

        public ClientRegistrationViewModel()
        {
            Demographic=new ClientDemographicViewModel();
            Contact=new ClientContactViewModel();
            Profile=new ClientProfileViewModel();
            Enrollment  =new ClientEnrollmentViewModel();
        }
    }
}