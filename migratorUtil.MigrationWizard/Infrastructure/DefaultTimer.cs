using System;
using migratorUtils.MigrationWizard.Models;

namespace migratorUtils.MigrationWizard.Infrastructure
{
    public sealed class DefaultTimer : ITimer
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}