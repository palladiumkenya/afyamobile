using System;
using MvvmCross.Platform.Converters;

namespace LiveHTS.Presentation.Converters
{
    public class DmyValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.ToString("dd MMM yyyy");
        }
    }
}