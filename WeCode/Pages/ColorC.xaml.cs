using System;
using Color;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WeCode.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorC : Page
    {
        public ColorC()
        {
            this.InitializeComponent();
        }
        /*public SolidColorBrush MakeColor(double red, double green, double blue)
        {
            return new SolidColorBrush(Windows.UI.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
        }*/
    }
}
