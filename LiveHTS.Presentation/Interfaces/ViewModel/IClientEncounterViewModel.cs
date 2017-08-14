using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEncounterViewModel
    {
        ClientDTO ClientDTO { get; set; }
        Form Form { get; set; }
        Encounter Encounter { get; set; }
    }
}