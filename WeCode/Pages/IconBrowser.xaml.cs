﻿using Core.DataModel;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.IO;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WeCode.Pages
{
    public sealed partial class IconBrowser : Page
    {
        public IconBrowser()
        {
            this.InitializeComponent();
            Loaded += IconBrowser_Loaded;
            InitializeUi();
            InitializeList();

        }

        private void IconBrowser_Loaded(object sender, RoutedEventArgs e)
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

        private void InitializeList()
        {
            var fontList = InstalledFont.GetFonts();
            CmbFontFamily.ItemsSource = fontList;
        }

        private async void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            Character output = e.ClickedItem as Character;
            //AddToHistory(output);
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            async () =>
                            {
                                var newWindow = Window.Current;
                                var newAppView = ApplicationView.GetForCurrentView();

                                newAppView.Title = loader.GetString("Details");

                                var frame = new Frame();
                                frame.Navigate(typeof(Browser), output);
                                newWindow.Content = frame;
                                newWindow.Activate();

                                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                                    newAppView.Id,
                                    ViewSizePreference.UseMinimum,
                                    currentAV.Id,
                                    ViewSizePreference.UseMinimum);
                            });

        }
        private async void AddToHistory(Character data)
        {
            String ori = "";

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("history_icon.log");
            StringBuilder result = new StringBuilder();
            using (var storageStream = await sampleFile.OpenReadAsync())
            {
                using (Stream Inputstream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(Inputstream))
                    {
                        ori = reader.ReadToEnd();
                        ori = ori + data.Font + "," + data.Char+","+data.UnicodeIndex+ Environment.NewLine;
                        reader.Dispose();
                    }
                    Inputstream.Dispose();
                }
                storageStream.Dispose();
            }
            var streamout = await sampleFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = streamout.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    dataWriter.WriteString(ori);
                    await dataWriter.StoreAsync();
                    await outputStream.FlushAsync();
                }
            }
            streamout.Dispose();
        }

        private void CmbFontFamily_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var font =(sender as ComboBox).SelectedItem as InstalledFont;
            var fontList = InstalledFont.GetFonts();
            var items = font.GetCharacters();
            gridView.ItemsSource = items;
        }
    }

}

