using System;
using System.Collections.Generic;
using System.Linq;

namespace DecusTest.Business
{
    public class RiskOption
    {
        public string Name { get; set; }
        public string NiceName { get; set; }
        public string RiskPropertyName { get; set; }

        public Dictionary<object, object> Conversions { get; set; }

        public object ApplyConversions(object value)
        {
            if (Conversions != null && Conversions.Count > 0 && value != null && Conversions.TryGetValue(value, out object convertedValue))
            {
                return convertedValue;
            }

            return value;
        }

        public object RevertConversions(object obj, object value)
        {

            if (Conversions != null && Conversions.Count > 0)
            {
                KeyValuePair<object, object> conversionApplied = Conversions.FirstOrDefault(c => c.Value == null && value == null || (Helpers.ConversionsHelpers.TryChangeType(c.Value, this.GetRiskPropertyType(obj), out object convertedValue) && convertedValue != null && convertedValue.Equals(value)) || value != null && value.Equals(c.Value));

                if (conversionApplied.Key != null)
                {
                    return conversionApplied.Key;
                }
            }

            if (value != null)
            {
                if (GetRiskPropertyType(obj).IsGenericType)
                {
                    if (GetRiskPropertyType(obj).GenericTypeArguments[0].Equals(typeof(DateTime)))
                    {
                        // Coerce DateTime to string for UI to handle
                        value = DateTime.Parse(value.ToString());
                    }
                }
                else
                {
                    if (GetRiskPropertyType(obj).Equals(typeof(DateTime)))
                    {
                        // Coerce DateTime to string for UI to handle
                        value = DateTime.Parse(value.ToString());
                    }
                }
            }
            return value;
        }

        public IDictionary<string, object> CreateOptionsDictionary(Models.RiskData riskData, List<object> options)
        {
            IDictionary<string, object> optionsDictionary = new Dictionary<string, object>();

            foreach (object option in options)
            {                
                string key = Convert.ToString(RevertConversions(riskData, option));

                optionsDictionary.Add(key, option);
            }

            return optionsDictionary;
        }

        public Type GetRiskPropertyType(object obj)
        {
            return obj.GetType().GetProperty(RiskPropertyName).PropertyType;
        }
        public object GetRiskPropertyDefaultValue(object obj)
        {
            Type t = GetRiskPropertyType(obj);

            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }

        public object GetRiskPropertyValue(object obj)
        {
            return obj.GetType().GetProperty(RiskPropertyName).GetValue(obj, null);
        }
    }
}
