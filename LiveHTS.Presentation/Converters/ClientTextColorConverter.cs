using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Color;

namespace LiveHTS.Presentation.Converters
{
    public class ClientTextColorConverter : MvxColorValueConverter<bool>
    {
        protected override MvxColor Convert(bool value, object parameter, CultureInfo culture)
        {
            if (value)
            {
                return MvxColors.DarkBlue;
            }
            return MvxColors.Gray;
        }
    }
}
