using Microsoft.Extensions.DependencyInjection;

namespace DecusTest_NewStructure.Tests
{

    /// <summary>
    /// This test is used to ensure that the correct number of services are registered in the dependency injection container.
    /// Preventing Missing Dependency Runtime Errors
    /// Ensuring that refactoring or adding new features doesn't accidentally remove or misconfigure existing required services.
    /// 
    /// If the number of services changes, it could indicate that a service was added or removed, which may have unintended consequences on the application's functionality. 
    /// By having this test, we can quickly identify if there has been a change in the service registration and investigate further to ensure that it was intentional and does not introduce any issues.
    /// </summary>
    [TestFixture]
    internal class ServiceCollectionTests
    {
        [Test]
        public void CorrectNumberOfServicesRegistered()
        {
            var services = DependencyInjection.Builder.RegisterServices(new ServiceCollection());

            var numberOfServicesExpectedToBeAddedToContainer = 10;

            Assert.That(services.Count, Is.EqualTo(numberOfServicesExpectedToBeAddedToContainer));
        }

    }
}
