using DecusTest_NewStructure.DependencyInjection;
using DecusTest_NewStructure.Rater.OptionalCovers;
using DecusTest_NewStructure.Rater.Processor;
using Microsoft.Extensions.DependencyInjection;

namespace DecusTest_NewStructure.Rater
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Builder.CreateDefaultBuilder(args).Build();


            IRiskDataInput dataInput = new RiskDataInput { DistanceToWater = 5, State = Enums.StateEnum.FL, TotalInsuredValue = 10000 };
            IRiskDataOutput riskDataOutput = new RiskDataOutput();

            

            var riskProcessor = host.Services.GetRequiredService<IRiskRaterProcessor>();
            var raterFactory = host.Services.GetRequiredService<IRaterFactory>();
            var covers = host.Services.GetServices<IOptionalCover>();




            riskProcessor.ProcessRiskRater( dataInput, riskDataOutput, raterFactory, covers);


        }
    }
}
