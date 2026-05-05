using DecusTest_NewStructure.Rater;
using DecusTest_NewStructure.Rater.OptionalCovers;
using DecusTest_NewStructure.Rater.Processor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace DecusTest_NewStructure.DependencyInjection
{

    public static class Builder
    {
         
        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {

            var host = Host.CreateDefaultBuilder(args);

            host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(options =>
                {
                    options.TimestampFormat = "[HH:mm:ss:fff]  ";
                });

            });


            host.ConfigureServices((action, serviceCollection) =>
            {
                RegisterServices(serviceCollection);
            });


            return host;
        }

 
        public static IServiceCollection RegisterServices(IServiceCollection services)
        {
            //Covers
            services.AddTransient<IOptionalCover, OptionalCoverageA>();
            services.AddTransient<IOptionalCover, OptionalCoverageB>();
            services.AddTransient<IOptionalCover, OptionalCoverageC>();
            services.AddTransient<IOptionalCover, OptionalCoverageD>();



            // Raters
            services.AddTransient<IRater, RaterA>();
            services.AddTransient<IRater, RaterB>();
            services.AddTransient<IRaterFactory, RaterFactory>();

            services.AddTransient<IRiskDataInput, RiskDataInput>();
            services.AddTransient<IRiskDataOutput, RiskDataOutput>();

            services.AddTransient<IRiskRaterProcessor, RiskRaterProcessor>();

            return services;
        }
    }
}
