using DecusTest_NewStructure.Rater.OptionalCovers;
using Microsoft.Extensions.Logging;

namespace DecusTest_NewStructure.Rater.Processor
{
    public interface IRiskRaterProcessor
    {
        void ProcessRiskRater(IRiskDataInput riskDataInput, IRiskDataOutput riskDataOutput, IRaterFactory factory, IEnumerable<IOptionalCover> optionalCoversList);
    }
}
