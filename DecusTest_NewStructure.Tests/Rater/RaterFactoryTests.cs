using DecusTest_NewStructure.Rater;
using DecusTest_NewStructure.Rater.OptionalCovers;
using DecusTest_NewStructure.Rater.Processor;
using Microsoft.Extensions.Logging;
using Moq;

namespace DecusTest_NewStructure.Tests.Rater
{

    [TestFixture]
    internal class RaterFactoryTests
    {
        private Mock<ILogger<RaterFactory>> _loggerMock;
        private IRaterFactory _raterFactory;
        private IRiskDataInput _riskDataInput;

        [SetUp]
        public void SetUp()
        {

            _loggerMock = new Mock<ILogger<RaterFactory>>();
            _riskDataInput = new Mock<IRiskDataInput>().Object;
            _raterFactory = new RaterFactory(_loggerMock.Object);
        }


        [TestCase("RaterA", typeof(RaterA))]
        [TestCase("RaterB", typeof(RaterB))]
        public void CreateRater_Returns_Correct_Type(string raterName, Type expectedType) 
        {
            // Arrange 
            IEnumerable<IOptionalCover> optionalCoversList = new List<IOptionalCover>();
           

            // Act
            IRater rater = _raterFactory.CreateRater(raterName, optionalCoversList, _riskDataInput);

            // Assert 
            Assert.That(rater, Is.TypeOf(expectedType));
        }


        [Test]
        public void CreateRater_InvalidName_Returns_Exception() 
        {
            // Arrange 
            string raterName = "UnknownRater";
            IEnumerable<IOptionalCover> optionalCoversList = new List<IOptionalCover>();
           

            // Assert 
            Assert.Throws<ArgumentException>(() => _raterFactory.CreateRater(raterName, optionalCoversList, _riskDataInput));
             
        }
    }
}
