using DecusTest_NewStructure.Enums;
using DecusTest_NewStructure.Rater.OptionalCovers;
using DecusTest_NewStructure.Rater.Processor;
using Microsoft.Extensions.Logging;

namespace DecusTest_NewStructure.Rater
{
    public class RaterB(ILogger log, IRiskDataInput riskDataInput, IEnumerable<IOptionalCover> optionalCoversList) : RaterBase, IRater
    {
        public override IRiskDataOutput CalculateRate()
        {
            IRiskDataOutput result = new RiskDataOutput();
            result.Premium = CalculatePremium(riskDataInput);
             
            return result;
        }
        public override double CalculatePremium(IRiskDataInput riskData)
        {
            double baseRate = 0;
            double premium = 0;

            // Base rate (State based)
            if (riskData.State == StateEnum.FL)
            {
                baseRate = 1.20;
            }
            else if (riskData.State == StateEnum.TX)
            {
                baseRate = 1.08;
            }
            else
            {
                log.LogError("State not supported");
                throw new Exception("State not supported");
            }

            // Adjustments
            if (riskData.DistanceToWater < 10)
            {
                baseRate += 0.30;
            }

            // Additional premiums
            foreach (var optionalCover in optionalCoversList)
            {
                if (optionalCover is OptionalCoverageA)
                {
                    baseRate *= 1.5;
                    premium = baseRate * riskData.TotalInsuredValue / 100; // Note here the order of the optional cover matters, as the names are alphabetically orderer we are fine in this instance
                }


                if (optionalCover is OptionalCoverageB)
                {
                    switch ((((OptionalCoverageB)optionalCover).DefaultValue))
                    {
                        case CoverBOptionsEnum._10:
                            premium += 50;
                            break;
                        case CoverBOptionsEnum._15:
                            premium += 150;
                            break;
                        case CoverBOptionsEnum._20:
                            premium += 200;
                            break;
                        default:
                            break;
                    }

                }
            }

            return Math.Round(premium);
        }
        public override double CalculateSecondaryPremium(IRiskDataInput riskData)
        {
            log.LogInformation("Secondary premium calculation not applicable for RaterB");
            return 0;
        }
        public override void SetOptionalCovers()
        {
            foreach (var cover in optionalCoversList)
            {
                switch (cover)
                {
                    case OptionalCoverageA optionalCoverageA:
                        optionalCoverageA.IsAvailable = true;
                        optionalCoverageA.DefaultValue = CoverAOptionsEnum.Included;
                        break;

                    case OptionalCoverageB optionalCoverageB:
                        optionalCoverageB.IsAvailable = true;
                        optionalCoverageB.DefaultValue = CoverBOptionsEnum._10;
                        break;
                    default:
                        cover.IsAvailable = false;
                        break;
                }
            }
        }
    }

}
