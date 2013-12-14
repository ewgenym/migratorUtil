using System;
using migratorUtils.VSPackage.Models;

namespace migratorUtils.VSPackage.Infrastructure
{
    public sealed class DefaultTimer : ITimer
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}