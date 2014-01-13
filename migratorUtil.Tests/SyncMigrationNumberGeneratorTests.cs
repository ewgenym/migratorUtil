using System.Threading;
using NSubstitute;
using NUnit.Framework;
using Xunit;
using migratorUtils.Console;
using migratorUtils.Console.Infrastructure;
using migratorUtils.Tests.Utils;

namespace migratorUtils.Tests
{
    [TestFixture]
    public class SyncMigrationNumberGeneratorTests
    {
        // - offline/remote user gets online and needs to be updated with latest migrations (is offline mode detectable?)
        // - offline/remote user gets online and has local migrations to share (is offline mode detectable?)
        // - user has merged feature branch to master and renamed migrations
        // - user has switched to a master branch from a feature branch (this is often the case and migration should still be occupied)
        [Test]
        public void should_broadcast_occupy_message_to_peers()
        {
            var evt = new ManualResetEvent(false);
            //var synService = Substitute.For<IMigrationNumberSync>();
            //var publisher = Substitute.For<IEventPublisher>();
            var synService = Substitute.For<IMigrationNumberSync>();
            synService.WhenForAnyArgs(x => x.Occupy(new Migration())).Do(c => evt.Set());

                //new MigrationNumberSyncService(publisher);
            var localPeer = new MigrationNumberSyncPeer(synService);
            localPeer.Start(521);
            try
            {
                // act
                Sandbox.Execute(() =>
                    {
                        var remotePeer =
                            new MigrationNumberSyncPeer(new MigrationNumberSyncService(new EventPublisher(), new CallbackChannelProvider()));
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
                evt.WaitOne(400000, false);
                //publisher.Received().Publish(Arg.Is<OccupyEvent>(e => e.Number == "1" && e.ProjectId == "1"));
                synService.ReceivedWithAnyArgs().Occupy(Arg.Is<Migration>(m => m.Number == "1" && m.ProjectId == "2"));
            }
            finally
            {
                localPeer.Stop();
            }
        }

        //[Fact]
        //public void should_broadcast_release_message_to_peers()
        //{
        //    var synService = Substitute.For<IMigrationNumberSync>();
        //    var localPeer = new MigrationNumberSyncPeer(synService);
        //    localPeer.Start(521);
        //    try
        //    {
        //        // act
        //        Sandbox.Execute(() =>
        //        {
        //            var remotePeer = new MigrationNumberSyncPeer(new MigrationNumberSyncService(new EventPublisher()));
        //            remotePeer.Start(125);
        //            try
        //            {
        //                remotePeer.Channel.Release(new Migration { Number = "1", ProjectId = "2" });
        //            }
        //            finally
        //            {
        //                remotePeer.Stop();
        //            }
        //        });

        //        // assert
        //        synService.Received().Release(Arg.Is<Migration>(m => m.Number == "1" && m.ProjectId == "2"));
        //    }
        //    finally
        //    {
        //        localPeer.Stop();
        //    }
        //}

        //[Fact]
        //public void should_broadcast_join_message_to_neighbour_peers()
        //{
        //    var synService = Substitute.For<IMigrationNumberSync>();
        //    var localPeer = new MigrationNumberSyncPeer(synService);
        //    localPeer.Start(521);
        //    try
        //    {
        //        // act
        //        Sandbox.Execute(() =>
        //        {
        //            var remotePeer = new MigrationNumberSyncPeer(new MigrationNumberSyncService(new EventPublisher()));
        //            remotePeer.Start(125);
        //            try
        //            {
        //                remotePeer.Channel.Join(new JoinRequest());
        //            }
        //            finally
        //            {
        //                remotePeer.Stop();
        //            }
        //        });

        //        // assert
        //        synService.ReceivedWithAnyArgs().Join(null);
        //    }
        //    finally
        //    {
        //        localPeer.Stop();
        //    }
        //}

        [Fact]
        public void should_not_receive_own_messages()
        {
        }
    }
}