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
    }
}