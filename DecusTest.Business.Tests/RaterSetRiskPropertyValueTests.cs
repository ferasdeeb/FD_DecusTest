using DecusTest.Business.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecusTest.Business.Tests
{
    [TestClass]
    public class RaterSetRiskPropertyValueTests
    {

        RiskData _riskData;

        [TestInitialize]
        public void Initialize()
        {
            _riskData = new RiskData();
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.TotalInsuredValue), 5000.0)]
        [DataRow(nameof(RiskData.TotalInsuredValue), 12345.67)] // Note for interview: This test is not needed since the test has been covered in the previous sample. The same applies to the tests below.
        public void SetRiskPropertyValue_ShouldSetDoubleProperties(string propertyName, double value)
        {
            Rater.SetRiskPropertyValue(_riskData, propertyName, value);
            Assert.AreEqual(value, _riskData.TotalInsuredValue);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.State), "FL")]
        [DataRow(nameof(RiskData.State), "TX")] // Not needed since the test has been covered in the previous sample
        [DataRow(nameof(RiskData.State), " ")]
        [DataRow(nameof(RiskData.State), null)]
        [DataRow(nameof(RiskData.State), "")]
        public void SetRiskPropertyValue_ShouldSetStringProperties(string propertyName, string value)
        {
            Rater.SetRiskPropertyValue(_riskData, propertyName, value);
            Assert.AreEqual(value, _riskData.State);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageA), true)]
        [DataRow(nameof(RiskData.OptionalCoverageA), false)]
        [DataRow(nameof(RiskData.OptionalCoverageA), null)]
        public void SetRiskPropertyValue_ShouldSetBoolProperties(string propertyName, bool value)
        {
            Rater.SetRiskPropertyValue(_riskData, propertyName, value);
            Assert.AreEqual(value, _riskData.OptionalCoverageA);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageB), 10.0)]
        [DataRow(nameof(RiskData.OptionalCoverageB), 20.0)] // Not needed since the test has been covered in the previous sample
        [DataRow(nameof(RiskData.OptionalCoverageB), null)]
        public void SetRiskPropertyValue_ShouldSetNullableDoubleProperties(string propertyName, double value)
        {
            Rater.SetRiskPropertyValue(_riskData, propertyName, value);
            Assert.AreEqual(value, _riskData.OptionalCoverageB);
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageC), "Full Value")]
        [DataRow(nameof(RiskData.OptionalCoverageC), "Custom Value")]
        public void SetRiskPropertyValue_ShouldSetStringCoverageProperties(string propertyName, string value)
        {
            Rater.SetRiskPropertyValue(_riskData, propertyName, value);
            Assert.AreEqual(value, _riskData.OptionalCoverageC);
        }
    }
}
