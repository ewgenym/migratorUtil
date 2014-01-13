using System.Collections.Generic;
using NSubstitute;
using Xunit;
using migratorUtils.Console;
using migratorUtils.Console.Events;

namespace migratorUtils.Tests
{
    public class MigrationNumberSyncServiceTests
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly MigrationNumberSyncService _syncService;
        private readonly Migration _migration;
        private readonly ICallbackChannelProvider _callbackChannelProvider;
        private readonly IMigrationNumberSync _callbackService;

        public MigrationNumberSyncServiceTests()
        {
            _eventPublisher = Substitute.For<IEventPublisher>();
            _callbackChannelProvider = Substitute.For<ICallbackChannelProvider>();
            _callbackService = Substitute.For<IMigrationNumberSync>();
            _callbackChannelProvider.GetCallbackChannel<IMigrationNumberSync>().Returns(_callbackService);
            _syncService = new MigrationNumberSyncService(_eventPublisher, _callbackChannelProvider);
            _migration = new Migration { ProjectId = "1", Number = "2" };
        }

        [Fact]
        public void should_raise_occupy_event()
        {

            _syncService.Occupy(_migration);
            _eventPublisher.Received().Publish(Arg.Is<OccupyEvent>(m => m.ProjectId == _migration.ProjectId && m.Number == _migration.Number));
        }

        [Fact]
        public void should_raise_release_event()
        {
            _syncService.Release(_migration);
            _eventPublisher.Received().Publish(Arg.Is<ReleaseEvent>(m => m.ProjectId == _migration.ProjectId && m.Number == _migration.Number));
        }

        [Fact]
        public void should_raise_join_event()
        {
            _syncService.Join(new JoinRequest());
            _eventPublisher.Received().Publish(Arg.Is<JoinEvent>(r => true));
        }

        [Fact]
        public void should_raise_sync_event()
        {
            _syncService.Sync(new List<Migration> {_migration});
            _eventPublisher.Received().Publish(Arg.Is<SyncEvent>(l => l.Migrations.Count == 1 && l.Migrations.Contains(_migration)));
        }

        [Fact]
        public void should_call_sync_when_join_received()
        {
            _syncService.Join(new JoinRequest());
            _callbackService.Received().Sync(Arg.Any<List<Migration>>());
        }
    }
}