using System;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Developer_Hub_For_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page3 : Page
    {
        public bool IsPicked1 = false;
        public bool IsPicked2 = false;
        public bool Iss44LVisible = true;
        public StorageFile StoredFile1 = null;
        public StorageFile StoredFile2 = null;
        public Page3()
        {
            this.InitializeComponent();

            s44L.Checked += s44L_Checked;
            s44L.Unchecked += s44L_Unchecked;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StoredFile1 = null;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            StoredFile1 = file;
            if (file != null)
            {
                Stor.Text = file.Name.Substring(0, file.Name.LastIndexOf("."));
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = 1240;
                    bitmapImage.DecodePixelWidth = 1240;

                    await bitmapImage.SetSourceAsync(fileStream);
                    img_s.Source = bitmapImage;
                }
                
                IsPicked1 = true;
            }

        }
        
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            if (IsPicked1 && IsPicked2)
            {
                var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                folderPicker.FileTypeFilter.Add(".png");
                StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                if (folder != null && StoredFile1 != null && StoredFile2 != null)
                {

                    Windows.Storage.AccessCache.StorageApplicationPermissions.
                    FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    StorageFolder newFolder = await folder.CreateFolderAsync(Stor.Text);
                    if (s44L.IsChecked == true)
                    {
                        Resize("Square44x44Logo.scale-400.png", 176, 176, StoredFile1, newFolder);
                        Resize("Square44x44Logo.scale-200.png", 88, 88, StoredFile1, newFolder);
                        Resize("Square44x44Logo.scale-150.png", 66, 66, StoredFile1, newFolder);
                        Resize("Square44x44Logo.scale-125.png", 55, 55, StoredFile1, newFolder);
                        Resize("Square44x44Logo.scale-100.png", 44, 44, StoredFile1, newFolder);
                        if (s44TL.IsChecked == true)
                        {
                            Resize("Square44x44Logo.targetsize-256.png", 256, 256, StoredFile1, newFolder);
                            Resize("Square44x44Logo.targetsize-48.png", 48, 48, StoredFile1, newFolder);
                            Resize("Square44x44Logo.targetsize-24.png", 24, 24, StoredFile1, newFolder);
                            Resize("Square44x44Logo.targetsize-16.png", 16, 16, StoredFile1, newFolder);

                        }
                        if (s44uTL.IsChecked == true)
                        {
                            Resize("Square44x44Logo.targetsize-256_altform-unplated.png", 256, 256, StoredFile1, newFolder);
                            Resize("Square44x44Logo.targetsize-48_altform-unplated.png", 48, 48, StoredFile1, newFolder);
                            Resize("Square44x44Logo.targetsize-24_altform-unplated.png", 24, 24, StoredFile1, newFolder);
                            Resize("Square44x44Logo.targetsize-16_altform-unplated.png", 16, 16, StoredFile1, newFolder);
                        }
                    }
                    if (s71L.IsChecked == true)
                    {
                        Resize("Square71x71Logo.scale-400.png", 284, 284, StoredFile1, newFolder);
                        Resize("Square71x71Logo.scale-200.png", 142, 142, StoredFile1, newFolder);
                        Resize("Square71x71Logo.scale-150.png", 107, 107, StoredFile1, newFolder);
                        Resize("Square71x71Logo.scale-125.png", 89, 89, StoredFile1, newFolder);
                        Resize("Square71x71Logo.scale-100.png", 71, 71, StoredFile1, newFolder);
                    }
                    if (mL.IsChecked == true)
                    {
                        Resize("Square150x150Logo.scale-400.png", 600, 600, StoredFile1, newFolder);
                        Resize("Square150x150Logo.scale-200.png", 300, 300, StoredFile1, newFolder);
                        Resize("Square150x150Logo.scale-150.png", 225, 225, StoredFile1, newFolder);
                        Resize("Square150x150Logo.scale-125.png", 188, 188, StoredFile1, newFolder);
                        Resize("Square150x150Logo.scale-100.png", 150, 150, StoredFile1, newFolder);
                    }
                    if (wL.IsChecked == true)
                    {
                        Resize("Wide310x150Logo.scale-400.png", 600, 1240, StoredFile2, newFolder);
                        Resize("Wide310x150Logo.scale-200.png", 300, 620, StoredFile2, newFolder);
                        Resize("Wide310x150Logo.scale-150.png", 225, 465, StoredFile2, newFolder);
                        Resize("Wide310x150Logo.scale-125.png", 188, 388, StoredFile2, newFolder);
                        Resize("Wide310x150Logo.scale-100.png", 150, 310, StoredFile2, newFolder);
                    }
                    if (lL.IsChecked == true)
                    {
                        Resize("Square310x310Logo.scale-400.png", 1240, 1240, StoredFile1, newFolder);
                        Resize("Square310x310Logo.scale-200.png", 620, 620, StoredFile1, newFolder);
                        Resize("Square310x310Logo.scale-150.png", 465, 465, StoredFile1, newFolder);
                        Resize("Square310x310Logo.scale-125.png", 388, 388, StoredFile1, newFolder);
                        Resize("Square310x310Logo.scale-100.png", 310, 310, StoredFile1, newFolder);
                    }
                    if (sL.IsChecked == true)
                    {
                        Resize("StoreLogo.scale-400.png", 200, 200, StoredFile1, newFolder);
                        Resize("StoreLogo.scale-200.png", 100, 100, StoredFile1, newFolder);
                        Resize("StoreLogo.scale-150.png", 75, 75, StoredFile1, newFolder);
                        Resize("StoreLogo.scale-125.png", 63, 63, StoredFile1, newFolder);
                        Resize("StoreLogo.scale-100.png", 50, 50, StoredFile1, newFolder);
                    }
                    if (bL.IsChecked == true)
                    {
                        Resize("BadgeLogo.scale-400.png", 96, 96, StoredFile1, newFolder);
                        Resize("BadgeLogo.scale-200.png", 48, 48, StoredFile1, newFolder);
                        Resize("BadgeLogo.scale-150.png", 36, 36, StoredFile1, newFolder);
                        Resize("BadgeLogo.scale-125.png", 30, 30, StoredFile1, newFolder);
                        Resize("BadgeLogo.scale-100.png", 24, 24, StoredFile1, newFolder);
                    }
                    if (sS.IsChecked == true)
                    {
                        Resize("SplashScreen.scale-400.png", 1200, 2480, StoredFile2, newFolder);
                        Resize("SplashScreen.scale-200.png", 600, 1240, StoredFile2, newFolder);
                        Resize("SplashScreen.scale-150.png", 450, 930, StoredFile2, newFolder);
                        Resize("SplashScreen.scale-125.png", 375, 775, StoredFile2, newFolder);
                        Resize("SplashScreen.scale-100.png", 300, 620, StoredFile2, newFolder);


                    }
                    await new MessageDialog(loader.GetString("FinishDia")).ShowAsync();
                    StoredFile1 = null;
                    StoredFile2 = null;
                    IsPicked1 = false;
                    IsPicked2 = false;
                }
            }
            else
            {
                await new MessageDialog(loader.GetString("ErrCho")).ShowAsync();
            }
        }


        public async void Resize(String name, uint hsize, uint wsize, StorageFile file,StorageFolder folder)
        {
            StorageFile newFile = await folder.CreateFileAsync(name);
            using (var sourceStream = await file.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(sourceStream);
                BitmapTransform transform = new BitmapTransform() { ScaledHeight = hsize, ScaledWidth = wsize };
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                    BitmapPixelFormat.Rgba8,
                    BitmapAlphaMode.Straight,
                    transform,
                    ExifOrientationMode.RespectExifOrientation,
                    ColorManagementMode.DoNotColorManage);

                using (var destinationStream = await newFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, destinationStream);
                    encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied, wsize, hsize, 100, 100, pixelData.DetachPixelData());
                    await encoder.FlushAsync();
                }
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StoredFile2 = null;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            StoredFile2 = file;
            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = 1200;
                    bitmapImage.DecodePixelWidth = 2480;

                    await bitmapImage.SetSourceAsync(fileStream);
                    img_w.Source = bitmapImage;
                }

                IsPicked2 = true;
            }
        }

        private void s44L_Checked(object sender, RoutedEventArgs e)
        {
            s44TL.IsEnabled = true;
            s44uTL.IsEnabled = true;
            s44TL.IsChecked = true;
            s44uTL.IsChecked = true;
        }

        private void s44L_Unchecked(object sender, RoutedEventArgs e)
        {
            s44TL.IsEnabled = false;
            s44uTL.IsEnabled = false;
            s44TL.IsChecked = false;
            s44uTL.IsChecked = false;
        }
        private void updatecheckbox()
        {
            
        }
    }
}
