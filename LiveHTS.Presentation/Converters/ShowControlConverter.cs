using System;
using MvvmCross.Platform.Converters;

namespace LiveHTS.Presentation.Converters
{
    public class ShowControlConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter,System.Globalization.CultureInfo culture)
        {
            return value ? "visible" : "gone";
        }
    }
}