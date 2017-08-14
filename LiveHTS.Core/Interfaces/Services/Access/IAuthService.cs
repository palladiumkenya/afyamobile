using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Access
{
    public interface IAuthService
    {
        Provider GetDefaultProvider();
        User SignIn(string username,string password);
    }
}