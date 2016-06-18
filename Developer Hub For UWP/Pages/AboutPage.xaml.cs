using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Developer_Hub_For_UWP.Pages
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9nblggh5p90f"));
        }

        private async void feedbackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Microsoft.Services.Store.Engagement.Feedback.IsSupported)
            {
                await Microsoft.Services.Store.Engagement.Feedback.LaunchFeedbackAsync();
            }
            else
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:wotingwu@live.com"));
            }

        }
        private async void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://patrickwu.cf/apps/"));
        }

        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            StandardPopup.IsOpen = !StandardPopup.IsOpen;
        }
    }
}
