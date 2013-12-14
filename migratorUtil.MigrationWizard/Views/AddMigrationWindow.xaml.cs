﻿using System.Windows;
using migratorUtil.MigrationWizard.Infrastructure;
using migratorUtil.MigrationWizard.Models;
using migratorUtil.MigrationWizard.ViewModels;

namespace migratorUtil.MigrationWizard.Views
{
    public partial class AddMigrationWindow : Window
    {
        public AddMigrationWindow()
        {
            DataContextChanged += addMigrationControl_DataContextChanged;

            InitializeComponent();

            var timer = new DefaultTimer();
            var generator = new TimestampMigrationNumberGenerator(timer);
            DataContext = new MigrationViewModel(generator);
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
