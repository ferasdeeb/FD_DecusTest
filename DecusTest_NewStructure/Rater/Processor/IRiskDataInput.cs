using DecusTest_NewStructure.Enums;

namespace DecusTest_NewStructure.Rater.Processor
{
    public interface IRiskDataInput
    {
         double TotalInsuredValue { get; set; }
         StateEnum State { get; set; }
         double DistanceToWater { get; set; }
    }
}