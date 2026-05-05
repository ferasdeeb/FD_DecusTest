using System.ComponentModel;

namespace DecusTest_NewStructure.Enums
{
    public enum CoverBOptionsEnum
    {
        [Description("Excluded")]
        Excluded = 1,
        [Description("10%")]
        _10 = 2,
        [Description("15%")]
        _15 = 3,
        [Description("20%")]
        _20 = 4,
    }
}
