using System.Collections.Generic;

namespace DecusTest.Business.Models
{
    public class RatingOption
    {
        public string Name { get; set; }
        public string NiceName { get; set; }
        public bool Available { get; set; }
        public List<KeyValuePair<string, object>> Options { get; set; }
        public object SelectedValue { get; set; }
    }
}