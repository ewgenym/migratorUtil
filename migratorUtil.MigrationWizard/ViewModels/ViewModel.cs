using System;
using System.ComponentModel;

namespace migratorUtils.MigrationWizard.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, IRequestCloseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnRequestClose(bool? dialogResult)
        {
            var requestClose = RequestClose;
            if (requestClose != null)
            {
                requestClose(this, new RequestCloseEventArgs(dialogResult));
            }
        }

        public event EventHandler<RequestCloseEventArgs> RequestClose;
    }
}