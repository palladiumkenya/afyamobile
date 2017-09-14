using System;
using System.Threading.Tasks;

namespace LiveHTS.Presentation.Interfaces
{
    public interface IDialogService
    {
        void ShowWait(string message = "Loading");
        void HideWait();
        void Alert(string message, string title = "LiveHTS", string okbtnText = "Ok");
        void ConfirmExit();
        Task<bool> ConfirmAction(string message, string title = "LiveHTS", string yesbtnText = "Yes", string nobtnText = "No");
        void ShowToast(string message);
    }
}