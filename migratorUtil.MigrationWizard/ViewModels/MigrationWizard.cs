using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using migratorUtil.MigrationWizard.Views;

namespace migratorUtil.MigrationWizard.ViewModels
{
    public class MigrationWizard : IWizard
    {
        private bool _dialogResult;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            var window = new AddMigrationWindow();
            _dialogResult = window.ShowDialog() ?? false;

            if (_dialogResult)
            {
                //TODO: the name of the file still "Class123.cs" as it is specified in Add->New Item->Migration dialog. Make the file name equal to migration name.
                replacementsDictionary.Add("$migname$", window.ViewModel.MigrationName);
                replacementsDictionary.Add("$mignumber$", window.ViewModel.MigrationNumber);
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return _dialogResult;
        }

        public void RunFinished()
        {
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }
    }
}