using System;
using Autofac;
using Burgerama.Services.OutingScheduler.Data.Rest;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using Burgerama.Services.OutingScheduler.Services;

namespace Burgerama.Services.OutingScheduler.App
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var container = GetAutofacContainer();
            var scheduler = container.Resolve<SchedulingService>();
            var outing = scheduler.ScheduleOuting();

            if (outing == null)
            {
                Console.WriteLine("No outing was scheduled.");
            }
            else
            {
                Console.WriteLine("Outing '{0}' scheduled for '{1}' on '{2}'.", outing.Id, outing.Venue.Title, outing.Date);
            }
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<SchedulingService>();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            return builder.Build();
        }
    }
}
