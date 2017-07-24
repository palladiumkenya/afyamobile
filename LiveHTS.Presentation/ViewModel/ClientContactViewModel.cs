using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientContactViewModel : MvxViewModel,IClientContactViewModel
    {
        public string Title { get; set; }
    }
}