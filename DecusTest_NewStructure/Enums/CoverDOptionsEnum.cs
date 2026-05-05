using System.ComponentModel;

namespace DecusTest_NewStructure.Enums
{
    public enum CoverDOptionsEnum
    {
        [Description("Not Applicable (N/A)")]
        NA = 1,
        [Description("Included")]
        Included = 2,
        [Description("Excluded")]
        Excluded = 3,
    }

}
