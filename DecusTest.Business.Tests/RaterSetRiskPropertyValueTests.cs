using DecusTest.Business.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecusTest.Business.Tests
{
    [TestClass]
    public class RaterSetRiskPropertyValueTests
    {
        [DataTestMethod]
        [DataRow(nameof(RiskData.TotalInsuredValue), 5000.0)]
        [DataRow(nameof(RiskData.TotalInsuredValue), 12345.67)]
        public void SetRiskPropertyValue_ShouldSetDoubleProperties(string propertyName, double value)
        {
            var riskData = new RiskData();
            Rater.SetRiskPropertyValue(riskData, propertyName, value);
            Assert.AreEqual(value, riskData.TotalInsuredValue);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.State), "FL")]
        [DataRow(nameof(RiskData.State), "TX")]
        public void SetRiskPropertyValue_ShouldSetStringProperties(string propertyName, string value)
        {
            var riskData = new RiskData();
            Rater.SetRiskPropertyValue(riskData, propertyName, value);
            Assert.AreEqual(value, riskData.State);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageA), true)]
        [DataRow(nameof(RiskData.OptionalCoverageA), false)]
        public void SetRiskPropertyValue_ShouldSetBoolProperties(string propertyName, bool value)
        {
            var riskData = new RiskData();
            Rater.SetRiskPropertyValue(riskData, propertyName, value);
            Assert.AreEqual(value, riskData.OptionalCoverageA);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageB), 10.0)]
        [DataRow(nameof(RiskData.OptionalCoverageB), 20.0)]
        public void SetRiskPropertyValue_ShouldSetNullableDoubleProperties(string propertyName, double value)
        {
            var riskData = new RiskData();
            Rater.SetRiskPropertyValue(riskData, propertyName, value);
            Assert.AreEqual(value, riskData.OptionalCoverageB);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageC), "Full Value")]
        [DataRow(nameof(RiskData.OptionalCoverageC), "Custom Value")]
        public void SetRiskPropertyValue_ShouldSetStringCoverageProperties(string propertyName, string value)
        {
            var riskData = new RiskData();
            Rater.SetRiskPropertyValue(riskData, propertyName, value);
            Assert.AreEqual(value, riskData.OptionalCoverageC);
        }
    }
}
