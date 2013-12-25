using System;

namespace migratorUtils.MigrationWizard.ViewModels
{
    public interface IRequestCloseViewModel
    {
        event EventHandler<RequestCloseEventArgs> RequestClose;
    }

    public class RequestCloseEventArgs : EventArgs
    {
        public RequestCloseEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }

        public bool? DialogResult { get; private set; }
    }
}