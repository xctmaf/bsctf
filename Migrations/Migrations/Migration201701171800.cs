namespace Migrations.Migrations
{
    using FluentMigrator;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [Migration(201701171800)]
    public class Migration201701171800 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Users").WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                  .WithColumn("Login").AsString().NotNullable()
                  .WithColumn("Password").AsString().NotNullable()
                  .WithColumn("Username").AsString().NotNullable();
        }
    }
}