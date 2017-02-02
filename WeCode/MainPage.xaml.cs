using Core.DataModel;
using WeCode.Pages;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System.Collections.Generic;
using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



namespace WeCode
{
    public sealed partial class MainPage : Page
    {
        public ApplicationDataContainer _localSettings;
        public MainPage()
        {
            this.InitializeComponent();

            _localSettings = ApplicationData.Current.LocalSettings;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            this.Loaded += MainPage_Loaded;

            InitializeUi();


            List<Feature> fList = new List<Feature>();

            fList.Add(new Feature { Icon = "", Name = loader.GetString("UL"), PageType = typeof(URILauncher) });
            fList.Add(new Feature { Icon = "", Name = loader.GetString("SMI"), PageType = typeof(IconBrowser) });
            fList.Add(new Feature { Icon = "", Name = loader.GetString("AG"), PageType = typeof(AssetsGen) });
            fList.Add(new Feature { Icon = "", Name = "Device Portal(i)", PageType = typeof(DevicePortal) });
            fList.Add(new Feature { Icon = "", Name = "Json2Class Converter(i)", PageType = typeof(Json2CS) });
            fList.Add(new Feature { Icon = "", Name = "Color Converter(i)", PageType = typeof(ColorC) });
            fList.Add(new Feature { Icon = "", Name = "Notification Editor(i)", PageType = typeof(NotificationEd) });
            //vm.BottomItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("Settings"), PageType = typeof(SettingsPage) });

            gridView.ItemsSource = fList;

            
            UpdateBuildInfo();
           
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
        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            bg.Blur(value: 20, duration: 50, delay: 10).Start();
        }
        private void UpdateBuildInfo()
        {
            //System Version
            string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong v = ulong.Parse(sv);
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            bu.Text = $"{v3}";

            //System Info
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            ma.Text = eas.SystemManufacturer + " " + eas.SystemProductName;
        }

        private async void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var items = (Feature)e.ClickedItem;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            async () =>
                            {
                                var newWindow = Window.Current;
                                var newAppView = ApplicationView.GetForCurrentView();

                                newAppView.Title = items.Name;

                                var frame = new Frame();
                                frame.Navigate(items.PageType);
                                newWindow.Content = frame;
                                newWindow.Activate();

                                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                                    newAppView.Id,
                                    ViewSizePreference.UseMinimum,
                                    currentAV.Id,
                                    ViewSizePreference.UseMinimum);
                            });
        }
    }
}
