namespace Migrations.Migrations
{
    using FluentMigrator;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [Migration(201701191830)]
    public class Migration201701191830 : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Images").AddColumn("FilePath").AsString().NotNullable();
        }
    }
}