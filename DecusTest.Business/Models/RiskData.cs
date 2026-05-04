namespace DecusTest.Business.Models
{
    /// <summary>
    /// Model that holds the risk data including any rating selections
    /// </summary>
    public class RiskData
    {
        public double TotalInsuredValue { get; set; }
        public string State { get; set; }
        public double DistanceToWater {  get; set; }

        public bool? OptionalCoverageA { get; set; }
        public double? OptionalCoverageB { get; set; }
        public string OptionalCoverageC { get; set; }
        public string OptionalCoverageD { get; set; }
        public double? Premium { get; set; }
        public double? SecondaryPremium { get; set; }
    }
}