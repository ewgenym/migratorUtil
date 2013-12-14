using System;

namespace migratorUtil.MigrationWizard.Models
{
    public interface ITimer
    {
        DateTime Now { get; }
    }
}