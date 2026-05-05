using DecusTest_NewStructure.Rater.OptionalCovers;

namespace DecusTest_NewStructure.Rater.Processor
{
    public class RiskRaterProcessor : IRiskRaterProcessor
    {
        public void ProcessRiskRater(IRiskDataInput riskDataInput, IRiskDataOutput riskDataOutput, IRaterFactory factory, IEnumerable<IOptionalCover> optionalCoversList) 
        {

            IRater raterA =  factory.CreateRater(nameof(RaterA), optionalCoversList);

            IRater raterB = factory.CreateRater(nameof(RaterB), optionalCoversList);

            

        }

    

    }
}
