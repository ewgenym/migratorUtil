using FluentAssertions;
using NSubstitute;
using Xunit;
using migratorUtil.MigrationWizard.Models;
using migratorUtil.MigrationWizard.ViewModels;

namespace migratorUtil.Tests
{
    public class MigrationViewModelTests
    {
        [Fact]
        public void should_extract_migration_number_from_its_name()
        {
            var generator = Substitute.For<IMigrationNumberGenerator>();
            var viewModel = new MigrationViewModel(generator);

            viewModel.MigrationName = "M20131207170125_AddSomeChanges";

            viewModel.MigrationNumber.Should().Be("20131207170125");
        }
    }
}