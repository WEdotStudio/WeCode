using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WeCode.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private ApplicationDataContainer _localSettings;
        public SettingsPage()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            this.InitializeComponent();

            Loaded += SettingsPage_Loaded;
            InitializeUi();
            //bool PopupDisabled = Convert.ToBoolean(_localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"]);
            //txt.IsOn = PopupDisabled;
            //txt.Header = loader.GetString("IsUpdatePopupDisabled");
            //txt.OnContent = loader.GetString("on");
            //txt.OffContent = loader.GetString("off");

            //bool OfflineM = Convert.ToBoolean(_localSettings.Containers["Settings"].Values["OfflineMode"]);
            //om.IsOn = OfflineM;
           // om.Header = loader.GetString("OfflineMode");
            //om.OnContent = loader.GetString("on");
            //om.OffContent = loader.GetString("off");
        }

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            bg.Blur(value: 20, duration: 50, delay: 10).Start();
        }

        private void InitializeUi()
        {
            var applicationView = ApplicationView.GetForCurrentView();
            var titleBar = applicationView.TitleBar;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.White;
            Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            // If we have a phone contract, hide the status bar
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 0;
                statusBar.BackgroundColor = Colors.Black;
                statusBar.ForegroundColor = Colors.White;
            }
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
            /*var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
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
            await new MessageDialog(loader.GetString("CMess")).ShowAsync();*/
        }

        private void txt_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            /*switch (txt.IsOn)
            {
                case true:
                    _localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"] = true;
                    break;
                case false:
                    _localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"] = false;
                    break;
            } */
        }

        private void om_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            /*switch (txt.IsOn)
            {
                case true:
                    _localSettings.Containers["Settings"].Values["OfflineMode"] = true;
                    break;
                case false:
                    _localSettings.Containers["Settings"].Values["OfflineMode"] = false;
                    break;
            }*/
        }
    }
}
