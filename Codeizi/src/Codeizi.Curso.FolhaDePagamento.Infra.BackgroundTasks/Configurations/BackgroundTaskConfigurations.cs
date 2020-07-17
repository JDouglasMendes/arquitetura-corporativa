namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.BackgroundTasks.Configurations
{
    public class BackgroundTaskConfigurations
    {
        public string ConnectionString { get; set; }

        public string EventBusConnection { get; set; }

        public int GracePeriodTime { get; set; }

        public int CheckUpdateTime { get; set; }

        public string SubscriptionClientName { get; set; }
    }
}