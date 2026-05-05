namespace DecusTest_NewStructure.Rater.Processor
{
    public interface IRiskDataOutput    
    {
        public double Premium { get; set; }
        public double? SecondaryPremium { get; set; }
    }
}