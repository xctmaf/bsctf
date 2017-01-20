namespace Migrations.Migrations
{
    using FluentMigrator;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [Migration(201701201322)]
    public class Migration201701201322 : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Images").AddColumn("Description").AsString(size: int.MaxValue).Nullable();
        }
    }
}