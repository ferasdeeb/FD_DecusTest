namespace DecusTest_NewStructure.Rater.OptionalCovers
{
    public interface IOptionalCover
    {
        string Name { get; set; }
        string NiceName { get; set; }
        bool IsAvailable { get; set; }
        bool CanAddToPremium();
        
    }
}
