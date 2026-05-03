using DecusTest.Business;
using DecusTest.Business.Models;
using System;
using System.Collections.Generic;

namespace DecusTest.RaterB
{
    public class RaterB : Rater
    {
        public override int Id => 2;
        public override string Name => "Rater B";

        public RaterB(RiskData riskData) : base(riskData)
        {
        }

        public override RatingResults CalculateRate(List<RatingOption> selectedOptions)
        {
            var results = new RatingResults();

            results.RatingOptions = GetOptions(_riskData, selectedOptions);

            results.Premium = CalculatePremium(_riskData);

            return results;
        }

        private double CalculatePremium(RiskData riskData)
        {
            double? baseRate;

            // Base rate (State based)
            if (riskData.State == "FL")
            {
                baseRate = 1.20;
            }
            else if (riskData.State == "TX")
            {
                baseRate = 1.08;
            }
            else
            {
                throw new Exception("State not supported");
            }

            // Adjustments
            if (riskData.DistanceToWater < 10)
            {
                baseRate += 0.30;
            }

            if (riskData.OptionalCoverageA == true)
            {
                baseRate *= 1.5;
            }

            // Apply base rate
            double premium = baseRate.Value * riskData.TotalInsuredValue / 100;

            // Additional premiums
            switch (riskData.OptionalCoverageB)
            {
                case 10:
                    premium += 50;
                    break;
                case 15:
                    premium += 150;
                    break;
                case 20:
                    premium += 200;
                    break;
            }
            
            return Math.Round(premium);
        }

        protected override List<RatingOption> GetOptions(RiskData riskData, List<RatingOption> selectedOptions)
        {
            List<RatingOption> ratingOptions = new List<RatingOption>();

            //Optional Coverage A
            ratingOptions.Add(CreateOptionalCoverageA(riskData, selectedOptions));

            //Optional Coverage B
            ratingOptions.Add(CreateOptionalCoverageB(riskData, selectedOptions));

            return ratingOptions;
        }

        private RatingOption CreateOptionalCoverageA(RiskData riskData, List<RatingOption> selectedOptions)
        {
            bool isCoverageAvailable = true;
            List<object> coverageOptions = new List<object> { false, true };
            object coverageDefaultValue = true;

            if (RiskOptions.TryGetRiskOption("OptionalCoverageA", out RiskOption coverageRiskOption) == false)
            {
                throw new Exception("Optional Coverage A option not configured");
            }

            return CreateOption(riskData, coverageRiskOption, "OptionalCoverageA", "Optional Coverage A", coverageOptions, coverageDefaultValue, selectedOptions, isCoverageAvailable);
        }

        private RatingOption CreateOptionalCoverageB(RiskData riskData, List<RatingOption> selectedOptions)
        {
            bool isCoverageAvailable = true;
            List<object> coverageOptions = new List<object> { false, 10, 15, 20 };

            /*    
             
            Note for the interviewer: The false option is added to the list of options to allow the user to select no additional coverage. 
            

            If left as null this will cause an issue when attempting to set null to the dictionary key value, as the dictionary expects a non-null value for the key. 
            This can be seen in Rater.CreateOption in IsMatch function
                        
             if (selectedValue != null && selectedValue.Equals(candidate))
                    return true;

            or old code:
            (selOpt.SelectedValue != null && selOpt.SelectedValue.Equals(o)) 


            as well later on when attempting to set the property value in Rater.SetRiskPropertyValue ( only if the previous occuranse was avoided by comparing "null" string value instead of null object referende)  


            */

            object coverageDefaultValue = 10;

            if (RiskOptions.TryGetRiskOption("OptionalCoverageB", out RiskOption coverageRiskOption) == false)
            {
                throw new Exception("Optional Coverage B option not configured");
            }

            return CreateOption(riskData, coverageRiskOption, "OptionalCoverageB", "Optional Coverage B", coverageOptions, coverageDefaultValue, selectedOptions, isCoverageAvailable);
        }
    }
}
