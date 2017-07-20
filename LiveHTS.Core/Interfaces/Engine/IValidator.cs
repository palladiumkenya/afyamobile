using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Engine
{
    public interface IValidator
    {
        bool Validate(Response response);
    }
}