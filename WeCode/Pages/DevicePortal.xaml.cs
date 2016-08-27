using System;
using System.Net.Http;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Developer_Hub_For_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DevicePortal : Page
    {

        public DevicePortal()
        {
            this.InitializeComponent();

            Loaded += Page5_Loaded;
        }

        private void Page5_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Uri uri = new Uri("https://192.168.0.103/api/resourcemanager/processes");
         //   HttpClient httpClient = new HttpClient();
          //  HttpResponseMessage response = await httpClient.GetAsync(uri);
          //  string result = await response.Content.ReadAsStringAsync();
          //  info.Text = result;
        }
    }
}
