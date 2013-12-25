using System;

namespace migratorUtils.MigrationWizard.Models
{
    public interface ITimer
    {
        DateTime Now { get; }
    }
}