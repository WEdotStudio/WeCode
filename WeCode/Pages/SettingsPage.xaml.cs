using System;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Developer_Hub_For_UWP.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private ApplicationDataContainer _localSettings;
        public SettingsPage()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            this.InitializeComponent();

            bool PopupDisabled = Convert.ToBoolean(_localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"]);
            txt.IsOn = PopupDisabled;
            txt.Header = loader.GetString("IsUpdatePopupDisabled");
            txt.OnContent = loader.GetString("on");
            txt.OffContent = loader.GetString("off");

            bool OfflineM = Convert.ToBoolean(_localSettings.Containers["Settings"].Values["OfflineMode"]);
            om.IsOn = OfflineM;
            om.Header = loader.GetString("OfflineMode");
            om.OnContent = loader.GetString("on");
            om.OffContent = loader.GetString("off");
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9nblggh5p90f"));
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://publisher/?name=WE. Studio"));
        }

        private async void mailButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:patrick.we.studio@outlook.com"));
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            if (sma.IsChecked == true)
                {
                StorageFile sampleFile = await storageFolder.GetFileAsync("history_icon.log");
                await sampleFile.DeleteAsync();
            }
            if (urim.IsChecked == true)
                {
                StorageFile sampleFile = await storageFolder.GetFileAsync("history.log");
                await sampleFile.DeleteAsync();
            }
            await new MessageDialog(loader.GetString("CMess")).ShowAsync();
        }

        private void txt_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            switch (txt.IsOn)
            {
                case true:
                    _localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"] = true;
                    break;
                case false:
                    _localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"] = false;
                    break;
            } 
        }

        private void om_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            switch (txt.IsOn)
            {
                case true:
                    _localSettings.Containers["Settings"].Values["OfflineMode"] = true;
                    break;
                case false:
                    _localSettings.Containers["Settings"].Values["OfflineMode"] = false;
                    break;
            }
        }
    }
}
