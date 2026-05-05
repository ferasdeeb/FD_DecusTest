using DecusTest_NewStructure.Rater.OptionalCovers;
using Microsoft.Extensions.Logging;

namespace DecusTest_NewStructure.Rater.Processor
{
    public interface IRiskRaterProcessor
    {
        IEnumerable<IRiskDataOutput> ProcessRiskRater(IRiskDataInput riskDataInput, IRaterFactory factory, IEnumerable<IOptionalCover> optionalCoversList);
    }
}
