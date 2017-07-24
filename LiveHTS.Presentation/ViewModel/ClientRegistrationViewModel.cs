using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {

        public IClientDemographicViewModel Demographic { get; }
        public IClientContactViewModel Contact { get; }
        public IClientProfileViewModel Profile { get; }
        public IClientEnrollmentViewModel Enrollment { get; }

        public ClientRegistrationViewModel()
        {
            Demographic=new ClientDemographicViewModel();
            Contact=new ClientContactViewModel();
            Profile=new ClientProfileViewModel();
            Enrollment  =new ClientEnrollmentViewModel();
        }
    }
}