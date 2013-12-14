using System.Windows.Input;
using migratorUtils.VSPackage.Models;

namespace migratorUtils.VSPackage.ViewModels
{
    public class MigrationViewModel : ViewModel
    {
        private string _migrationName;
        private readonly IMigrationNumberGenerator _migrationNumberGenerator;

        public MigrationViewModel(IMigrationNumberGenerator migrationNumberGenerator)
        {
            _migrationNumberGenerator = migrationNumberGenerator;
            GenerateNumber();
        }

        private void GenerateNumber()
        {
            var number = _migrationNumberGenerator.Generate();
            _migrationName = string.Format("M{0}_", number);
        }

        public string MigrationName
        {
            get { return _migrationName; }
            set
            {
                if (value == _migrationName)
                {
                    return;
                }

                _migrationName = value;
                OnPropertyChanged("MigrationName");
            }
        }

        public ICommand AddMigrationCommand
        {
            get { return new DelegateCommand(AddMigration); }
        }

        private void AddMigration()
        {
        }
    }
}