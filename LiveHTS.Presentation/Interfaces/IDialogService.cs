using System;

namespace LiveHTS.Presentation.Interfaces
{
    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);
        void ConfirmExit();
        void ConfirmAction(string message, EventHandler action);
    }
}