using NSubstitute;
using Xunit;
using migratorUtils.Console;
using migratorUtils.Console.Infrastructure;
using migratorUtils.Tests.Utils;

namespace migratorUtils.Tests
{
    public class SyncMigrationNumberGeneratorTests
    {
        // - user creates a new migration
        // - user deletes an existing migration (the explicit delete of the file with migration class inside should cause the Release message)
        // - offline/remote user gets online and needs to be updated with latest migrations (is offline mode detectable?)
        // - offline/remote user gets online and has local migrations to share (is offline mode detectable?)
        // - user has merged feature branch to master and renamed migrations
        // - user has switched to a master branch from a feature branch (this is often the case and migration should still be occupied)

        [Fact]
        public void should_broadcast_occupy_message_to_peers()
        {
            var synService = Substitute.For<IMigrationNumberSync>();
            var localPeer = new MigrationNumberSyncPeer(synService);
            localPeer.Start(521);
            try
            {
                // act
                Sandbox.Execute(() =>
                    {
                        var remotePeer = new MigrationNumberSyncPeer(new MigrationNumberSyncService(new EventPublisher()));
                        remotePeer.Start(125);
                        try
                        {
                            remotePeer.Channel.Occupy(new Migration {Number = "1", ProjectId = "2"});
                        }
                        finally
                        {
                            remotePeer.Stop();
                        }
                    });

                // assert
                synService.Received().Occupy(Arg.Is<Migration>(m => m.Number == "1" && m.ProjectId == "2"));
            }
            finally
            {
                localPeer.Stop();
            }
        }

        [Fact]
        public void should_broadcast_release_message_to_peers()
        {
            var synService = Substitute.For<IMigrationNumberSync>();
            var localPeer = new MigrationNumberSyncPeer(synService);
            localPeer.Start(521);
            try
            {
                // act
                Sandbox.Execute(() =>
                {
                    var remotePeer = new MigrationNumberSyncPeer(new MigrationNumberSyncService(new EventPublisher()));
                    remotePeer.Start(125);
                    try
                    {
                        remotePeer.Channel.Release(new Migration { Number = "1", ProjectId = "2" });
                    }
                    finally
                    {
                        remotePeer.Stop();
                    }
                });

                // assert
                synService.Received().Release(Arg.Is<Migration>(m => m.Number == "1" && m.ProjectId == "2"));
            }
            finally
            {
                localPeer.Stop();
            }
        }
    }
}