using BackgroundServiceTest.DBContex;
using BackgroundServiceTest.IBackgroundService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundServiceTest.BackgroundService
{
    public class UpdateSurPlus : IHostedService
    {
        private readonly IInterest interest;
        private readonly ILogger logger;
        private readonly IServiceProvider _provider;

        public UpdateSurPlus(ILogger<UpdateSurPlus> logger, IServiceProvider provider, IInterest interest)
        {
            this.interest = interest;
            this.logger = logger;
            this._provider = provider;
        }

        /// <summary>
        /// Start Caculate interest
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                using (var scope = _provider.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    await interest.Dowork(cancellationToken, dbcontext);
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop Caculate interest
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Service has stopped");
            return Task.CompletedTask;
        }
    }
}
