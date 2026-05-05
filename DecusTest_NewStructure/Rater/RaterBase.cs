using DecusTest_NewStructure.Rater.Processor;

namespace DecusTest_NewStructure.Rater
{
    public abstract class RaterBase : IRater
    {
        public abstract IRiskDataOutput CalculateRate();
        public abstract double CalculatePremium(IRiskDataInput riskData);
        public abstract double CalculateSecondaryPremium(IRiskDataInput riskData);

        public abstract void SetOptionalCovers();
         
    }

}
