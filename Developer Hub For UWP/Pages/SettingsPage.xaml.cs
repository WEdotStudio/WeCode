using System;
using Windows.Storage;
using Windows.UI.Popups;
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
            txt.OnContent = loader.GetString("on");
            txt.OffContent = loader.GetString("off");
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
    }
}
