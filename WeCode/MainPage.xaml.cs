using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.Xaml.Controls;



namespace WeCode
{
    public sealed partial class MainPage : Page
    {
        public ApplicationDataContainer _localSettings;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            _localSettings = ApplicationData.Current.LocalSettings;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            UpdateBuildInfo();
           
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
    }
}
