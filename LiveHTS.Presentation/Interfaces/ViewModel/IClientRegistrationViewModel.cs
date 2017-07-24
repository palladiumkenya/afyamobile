using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
    {
        ClientDemographicViewModel Demographic { get; }
        ClientContactViewModel Contact { get; }
        ClientProfileViewModel Profile { get; }
        ClientEnrollmentViewModel Enrollment { get; }
    }
}