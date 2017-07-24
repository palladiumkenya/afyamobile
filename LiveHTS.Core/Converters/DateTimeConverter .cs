using System;
using MvvmCross.Platform.Converters;

namespace LiveHTS.Core.Converters
{
    public class DateTimeConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.ToString("dd MMM yyyy");
        }
    }
}