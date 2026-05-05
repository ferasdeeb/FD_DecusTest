using DecusTest_NewStructure.Rater.OptionalCovers;
using Microsoft.Extensions.Logging;
using DecusTest_NewStructure.Rater.Processor;

namespace DecusTest_NewStructure.Rater
{

    /*

    abstract class RatingOption { }


    class OptionalCoverageA : RatingOption { }

    class OptionalCoverageB : RatingOption { }

    class OptionalCoverageC : RatingOption { }

    class OptionalCoverageD : RatingOption { }




    abstract class Rater

    {
        private List<RatingOption> _ratingOptions = new List<RatingOption>();

        public List<RatingOption> RatingOptions
        {
            get { return _ratingOptions; }
        }
        
        public abstract void CreateRatingOption();

        public Rater()
        {
                CreateRatingOption();
        }
    }


    class RaterA : Rater
    {
        public override void CreateRatingOption()
        {
            RatingOptions.Add(new OptionalCoverageA());
            RatingOptions.Add(new OptionalCoverageC());
            RatingOptions.Add(new OptionalCoverageD());
        }
    }

    class RaterB : Rater
    {
        public override void CreateRatingOption()
        {
            RatingOptions.Add(new OptionalCoverageA());
            RatingOptions.Add(new OptionalCoverageB());
        }
    }

    public interface IRater
    {
        RatingResults CalculateRate();

        double CalculatePremium(RiskDataInput riskData);

        double CalculateSecondaryPremium(RiskDataInput riskData);

        void SetOptionalCovers();

    }

    public interface IRaterFactory
    {
        IRater CreateRater(string Name);
    }

     public IRater CreateRater(string Name)
        {
            IRater rater;

            switch (Name)
            {
                case "RaterA":
                        return new RaterA();
                case "RaterB":
                        return new RaterB();
                default:
                    throw new ArgumentException("Invalid rater name");
            }
        }

    */


    public interface IRaterFactory
    {
       IRater CreateRater(string Name, IEnumerable<IOptionalCover> optionalCoversList);
    }

    public class RaterFactory(ILogger<RaterFactory> log, IRiskDataInput riskDataInput) : IRaterFactory
    {
       
        public IRater CreateRater(string Name, IEnumerable<IOptionalCover> optionalCoversList)
        {

            switch (Name)
            {
                case "RaterA":
                    IRater raterA =  new RaterA(log, riskDataInput, optionalCoversList);

                    raterA.SetOptionalCovers();
                    return raterA;

                case "RaterB":
                    IRater raterB = new RaterB(log, riskDataInput, optionalCoversList);
                    raterB.SetOptionalCovers();
                    return raterB;
                default:
                    throw new ArgumentException("Invalid rater name");
            }
        }
    }


     

    

    //abstract class Rater

    //{
    //    private List<RatingOption> _ratingOptions = new List<RatingOption>();

    //    public List<RatingOption> RatingOptions
    //    {
    //        get { return _ratingOptions; }
    //    }

    //    public abstract void CreateRatingOption();

    //    public Rater()
    //    {
    //        CreateRatingOption();
    //    }
    //}


    //class RaterA : Rater
    //{
    //    public override void CreateRatingOption()
    //    {
    //        RatingOptions.Add(new OptionalCoverageA());
    //        RatingOptions.Add(new OptionalCoverageC());
    //        RatingOptions.Add(new OptionalCoverageD());
    //    }
    //}

    //class RaterB : Rater
    //{
    //    public override void CreateRatingOption()
    //    {
    //        RatingOptions.Add(new OptionalCoverageA());
    //        RatingOptions.Add(new OptionalCoverageB());
    //    }
    //}




}
