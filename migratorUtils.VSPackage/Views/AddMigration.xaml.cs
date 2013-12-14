using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using migratorUtils.VSPackage.Infrastructure;
using migratorUtils.VSPackage.Models;
using migratorUtils.VSPackage.ViewModels;

namespace migratorUtils.VSPackage.Views
{
    public partial class AddMigration : UserControl
    {
        public AddMigration()
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
