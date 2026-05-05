using DecusTest_NewStructure.Rater.Processor;

namespace DecusTest_NewStructure.Rater
{
    public interface IRater
    {
        IRiskDataOutput CalculateRate();

        double CalculatePremium(IRiskDataInput riskData);

        double CalculateSecondaryPremium(IRiskDataInput riskData);

        void SetOptionalCovers();

    }
}
