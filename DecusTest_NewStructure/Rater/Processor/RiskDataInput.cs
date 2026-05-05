using DecusTest_NewStructure.Enums;

namespace DecusTest_NewStructure.Rater.Processor
{
    public class RiskDataInput : IRiskDataInput
    {
        public double TotalInsuredValue { get; set; }
        public StateEnum State { get; set; }
        public double DistanceToWater { get; set; }
    }

    public class RiskDataOutput : IRiskDataOutput
    {
        public double Premium { get; set; } = 0;
        public double? SecondaryPremium { get; set; } = 0;
    }
}
