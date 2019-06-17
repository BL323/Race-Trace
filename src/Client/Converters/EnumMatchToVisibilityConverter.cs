using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Client.Converters
{
    internal sealed class EnumMatchToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts enumeration to visibility match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Visibility value if enumeration matches.</returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            var checkValue = value.ToString();
            var targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                StringComparison.InvariantCultureIgnoreCase)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts visibility to enumeration match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A enumeration if input visibility was visible.</returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            var success = Enum.TryParse(value.ToString(), out Visibility visibility);
            if (success)
                return visibility == Visibility.Visible;

            return null;
        }
    }
}
