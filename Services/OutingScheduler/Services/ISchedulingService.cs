using System;
using Burgerama.Services.OutingScheduler.Domain;

namespace Burgerama.Services.OutingScheduler.Services
{
    public interface ISchedulingService
    {
        ScheduledOuting ScheduleOuting(DateTime date);
    }
}