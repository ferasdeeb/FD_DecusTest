using System.Collections.Generic;

namespace DecusTest.Business.Models
{
    public class RatingResults
    {
        public double? Premium { get; set; }
        public double? SecondaryPremium { get; set; }

        public List<RatingOption> RatingOptions { get; set; }
    }
}