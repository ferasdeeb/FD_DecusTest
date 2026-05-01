using DecusTest.Business;
using DecusTest.UI.Web.Models;
using DecusTest.UI.Web.Models.Raters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace DecusTest.UI.Web.Raters
{
    /// <summary>
    /// Summary description for get_rates
    /// </summary>
    public class get_rates : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Clear();

            string ratingRequestJson;
            using (var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8))
            {
                ratingRequestJson = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(ratingRequestJson))
            {
                throw new Exception("Rating data is missing");
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RatingValuesRequest ratingRequest = serializer.Deserialize<RatingValuesRequest>(ratingRequestJson);
            var riskData = ratingRequest.RiskData;

            if (riskData == null)
            {
                throw new Exception("Cannot read risk data");
            }

            var rateResponse = new RateResponse() { ErrorMessages = new List<string>() };

            if (string.IsNullOrEmpty(riskData.State))
            {
                rateResponse.ErrorMessages.Add("State is missing");
            }

            if (riskData.DistanceToWater <= 0)
            {
                rateResponse.ErrorMessages.Add("Invalid value for 'distance to water'");
            }

            if (rateResponse.ErrorMessages.Count == 0)
            {
                try
                {                    
                    var ratingValues = new List<RatingValuesResponse>();

                    if (ratingRequest.RaterId == null || ratingRequest.RaterId == 1)
                    {
                        var raterA = new RaterA.RaterA(riskData);
                        ratingValues.Add(GetRatingValues(ratingRequest, raterA));
                    }

                    if (ratingRequest.RaterId == null || ratingRequest.RaterId == 2)
                    {
                        var raterB = new RaterB.RaterB(riskData);
                        ratingValues.Add(GetRatingValues(ratingRequest, raterB));
                    }

                    rateResponse.RatingValues = ratingValues;
                    rateResponse.Success = true;
                }
                catch (Exception ex)
                {
                    rateResponse.ErrorMessages.Add($"Unexpected error rating: {ex.Message}");
                }
            }

            context.Response.Write(serializer.Serialize(rateResponse));
        }

        private static RatingValuesResponse GetRatingValues(RatingValuesRequest ratingRequest, Rater rater)
        {
            var ratingOptionsWithSelections = rater.ValidateSelectedOptions(ratingRequest.RatingOptions);

            var rateResults = rater.CalculateRate(ratingOptionsWithSelections);

            return new RatingValuesResponse()
            {
                RaterId = rater.Id,
                RaterName = rater.Name,
                Premium = rateResults.Premium,
                SecondaryPremium = rateResults.SecondaryPremium,
                RatingOptions = rateResults.RatingOptions?.Where(o => o.Available)?.ToList()
            };
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}