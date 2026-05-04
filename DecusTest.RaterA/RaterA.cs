using DecusTest.Business;
using DecusTest.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

            //  Note for Interview: The call to GetOptions is not strictly necessry as the options already exist in the selectedOptions list.
            //  The method ValidateSelectedOptions has already been called prior to Calculate Rate and it ensures that the selected options are valid and available for the given risk data.
            //   I might be missing some context of why this call is needed. But wasnted to leave a note that line 22 can be removed, and replaced with more efficient code (below) that just filters the selectedOptions
            //   list to only include available options

            //      results.RatingOptions = selectedOptions.Where(o => o.Available).ToList();

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

            if (riskData.OptionalCoverageA == true  && riskData.OptionalCoverageD == "True")
            {
                premium += 50;
            }

            return premium;
        }

        protected override List<RatingOption> GetOptions(RiskData riskData, List<RatingOption> selectedOptions)
        {

            bool IsNotNull(RatingOption option) => option != null;
            bool IsAvailable(RatingOption option) => option.Available;
            bool IsSelectedValueTrue(RatingOption option) => option.SelectedValue is bool && Convert.ToBoolean(option.SelectedValue) == true;
            bool IsSelectedValueIncluded(RatingOption option) => option.SelectedValue is string && option.SelectedValue.ToString() == "Included";


            List<RatingOption> ratingOptions = new List<RatingOption>();

            //Optional Coverage A
            ratingOptions.Add(CreateOptionalCoverageA(riskData, selectedOptions));
            
            var optionA = ratingOptions.FirstOrDefault(o => o.Name == "OptionalCoverageA");
            bool enableOptionalCoverageD = false;

            if (IsNotNull(optionA) && IsAvailable(optionA) 
                && (IsSelectedValueTrue(optionA) || IsSelectedValueIncluded(optionA)))
                enableOptionalCoverageD = true;

            //Optional Coverage C
            ratingOptions.Add(CreateOptionalCoverageC(riskData, selectedOptions));

            //Optional Coverage D
            ratingOptions.Add(CreateOptionalCoverageD(riskData, selectedOptions, enableOptionalCoverageD));

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

        private RatingOption CreateOptionalCoverageD(RiskData riskData, List<RatingOption> selectedOptions,bool isEnabled)
        {
            bool isCoverageAvailable = true;
            object coverageDefaultValue = null; 

            List<object> coverageOptions = new List<object> { "Not Applicable (N/A)" };  // Note for interview: if this is set to null, the same bug of the first excersise will show again here.
            
            if (isEnabled)
            {
                coverageOptions.Clear();
                coverageOptions.Add(true);
                coverageOptions.Add(false);
                coverageDefaultValue = true;
            }

            if (RiskOptions.TryGetRiskOption("OptionalCoverageD", out RiskOption coverageRiskOption) == false)
            {
                throw new Exception("Optional Coverage D option not configured");
            }

            return CreateOption(riskData, coverageRiskOption, "OptionalCoverageD", "Optional Coverage D", coverageOptions, coverageDefaultValue, selectedOptions, isCoverageAvailable);
        }
    }
}
