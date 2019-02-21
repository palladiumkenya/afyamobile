using System;
using LiveHTS.Presentation.Interfaces;

namespace LiveHTS.Presentation.Common
{
    public class StatusInfo
    {
        public static void Show(IDialogService dialogService, string message = "Please wait")
        {
            if (null != dialogService)
                dialogService.ShowWait(message);
        }
        public static void Close(IDialogService dialogService)
        {
            if (null != dialogService)
                dialogService.HideWait();
        }
    }
}
