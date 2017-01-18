namespace Migrations.Migrations
{
    using FluentMigrator;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [Migration(201701181759)]
    public class Migration201701181759 : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Users").AddColumn("Salt").AsString().NotNullable();
        }
    }
}