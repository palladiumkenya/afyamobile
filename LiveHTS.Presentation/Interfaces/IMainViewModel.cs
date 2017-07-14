using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Interfaces
{
    public interface IMainViewModel
    {
        Module Module { get; set; }
        Form SelectedForm { get; set; }
    }
}