using BackgroundServiceTest.DBContex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundServiceTest.IBackgroundService
{
    public interface IInterest
    {
        Task Dowork(CancellationToken cancellationToken, DatabaseContext provider);
    }
}
