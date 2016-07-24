using Intense.Presentation;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Developer_Hub_For_UWP.Pages;
using Core.Network;
using Core.DataModel;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.System;
using System.Net.Http;
using Windows.UI.Notifications;
using System;
using Windows.Data.Xml.Dom;
using Newtonsoft.Json;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Developer_Hub_For_UWP.Presentation;
using Core;

namespace Developer_Hub_For_UWP
{
    public sealed partial class Shell : UserControl
    {
        private bool isAltKeyPressed;
        private ApplicationDataContainer _localSettings;

        public Shell()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            this.InitializeComponent();

            var applicationView = ApplicationView.GetForCurrentView();
            var titleBar = applicationView.TitleBar;
            titleBar.ButtonInactiveBackgroundColor = Colors.Black;
            titleBar.ButtonInactiveForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Black;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.InactiveBackgroundColor = Colors.Black;
            titleBar.InactiveForegroundColor = Colors.White;
            titleBar.BackgroundColor = Colors.Black;
            titleBar.ForegroundColor = Colors.White;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            var vm = new ShellViewModel();

            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("Home"), PageType = typeof(Home) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("UL"), PageType = typeof(Page2) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("SMI"), PageType = typeof(Page1) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("AG"), PageType = typeof(Page3) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("LBI"), PageType = typeof(Page4) });

            vm.BottomItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("Settings"), PageType = typeof(SettingsPage) });


            // select the first top item
            vm.SelectedItem = vm.TopItems.First();
            this.ViewModel = vm;
            //if new download
            if (!_localSettings.Containers.ContainsKey("Settings"))
            {
                ApplicationDataContainer container = _localSettings.CreateContainer("Settings", ApplicationDataCreateDisposition.Always);
                _localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"] = false;
                _localSettings.Containers["Settings"].Values["IsFonticonExtraFileDownloaded"] = false;
                _localSettings.Containers["Settings"].Values["OfflineMode"] = false;
                _localSettings.Containers["Settings"].Values["Version"] = "020500";

                DelLegacyHistory();
                TransferToStorage();
            }
            //if update
            else if ((string)_localSettings.Containers["Settings"].Values["Version"] != "020500")
            {
                _localSettings.Containers["Settings"].Values["Version"] = "020500";
                _localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"] = false;

                _localSettings.Containers["Settings"].Values["OfflineMode"] = false;
            }
            // whether the internet is connected.
            var conetvty = NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel();
            bool PopIgnored = Convert.ToBoolean(_localSettings.Containers["Settings"].Values["IsUpdatePopupIgnored"]);
            bool PopupDisabled = Convert.ToBoolean(_localSettings.Containers["Settings"].Values["IsUpdatePopupDisabled"]);
            if (conetvty == NetworkConnectivityLevel.InternetAccess)
            {
                if (!PopupDisabled) CheckUpdate();
                UpdateInsidetenApi();
            }

            this.Loaded += delegate { this.Focus(Windows.UI.Xaml.FocusState.Programmatic); };
        }

        private async void CheckUpdate()
        {
            await CoreAction.CheckUpdate();
        }

        private async void DelLegacyHistory()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync("history_icon.log", CreationCollisionOption.OpenIfExists);
            await sampleFile.DeleteAsync();
        }
        public async void TransferToStorage()
        {
            // Cant await inside catch, but this works anyway
            StorageFile stopfile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/api.json"));
            await stopfile.CopyAsync(ApplicationData.Current.LocalFolder);
        }
        private async void UpdateInsidetenApi()
        {
            await UrlPhraser.ReachFile("http://insideten.xyz/api.json", Response.DownloadToLocalFolder);
        }


        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }

        private void Grid_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Menu) isAltKeyPressed = true;
            else if (isAltKeyPressed)
            {
                switch (e.Key)
                {
                    case VirtualKey.H: Frame.Navigate(typeof(Home), Frame); break;
                    case VirtualKey.U: Frame.Navigate(typeof(Page2), Frame); break;
                    case VirtualKey.F: Frame.Navigate(typeof(Page1), Frame); break;
                    case VirtualKey.A: Frame.Navigate(typeof(Page3), Frame); break;
                    case VirtualKey.B: Frame.Navigate(typeof(Page4), Frame); break;
                    case VirtualKey.S: Frame.Navigate(typeof(SettingsPage), Frame); break;
                }
            }
        }
    }
}