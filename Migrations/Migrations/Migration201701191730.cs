namespace Migrations.Migrations
{
    using FluentMigrator;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [Migration(201701191730)]
    public class Migration201701191730 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Images").WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                  .WithColumn("FileName").AsString().NotNullable()
                  .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").NotNullable();
        }
    }
}