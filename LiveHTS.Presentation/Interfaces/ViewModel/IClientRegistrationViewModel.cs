namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRegistrationViewModel
    {
        IClientDemographicViewModel Demographic { get; }
        IClientContactViewModel Contact { get; }
        IClientProfileViewModel Profile { get; }
        IClientEnrollmentViewModel Enrollment { get; }
    }
}