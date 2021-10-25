using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaClub.Services;
using PizzaClub.Utils;
using System.Threading.Tasks;

namespace PizzaClub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            /**
            var dbInitTask = host.Services.GetService<IDbInitTask>();
            var _schedules = await dbInitTask.Execute();
            var scheduleService = host.Services.GetService<IScheduleService>();
            foreach (var schedule in _schedules)
            {
                await scheduleService.AddSchedule(schedule);
            }
            */
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
