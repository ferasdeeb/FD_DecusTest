using DecusTest_NewStructure.Enums;
using DecusTest_NewStructure.Rater.OptionalCovers;
using DecusTest_NewStructure.Rater.Processor;
using Microsoft.Extensions.Logging;

namespace DecusTest_NewStructure.Rater
{
    public class RaterA(ILogger log, IRiskDataInput riskDataInput, IEnumerable<IOptionalCover> optionalCoversList) : RaterBase, IRater
    {
        public override IRiskDataOutput CalculateRate()
        {
            IRiskDataOutput result = new RiskDataOutput();
            result.Rater = this;
            result.Premium = CalculatePremium(riskDataInput);
            result.SecondaryPremium = CalculateSecondaryPremium(riskDataInput);

            return result;

        }

        public override double CalculatePremium(IRiskDataInput riskData)
        {

            double baseRate = 0.0;

            // Base rate (State based)
            if (riskData.State == StateEnum.FL)
            {
                baseRate = 1.15;
            }
            else if (riskData.State == StateEnum.TX)
            {
                baseRate = 1.05;
            }
            else
            {
                log.LogError("State not supported");
                throw new Exception("State not supported");
            }

            // Adjustments

            if (riskData.DistanceToWater < 5)
            {
                baseRate += 0.20;
            }

            // Apply base rate
            double premium = baseRate * riskData.TotalInsuredValue / 100;



            this.SetOptionalCovers();
            // Additional premiums
            foreach (var optionalCover in optionalCoversList)
            {
                if (optionalCover is OptionalCoverageA && optionalCover.CanAddToPremium() ) 
                {
                    premium += 50;
                }

                if (optionalCover is OptionalCoverageC && optionalCover.CanAddToPremium())
                {
                    switch ((((OptionalCoverageC)optionalCover).DefaultValue))
                    {
                        case CoverCOptionsEnum.Full_Value:
                            premium += riskData.TotalInsuredValue * 0.005;
                            break;
                        case CoverCOptionsEnum._10000:
                            premium += 150;
                            break;
                        case CoverCOptionsEnum._15000:
                            premium += 250;
                            break;
                        case CoverCOptionsEnum._20000:
                            premium += 400;
                            break;
                        case CoverCOptionsEnum._50000:
                            premium += 500;
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

            bool CoversAvailable = optionalCoversList.Where(c => c.Name == "OptionalCoverageA").Any(c => c.IsAvailable)
                && optionalCoversList.Where(c => c.Name == "OptionalCoverageD").Any(c => c.IsAvailable);

            double premium = 0;
            if (riskData.State == StateEnum.FL)
            {
                premium = 150;
            }
            else if (riskData.State == StateEnum.TX)
            {
                premium = 200;
            }
            else
            {
                log.LogError("State not supported");
                // throw new Exception("State not supported");
            }

            if (CoversAvailable)
            {
                premium += 50;
            }

            return premium;
        }

        public override void SetOptionalCovers()
        {
            foreach (var cover in optionalCoversList)
            {
                switch (cover)
                {
                    case OptionalCoverageA optionalCoverageA:
                        optionalCoverageA.IsAvailable = true;
                        optionalCoverageA.DefaultValue = CoverAOptionsEnum.Excluded;
                        break;

                    case OptionalCoverageC optionalCoverageC:
                        optionalCoverageC.IsAvailable = true;
                        optionalCoverageC.DefaultValue = CoverCOptionsEnum._10000;
                        break;
                    case OptionalCoverageD optionalCoverageD:
                        optionalCoverageD.IsAvailable = true;
                        optionalCoverageD.DefaultValue = CoverDOptionsEnum.NA;
                        break;
                    default:
                        cover.IsAvailable = false;
                        break;
                }
            }
        }



    }

}