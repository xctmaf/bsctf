namespace Migrations
{
    using System.Configuration;
    using System.Text;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;

    public static class MigrationsRunner
    {
        public static string Run()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

            var migrationLog = new StringBuilder();
            var announcer = new TextWriterAnnouncer(s => migrationLog.Append(s));
            var runnerContext = new RunnerContext(announcer)
                                    {
                                        Database = "postgres",
                                        Connection = connectionString,
                                        Targets = new[] {typeof (MigrationsRunner).Assembly.FullName}
                                    };

            
            var taskExecutor = new TaskExecutor(runnerContext);
            taskExecutor.Execute();

            return migrationLog.ToString();
        }
    }
}