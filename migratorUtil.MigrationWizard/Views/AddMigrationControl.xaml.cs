using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using migratorUtil.MigrationWizard.Infrastructure;
using migratorUtil.MigrationWizard.Models;
using migratorUtil.MigrationWizard.ViewModels;

namespace migratorUtil.MigrationWizard.Views
{
    public partial class AddMigrationControl : UserControl
    {
        public AddMigrationControl()
        {
            InitializeComponent();

            var timer = new DefaultTimer();
            var generator = new TimestampMigrationNumberGenerator(timer);
            DataContext = new MigrationViewModel(generator);
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
