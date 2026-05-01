namespace DecusTest.UI.Web.Models
{
    public class RatingValuesResponse : RatingValuesRequest
    {
        public string RaterName { get; set; }
        public double? Premium {  get; set; }
        public double? SecondaryPremium { get; set; }
    }
}