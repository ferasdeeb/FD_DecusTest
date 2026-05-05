using DecusTest_NewStructure.Rater.OptionalCovers;
using Microsoft.Extensions.Logging;
using DecusTest_NewStructure.Rater.Processor;

namespace DecusTest_NewStructure.Rater
{
    public interface IRaterFactory
    {
       IRater CreateRater(string Name, IEnumerable<IOptionalCover> optionalCoversList, IRiskDataInput riskDataInput);
    }

    public class RaterFactory(ILogger<RaterFactory> log) : IRaterFactory
    {

        public IRater CreateRater(string Name, IEnumerable<IOptionalCover> optionalCoversList, IRiskDataInput riskDataInput)
        {
            try
            {
                switch (Name)
                {
                    case "RaterA":
                        IRater raterA = new RaterA(log, riskDataInput, optionalCoversList);

                        raterA.SetOptionalCovers();
                        return raterA;

                    case "RaterB":
                        IRater raterB = new RaterB(log, riskDataInput, optionalCoversList);
                        raterB.SetOptionalCovers();
                        return raterB;
                    default:
                        {
                            log.LogError($"Invalid rater name. Name: {Name}");
                            throw new ArgumentException($"Invalid rater name. Name: {Name}");
                        }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error Creating Rater: {ex.Message}");
                throw;
            }
        }
    }
}
