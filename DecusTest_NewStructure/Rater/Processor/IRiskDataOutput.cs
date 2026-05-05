namespace DecusTest_NewStructure.Rater.Processor
{
    public interface IRiskDataOutput    
    {
        public IRater Rater { get; set; }
        public double Premium { get; set; }
        public double? SecondaryPremium { get; set; }
    }
}