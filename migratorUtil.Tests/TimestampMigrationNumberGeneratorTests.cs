using System;
using FluentAssertions;
using NSubstitute;
using Xunit;
using migratorUtils.MigrationWizard.Models;

namespace migratorUtils.Tests
{
    public class TimestampMigrationNumberGeneratorTests
    {
        private readonly ITimer _timer;
        private readonly TimestampMigrationNumberGenerator _generator;
        private readonly DateTime _timestamp = new DateTime(2013, 12, 07, 17, 01, 25);

        public TimestampMigrationNumberGeneratorTests()
        {
            _timer = Substitute.For<ITimer>();
            _timer.Now.Returns(_timestamp);
            _generator = new TimestampMigrationNumberGenerator(_timer);
        }

        [Fact]
        public void should_generate_migration_number_by_timestamp()
        {
            var number = _generator.Generate();
            number.Should().Be("20131207170125");
        }
    }
}