using System.Windows.Input;

namespace LiveHTS.Presentation.Interfaces
{
    public interface IObsViewModel
    {
        string Form { get; set; }
        bool CanStart { get; set; }
        bool CanStop { get; set; }
        ICommand StartCommand { get;  }
        ICommand StopCommand { get;  }

        string CurrentAction { get; set; }
    }
}