using System;

namespace migratorUtils.VSPackage.Models
{
    public interface ITimer
    {
        DateTime Now { get; }
    }
}