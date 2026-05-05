using DecusTest_NewStructure.Rater.OptionalCovers;

namespace DecusTest_NewStructure.Rater.Processor
{
    public class RiskRaterProcessor : IRiskRaterProcessor
    {
        public IEnumerable<IRiskDataOutput> ProcessRiskRater(IRiskDataInput riskDataInput, IRaterFactory factory, IEnumerable<IOptionalCover> optionalCoversList) 
        {
            List<IRiskDataOutput> riskDataOutputs = new List<IRiskDataOutput>();


            IRater raterA =  factory.CreateRater(nameof(RaterA), optionalCoversList, riskDataInput);
             
            riskDataOutputs.Add(raterA.CalculateRate());

            IRater raterB = factory.CreateRater(nameof(RaterB), optionalCoversList, riskDataInput);

            riskDataOutputs.Add(raterB.CalculateRate());

            return riskDataOutputs;

        }

    

    }
}
