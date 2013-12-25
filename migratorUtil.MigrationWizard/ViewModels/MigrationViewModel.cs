using System.Text.RegularExpressions;
using System.Windows.Input;
using migratorUtil.MigrationWizard.Models;

namespace migratorUtil.MigrationWizard.ViewModels
{
    public class MigrationViewModel : ViewModel
    {
        private string _migrationName;
        private readonly IMigrationNumberGenerator _migrationNumberGenerator;

        public MigrationViewModel(IMigrationNumberGenerator migrationNumberGenerator, string name)
        {
            _migrationNumberGenerator = migrationNumberGenerator;
            GenerateNumber(name);
        }

        private void GenerateNumber(string name)
        {
            var number = _migrationNumberGenerator.Generate();
            _migrationName = string.Format("M{0}_{1}", number, name);
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

        public string MigrationNumber
        {
            get
            {
                var regex = new Regex(@"^M(?<number>\d+)", RegexOptions.Singleline);
                var match = regex.Match(MigrationName);
                return match.Groups["number"].Value;
            }
        }

        public ICommand AddMigrationCommand
        {
            get { return new DelegateCommand(AddMigration); }
        }

        public ICommand CancelMigrationCommand
        {
            get { return new DelegateCommand(CancelMigration); }
        }

        private void AddMigration()
        {
            OnRequestClose(true);
        }

        private void CancelMigration()
        {
            OnRequestClose(false);
        }
    }
}