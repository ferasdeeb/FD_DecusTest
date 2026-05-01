using System;
using System.ComponentModel;

namespace DecusTest.Business.Helpers
{
    public static class ConversionsHelpers
    {
        public static bool TryChangeType(object value, Type conversionType, out object convertedValue)
        {
            convertedValue = null;


            if (conversionType == null)
            {
                return false;
            }

            if (value == null)
            {
                if (Nullable.GetUnderlyingType(conversionType) == null)
                {
                    return false;
                }
            }
            else
            {
                if (!(value is IConvertible))
                {
                    return false;
                }
            }

            if (value != null)
            {
                var conv = TypeDescriptor.GetConverter(value);
                if (value.GetType() == conversionType || conv.CanConvertTo(conversionType))
                {
                    convertedValue = Convert.ChangeType(value, conversionType);
                }
                else
                {
                    //If converting the value fails might be due to Nullable types => try using 'base' type
                    if (value.GetType() == conversionType.GenericTypeArguments[0] || conv.CanConvertTo(conversionType.GenericTypeArguments[0]))
                    {
                        convertedValue = Convert.ChangeType(value, conversionType.GenericTypeArguments[0]);
                    }
                    else
                    {
                        //One more try (special case for int <-> decimal)
                        if (value is int)
                        {
                            if (conversionType == typeof(decimal))
                            {
                                convertedValue = Convert.ChangeType(value, conversionType);
                            }
                            else if (conversionType == typeof(decimal?))
                            {
                                convertedValue = Convert.ChangeType(value, conversionType.GenericTypeArguments[0]);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (Nullable.GetUnderlyingType(conversionType) != null)
                {
                    if (conversionType.IsValueType)
                    {
                        convertedValue = Activator.CreateInstance(conversionType);
                    }
                }
            }

            return true;
        }

    }
}
