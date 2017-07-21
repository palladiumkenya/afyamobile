using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Engine
{
    public interface IValidationEngine
    {
        bool Validate(Response response);
    }
}