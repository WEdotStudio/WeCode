﻿using Core.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Developer_Hub_For_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        private ApplicationDataContainer _appSettings;
        public bool isopen = false;
        public Page2()
        {
            this.InitializeComponent();
            _appSettings = ApplicationData.Current.LocalSettings;
            this.Loaded += Page2_Loaded;
        }

        private async void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();

            Uri uri = new Uri("ms-appx:///Assets/Data/uri.csv");
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            using (var storageStream = await storageFile.OpenReadAsync())
            {
                using (Stream stream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        List<URI> Items = new List<URI>();
                        List<URI> mItems = new List<URI>();
                        List<URI> sItems = new List<URI>();
                        List<URI> wsItems = new List<URI>();
                        List<URI> pItems = new List<URI>();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            if (values[0].Contains("General"))
                            {
                                Items.Add(new URI { Intro = values[2], Content = values[1] });
                            }
                            if (values[0].Contains("Settings"))
                            {
                                sItems.Add(new URI { Intro = values[2], Content = values[1] });
                            }
                            if (values[0].Contains("Maps"))
                            {
                                mItems.Add(new URI { Intro = values[2], Content = values[1] });
                            }
                            if (values[0].Contains("Store"))
                            {
                                wsItems.Add(new URI { Intro = values[2], Content = values[1] });
                            }
                            if (values[0].Contains("People"))
                            {
                                pItems.Add(new URI { Intro = values[2], Content = values[1] });
                            }
                        }
                        gView.ItemsSource = Items;
                        sView.ItemsSource = sItems;
                        mView.ItemsSource = mItems;
                        wsView.ItemsSource = wsItems;
                        pView.ItemsSource = pItems;

                        reader.Dispose();
                    }
                    stream.Dispose();
                }
                storageStream.Dispose();
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            try
            {
                if (inp.Text != "")
                {

                    await Windows.System.Launcher.LaunchUriAsync(new Uri(inp.Text));
                    AddToHistory(inp.Text);
                }
            }
            catch
            {
                await new MessageDialog(loader.GetString("ErrURI")).ShowAsync();
            }

        }

        private async void AddToHistory(string data)
        {
            String ori = "";

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("history.log");
            StringBuilder result = new StringBuilder();
            using (var storageStream = await sampleFile.OpenReadAsync())
            {
                using (Stream Inputstream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(Inputstream))
                    {
                        ori = reader.ReadToEnd();
                        ori = ori + data + Environment.NewLine;
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

        private async void UpdateList()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("history.log", CreationCollisionOption.OpenIfExists);
            using (var storageStream = await storageFile.OpenReadAsync())
            {
                using (Stream stream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        List<UriHistory> Items = new List<UriHistory>();

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            Items.Add(new UriHistory { Content = line });
                        }
                        Items.Reverse();
                        listView.ItemsSource = Items;


                        reader.Dispose();
                    }
                    stream.Dispose();
                }
                storageStream.Dispose();
            }
        }

        private void HamburgerButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            splitView.IsPaneOpen = true;
            UpdateList();
        }
        private void HamburgerButton1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            splitView.IsPaneOpen = false;
        }

        private async void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            UriHistory his = e.ClickedItem as UriHistory;
            await Windows.System.Launcher.LaunchUriAsync(new Uri(his.Content));
            AddToHistory(his.Content);

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void listView1_ItemClick(object sender, ItemClickEventArgs e)
        {
            URI u = e.ClickedItem as URI;
            inp.Text = u.Content;
        }


        private void D_Action(object sender, RoutedEventArgs e)
        {
            if (!isopen)
            {
                Open.Begin();
                isopen = true;
            }
            else
            {
                Close.Begin();
                isopen = false;
            }
        }

        private void D_Close(object sender, RoutedEventArgs e)
        {
            Close.Begin();
            isopen = false;
        }
    }
    
   
}
