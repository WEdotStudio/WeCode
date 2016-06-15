using System;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Developer_Hub_For_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
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

           
    }
}
