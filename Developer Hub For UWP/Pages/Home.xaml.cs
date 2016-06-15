using Developer_Hub_For_UWP.Presentation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Windows.ApplicationModel.Core;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static Developer_Hub_For_UWP.Presentation.insideten;

//

namespace Developer_Hub_For_UWP.Pages
{

    public sealed partial class Home : Page
    {
        public ApplicationDataContainer _localSettings;
        public Home()
        {
            this.InitializeComponent();
            _localSettings = ApplicationData.Current.LocalSettings;

            UpdateBuildInfo();
            InitializeIconHistoryList();
        }

        private async void InitializeIconHistoryList()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("history_icon.log", CreationCollisionOption.OpenIfExists);
            using (var storageStream = await storageFile.OpenReadAsync())
            {
                using (Stream stream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        List<IconHistory> Items = new List<IconHistory>();

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            Items.Add(new IconHistory { Name = values[0], Graph = values[1] });
                        }
                        Items.Reverse();
                        if(Items.Count == 0)
                        {
                            noH.Visibility = Visibility.Visible;
                        }
                        else 
                        {
                            noH.Visibility = Visibility.Collapsed;
                            if (Items.Count > 8) Items = Items.GetRange(0, 8);
                        }
                       
                        gridView.ItemsSource = Items;


                        reader.Dispose();
                    }
                    stream.Dispose();
                }
                storageStream.Dispose();
            }
        }
        private async void UpdateBuildInfo()
        {
            string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong v = ulong.Parse(sv);
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            bu.Text = $"{v3}";

            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            ma.Text = eas.SystemManufacturer+" "+ eas.SystemProductName;

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://insideten.xyz/api.json"));

            var jsonString = await response.Content.ReadAsStringAsync();
            //if(!_localSettings.Containers.)
            RootObject data = JsonConvert.DeserializeObject<RootObject>(jsonString);
            Build_v.Text = data.@internal.build;
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsWrapGrid MyItemsPanel = (ItemsWrapGrid)gridView.ItemsPanelRoot;
            MyItemsPanel.ItemWidth = (e.NewSize.Width - 10) / 8;
        }

        private async void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            IconHistory output = e.ClickedItem as IconHistory;
            string selectInfo = output.Graph;
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            async () =>
                            {
                                var newWindow = Window.Current;
                                var newAppView = ApplicationView.GetForCurrentView();

                                newAppView.Title = selectInfo + loader.GetString("Detail");

                                var frame = new Frame();
                                frame.Navigate(typeof(Browser), selectInfo);
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
