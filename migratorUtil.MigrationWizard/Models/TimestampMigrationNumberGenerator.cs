namespace migratorUtils.MigrationWizard.Models
{
    public class TimestampMigrationNumberGenerator : IMigrationNumberGenerator
    {
        private readonly ITimer _timer;

        public TimestampMigrationNumberGenerator(ITimer timer)
        {
            _timer = timer;
        }

        public string Generate()
        {
            return _timer.Now.ToString("yyyyMMddHHmmss");
        }
    }
}