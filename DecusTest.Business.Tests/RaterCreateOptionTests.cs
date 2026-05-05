using DecusTest.Business.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DecusTest.Business.Tests
{

    // Note for interview: This test class is created to demonstrate the requirement to create a unit test for the existing codebase.
    // This is following the same logic/structure of the existing test samples as created in RaterCreateOptionTests

    // Ideally we should one TestClass called RaterTests, and within this class we should have unit tests for all the public methods of the Rater class.


    [TestClass]
    public class RaterCreateOptionTests
    {
        RiskData _riskData;
        RiskOption _riskOption;
        List<object> _coverageOptions;

        [TestInitialize] 
        public void Initialize() 
        {
            _riskData = new RiskData();
            _riskOption = new RiskOption();
            _coverageOptions = new List<object>();
        }

        [DataTestMethod]
        [DataRow(nameof(RiskData.OptionalCoverageD))]
        public void CreateOption_ValuesSetCorrectly(string coverageName)
        {


        }
    }
}
