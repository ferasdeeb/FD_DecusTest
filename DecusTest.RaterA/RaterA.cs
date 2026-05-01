using DecusTest.Business;
using DecusTest.Business.Models;
using System;
using System.Collections.Generic;

namespace DecusTest.RaterA
{
    public class RaterA : Rater
    {
        public override int Id => 1;
        public override string Name => "Rater A";

        public RaterA(RiskData riskData) : base(riskData)
        {
        }

        public override RatingResults CalculateRate(List<RatingOption> selectedOptions)
        {
            var results = new RatingResults();

            results.RatingOptions = GetOptions(_riskData, selectedOptions);

            results.Premium = CalculatePremium(_riskData);
            results.SecondaryPremium = CalculateSecondaryPremium(_riskData);

            return results;
        }

        private double CalculatePremium(RiskData riskData)
        {
            double? baseRate;

            // Base rate (State based)
            if (riskData.State == "FL")
            {
                baseRate = 1.15;
            }
            else if (riskData.State == "TX")
            {
                baseRate = 1.05;
            }
            else
            {
                throw new Exception("State not supported");
            }

            // Adjustments

            if (riskData.DistanceToWater < 5)
            {
                baseRate += 0.20;
            }

            // Apply base rate
            double premium = baseRate.Value * riskData.TotalInsuredValue / 100;

            // Additional premiums

            if (riskData.OptionalCoverageA == true)
            {
                premium += 50;
            }

            if (riskData.OptionalCoverageC == "Full Value")
            {
                premium += riskData.TotalInsuredValue * 0.005;
            }
            else if (double.TryParse(riskData.OptionalCoverageC, out double optionalCoverageCValue))
            {
                switch (optionalCoverageCValue)
                {
                    case 10000:
                        premium += 150;
                        break;
                    case 15000:
                        premium += 250;
                        break;
                    case 20000:
                        premium += 400;
                        break;
                    case 50000:
                        premium += 500;
                        break;
                }
            }

            return Math.Round(premium);
        }

        private double CalculateSecondaryPremium(RiskData riskData)
        {
            double premium;
            if (riskData.State == "FL")
            {
                premium = 150;
            }
            else if (riskData.State == "TX")
            {
                premium = 200;
            }
            else
            {
                throw new Exception("State not supported");
            }

            return premium;
        }

        protected override List<RatingOption> GetOptions(RiskData riskData, List<RatingOption> selectedOptions)
        {
            List<RatingOption> ratingOptions = new List<RatingOption>();

            //Optional Coverage A
            ratingOptions.Add(CreateOptionalCoverageA(riskData, selectedOptions));

            //Optional Coverage C
            ratingOptions.Add(CreateOptionalCoverageC(riskData, selectedOptions));

            return ratingOptions;
        }

        private RatingOption CreateOptionalCoverageA(RiskData riskData, List<RatingOption> selectedOptions)
        {
            bool isCoverageAvailable = true;
            List<object> coverageOptions = new List<object> { false, true };
            object coverageDefaultValue = false;

            if (RiskOptions.TryGetRiskOption("OptionalCoverageA", out RiskOption coverageRiskOption) == false)
            {
                throw new Exception("Optional Coverage A option not configured");
            }

            return CreateOption(riskData, coverageRiskOption, "OptionalCoverageA", "Optional Coverage A", coverageOptions, coverageDefaultValue, selectedOptions, isCoverageAvailable);
        }

        private RatingOption CreateOptionalCoverageC(RiskData riskData, List<RatingOption> selectedOptions)
        {
            bool isCoverageAvailable = true;
            List<object> coverageOptions = new List<object> { "Full Value", 10000, 15000, 20000, 50000 };
            object coverageDefaultValue = 10000;

            if (RiskOptions.TryGetRiskOption("OptionalCoverageC", out RiskOption coverageRiskOption) == false)
            {
                throw new Exception("Optional Coverage C option not configured");
            }

            return CreateOption(riskData, coverageRiskOption, "OptionalCoverageC", "Optional Coverage C", coverageOptions, coverageDefaultValue, selectedOptions, isCoverageAvailable);
        }
    }
}
