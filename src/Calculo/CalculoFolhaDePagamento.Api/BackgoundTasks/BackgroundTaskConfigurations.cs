using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoFolhaDePagamento.Api.BackgoundTasks
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
