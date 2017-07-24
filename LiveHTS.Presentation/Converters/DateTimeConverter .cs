using System;
using MvvmCross.Platform.Converters;

namespace LiveHTS.Presentation.Converters
{
    public class DMYDateTimeValueConverter : MvxValueConverter<DateTime?, string>
    {
        protected override string Convert(DateTime? value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (null == value)
                return string.Empty;

            return value.Value.ToString("dd MMM yyyy");
        }
    }
}