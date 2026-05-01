using DecusTest.Business.Models;
using System.Collections.Generic;

namespace DecusTest.UI.Web.Models
{
    public class RatingValuesRequest
    {
        public RiskData RiskData { get; set; }

        public int? RaterId { get; set; }
        public List<RatingOption> RatingOptions { get; set; }
    }
}