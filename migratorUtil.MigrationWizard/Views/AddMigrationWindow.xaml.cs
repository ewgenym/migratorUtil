using System.Windows;
using migratorUtils.MigrationWizard.Infrastructure;
using migratorUtils.MigrationWizard.Models;
using migratorUtils.MigrationWizard.ViewModels;

namespace migratorUtils.MigrationWizard.Views
{
    public partial class AddMigrationWindow : Window
    {
        public AddMigrationWindow(string migrationName)
        {
            DataContextChanged += addMigrationControl_DataContextChanged;

            InitializeComponent();

            var timer = new DefaultTimer();
            var generator = new TimestampMigrationNumberGenerator(timer);
            DataContext = new MigrationViewModel(generator, migrationName);
        }

        void addMigrationControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var requestCloseViewModel = e.NewValue as IRequestCloseViewModel;
            if (requestCloseViewModel != null)
            {
                requestCloseViewModel.RequestClose += (s, ev) =>
                    {
                        DialogResult = ev.DialogResult;
                        Close();
                    };
            }
        }

        public MigrationViewModel ViewModel
        {
            get { return DataContext as MigrationViewModel; }
        }
    }
}
