using AirportDistanceCalc.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AirportDistanceCalc.Tests.Config
{
    public class AirportDistanceCalcFactory<TStartup> : WebApplicationFactory<Program> where TStartup : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<MemoryContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<MemoryContext>(options =>
                {
                    options.UseInMemoryDatabase($"InMemoryDbTests");
                });

                var sp = services.BuildServiceProvider();
                Utilities.SetDbContext(sp.GetRequiredService<MemoryContext>());
            });

        }
    }
}
