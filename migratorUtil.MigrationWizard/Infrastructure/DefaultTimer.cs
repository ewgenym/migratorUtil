using System;
using migratorUtil.MigrationWizard.Models;

namespace migratorUtil.MigrationWizard.Infrastructure
{
    public sealed class DefaultTimer : ITimer
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}