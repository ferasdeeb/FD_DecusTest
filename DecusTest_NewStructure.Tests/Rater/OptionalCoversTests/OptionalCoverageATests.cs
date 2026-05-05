using DecusTest_NewStructure.Enums;
using DecusTest_NewStructure.Rater.OptionalCovers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecusTest_NewStructure.Tests.Rater.OptionalCoversTests
{

    [TestFixture]
    internal class OptionalCoverageATests
    {
        private  OptionalCoverageA _optionalCoverageA;

        [SetUp]
        public void SetUp()
        {

            _optionalCoverageA = new OptionalCoverageA();
        }


        [Test]
        public void OptionalCoverageA_Returns_Correct_DefaultValue()
        {
            // Arrange 
            CoverAOptionsEnum expectedDefaultValue = CoverAOptionsEnum.Excluded;

            // Act
            CoverAOptionsEnum assignedValue = _optionalCoverageA.DefaultValue;

            // Assert 
            Assert.That(assignedValue, Is.EqualTo(expectedDefaultValue));
        }



        [TestCase(true, CoverAOptionsEnum.Included, true)]
        [TestCase(false, CoverAOptionsEnum.Included, false)]
        [TestCase(true, CoverAOptionsEnum.Excluded, false)]
        [TestCase(false, CoverAOptionsEnum.Excluded, false)]
        public void OptionalCoverageA_CanAddToPremium_ReturnsCorrectValue(bool isAvailable, CoverAOptionsEnum defaultValue, bool expectedResult)
        {
            // Arrange 
            _optionalCoverageA.IsAvailable = isAvailable;
            _optionalCoverageA.DefaultValue = defaultValue;

            // Act
            bool canAddToPremium = _optionalCoverageA.CanAddToPremium();

            // Assert 
            Assert.That(canAddToPremium, Is.EqualTo(expectedResult));
        }




        //[Test]
        //public void CalculatePremium_ShouldReturnExpectedPremium()
        //{
        //    // Arrange
        //    var optionalCover = new OptionalCoverageA();
        //    var riskDataInput = new RiskDataInput
        //    {
        //        DistanceToWater = 25,
        //        State = Enums.StateEnum.FL,
        //        TotalInsuredValue = 10000
        //    };
        //    // Act
        //    var premium = optionalCover.CalculatePremium(riskDataInput);
        //    // Assert
        //    Assert.AreEqual(100, premium);
        //}
    }
}
