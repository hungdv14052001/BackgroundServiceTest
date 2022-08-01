using BackgroundServiceTest.DBContex;
using BackgroundServiceTest.IBackgroundService;
using BackgroundServiceTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundServiceTest.Services
{
    public class InterestService : IInterest
    {
        private readonly ILogger _logger;

        public InterestService(ILogger<InterestService> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// interest rate update
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public async Task Dowork(CancellationToken cancellationToken, DatabaseContext provider)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var accountList = provider.Accounts.ToList();
                foreach(Account a in accountList)
                {
                    a.Surplus += a.Surplus * 10 / 100;
                    provider.Entry(a).State = EntityState.Modified;
                }
                provider.SaveChanges();
                _logger.LogInformation(
                    "Wait 5 mins to update surplus: {accountList}", accountList.Count());
                await Task.Delay(60000, cancellationToken);
            }
        }
    }
}
