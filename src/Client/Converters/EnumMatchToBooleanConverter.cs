using System;
using System.Globalization;
using System.Windows.Data;

namespace Client.Converters
{
    internal sealed class EnumMatchToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts enumeration to boolean match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A boolean value if enumeration matches.</returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            var checkValue = value.ToString();
            var targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Converts boolean to enumeration match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A enumeration if input boolean was true for match.</returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            var useValue = (bool) value;
            var targetValue = parameter.ToString();
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return null;
        }
    }
}
