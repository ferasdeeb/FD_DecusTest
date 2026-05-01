using System.Collections.Generic;

namespace DecusTest.UI.Web.Models.Raters
{
    public class RateResponse
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }

        public List<RatingValuesResponse> RatingValues { get; set; }
    }
}