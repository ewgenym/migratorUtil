using System.Windows;
using migratorUtil.MigrationWizard.ViewModels;

namespace migratorUtil.MigrationWizard.Views
{
    /// <summary>
    /// Interaction logic for AddMigrationWindow.xaml
    /// </summary>
    public partial class AddMigrationWindow : Window
    {
        public AddMigrationWindow()
        {
            InitializeComponent();
        }

        public MigrationViewModel ViewModel
        {
            get { return addMigrationControl.DataContext as MigrationViewModel; }
        }
    }
}
