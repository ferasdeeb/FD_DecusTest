using DecusTest.Business.Helpers;
using DecusTest.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DecusTest.Business
{
    public class Rater
    {
        protected RiskData _riskData;

        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }

        public Rater(RiskData riskData)
        {
            _riskData = riskData;
        }

        public virtual RatingResults CalculateRate(List<RatingOption> selectedOptions)
        {
            throw new NotImplementedException();
        }

        protected virtual List<RatingOption> GetOptions(RiskData riskData, List<RatingOption> selectedOptions)
        {
            throw new NotImplementedException();
        }

        public static RatingOption CreateOption(RiskData riskData, RiskOption riskOption, string name, string niceName, List<object> options, object defaultValue, List<RatingOption> selectedOptions = null, bool available = true)
        {
            RatingOption option = new RatingOption
            {
                Name = name,
                NiceName = niceName,

                Options = new List<KeyValuePair<string, object>>(riskOption.CreateOptionsDictionary(riskData, options)),

                Available = available
            };

            //Get selected value or default
            if (selectedOptions != null && selectedOptions.Count > 0)
            {
                RatingOption selOpt = selectedOptions.FirstOrDefault(o => o.Name.Equals(name));

                if (selOpt != null)
                {
                    bool anyMatch = false;
                    foreach (var optionCandidate in options)
                    {
                        if (IsMatch(selOpt.SelectedValue, optionCandidate))
                        {
                            anyMatch = true;
                            break;
                        }
                    }

                    option.SelectedValue = anyMatch ? selOpt.SelectedValue : defaultValue;
                }
                else
                {
                    option.SelectedValue = defaultValue;
                }
            }
            else
            {
                option.SelectedValue = defaultValue;
            }

            if (!available)
            {
                //If the option is not available, reset the value
                option.SelectedValue = defaultValue;
            }

            //Set value in risk object, calculations might need the updated value
            if (riskData != null && riskOption != null)
            {
                SetRiskPropertyValue(riskData, riskOption.RiskPropertyName, option.SelectedValue, riskOption.Conversions);
            }

            return option;


            // Helper to try convert using riskOption's target property type
            bool TryConvert(object input, out object converted)
            {
                return ConversionsHelpers.TryChangeType(input, riskOption.GetRiskPropertyType(riskData), out converted);
            }

            // Helper to determine if the selected value matches a candidate option
            bool IsMatch(object selectedValue, object candidate)
            {
                // 1) Direct equality when selected value is not null
                //Selected value is not null and is one of the available options
                if (selectedValue != null && selectedValue.Equals(candidate))
                    return true;

                // 2) Compare after reverting conversions for both values
                //    Selected value is not null (after reverting conversions) and is one of the available options (after reverting conversions)
                if (TryConvert(riskOption.RevertConversions(riskData, selectedValue), out object convSel) &&
                    TryConvert(riskOption.RevertConversions(riskData, candidate), out object convCand) &&
                    convSel != null && convSel.Equals(convCand))
                    return true;

                // 3) Compare after applying conversions for both values
                //    Selected value is not null (after applying conversions) and is one of the available options (after applying conversions)
                if (TryConvert(riskOption.ApplyConversions(selectedValue), out convSel) &&
                    TryConvert(riskOption.ApplyConversions(candidate), out convCand) &&
                    convSel != null && convSel.Equals(convCand))
                    return true;

                // 4) Compare raw reverted value equality (safe null-aware)
                //Selected value (after reverting the conversions) is one of the available options
                if (riskOption.RevertConversions(riskData, selectedValue)?.Equals(candidate) ?? false)
                    return true;

                return false;
            }
        }

        public static void SetRiskPropertyValue(RiskData riskData, string riskPropertyName, object value, Dictionary<object, object> conversions = null)
        {
            //First apply any necessary conversions           
                if (conversions != null && conversions.Count > 0 && value != null && conversions.TryGetValue(value, out object convertedValue))
                {
                    value = convertedValue;
                }
            
            //Set value
            PropertyInfo propertyInfo = riskData.GetType().GetProperty(riskPropertyName);
            
            try
            {
                // Note for interview:
                // While this is still not ideal, it avoids having to catch and handle exceptions for all properties that are nullable, which there are a few of them.
                // This would need to be refactored and remove the additional try{} catch{} block leaving only the catch section to handle any other excpetions 
                if (propertyInfo.PropertyType.FullName.StartsWith("System.Nullable"))
                    propertyInfo.SetValue(riskData, Convert.ChangeType(value, propertyInfo.PropertyType.GenericTypeArguments[0]), null);

                else
                    propertyInfo.SetValue(riskData, Convert.ChangeType(value, propertyInfo.PropertyType), null);
            }
            catch
            {
                try
                {
                    //If setting the value fails might be due to Nullable types => try using 'base' type
                    propertyInfo.SetValue(riskData, Convert.ChangeType(value, propertyInfo.PropertyType.GenericTypeArguments[0]), null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected exception setting value: " + (value?.ToString() ?? "") + " for " + riskPropertyName + " -> " + ex.Message);
                }
            }
        }

        public List<RatingOption> ValidateSelectedOptions(List<RatingOption> selectedOptions)
        {
            List<RatingOption> validatedOptions = new List<RatingOption>();
            var raterOptions = GetOptions(_riskData, selectedOptions);

            foreach (RiskOption riskOption in RiskOptions.RiskOptionsList)
            {
                var raterOption = raterOptions.FirstOrDefault(r => r.Name.Equals(riskOption.Name));
                var selectedOption = selectedOptions.FirstOrDefault(s => s.Name.Equals(riskOption.Name));

                validatedOptions.Add(ValidateSelectedOption(riskOption, raterOption, selectedOption));
            }

            return validatedOptions;
        }

        private RatingOption ValidateSelectedOption(RiskOption riskOption, RatingOption raterOption, RatingOption selectedOption)
        {
            var selectedValue = GetSelectedValue(riskOption, raterOption, selectedOption);

            RatingOption validatedOption = new RatingOption()
            {
                Name = raterOption?.Name ?? riskOption.Name,
                NiceName = raterOption?.NiceName ?? riskOption.Name,
                Available = raterOption?.Available ?? false,
                Options = raterOption?.Options,
                SelectedValue = selectedValue
            };

            return validatedOption;
        }

        private object GetSelectedValue(RiskOption riskOption, RatingOption raterOption, RatingOption selectedOption)
        {
            var validOptions = raterOption?.Options;

            object riskOptionValue = riskOption.GetRiskPropertyValue(_riskData);

            var defaultValue = raterOption?.SelectedValue ?? riskOptionValue ?? riskOption.GetRiskPropertyDefaultValue(_riskData);

            if (selectedOption?.SelectedValue == null)
            {
                if (validOptions == null || validOptions.Any(o => o.Value == null || riskOption.ApplyConversions(o.Key) == null))
                {
                    return null;
                }
                else
                {
                    return defaultValue;
                }
            }
            else if (validOptions != null && validOptions.Any(o => Helpers.ConversionsHelpers.TryChangeType(riskOption.ApplyConversions(o.Value), riskOption.GetRiskPropertyType(_riskData), out object convertedValue) && selectedOption.SelectedValue.Equals(convertedValue)))
            {
                return riskOption.RevertConversions(_riskData, selectedOption.SelectedValue);
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
