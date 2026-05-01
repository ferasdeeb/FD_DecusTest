using System.Collections.Generic;
using System.Linq;

namespace DecusTest.Business
{
    public class RiskOptions
    {
        internal static List<RiskOption> RiskOptionsList => new List<RiskOption>()
        {
            new RiskOption()
            {
                Name = "OptionalCoverageA",
                NiceName = "Optional Coverage A",
                RiskPropertyName = "OptionalCoverageA",
                Conversions = new Dictionary<object, object>(){ { "Included", true },{ "Excluded", false } }
            },
            new RiskOption()
            {
                Name = "OptionalCoverageB",
                NiceName = "Optional Coverage B",
                RiskPropertyName = "OptionalCoverageB",
                Conversions = new Dictionary<object, object>(){ { "Excluded", null }, { "10%", 10 }, { "15%", 15 }, { "20%", 20 } }
            },
            new RiskOption()
            {
                Name = "OptionalCoverageC",
                NiceName = "Optional Coverage C",
                RiskPropertyName = "OptionalCoverageC",
                Conversions = new Dictionary<object, object>()
            }
        };

        public static bool TryGetRiskOption(string name, out RiskOption riskOption)
        {
            riskOption = null;

            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (RiskOptionsList.Any(o => o.Name.Equals(name)) == false)
            {
                return false;
            }

            riskOption = RiskOptionsList.First(o => o.Name.Equals(name));
            return true;
        }
    }
}
