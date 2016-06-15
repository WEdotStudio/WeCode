using Newtonsoft.Json;
using System;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using static Developer_Hub_For_UWP.Presentation.insideten;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Developer_Hub_For_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page4 : Page
    {
        public Page4()
        {
            this.InitializeComponent();

            UpdateData();
        }

        private async void UpdateData()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://insideten.xyz/api.json"));

            var jsonString = await response.Content.ReadAsStringAsync();
            RootObject data = JsonConvert.DeserializeObject<RootObject>(jsonString);

            pcwrp_b.Text = data.pcwrp.build;
            pcwrp_v.Text = data.pcwrp.version;
            pcwrp_m.NavigateUri = new Uri(add_format(data.pcwrp.more));
            pcwrp_r.Text = data.pcwrp.release_date;

            pcwif_b.Text = data.pcwif.build;
            pcwif_v.Text = data.pcwif.version;
            pcwif_m.NavigateUri = new Uri(add_format(data.pcwif.more));
            pcwif_r.Text = data.pcwif.release_date;

            pcwis_b.Text = data.pcwis.build;
            pcwis_v.Text = data.pcwis.version;
            pcwis_m.NavigateUri = new Uri(add_format(data.pcwis.more));
            pcwis_r.Text = data.pcwis.release_date;

            mowrp_b.Text = data.mowrp.build;
            mowrp_v.Text = data.mowrp.version;
            mowrp_m.NavigateUri = new Uri(add_format(data.mowrp.more));
            mowrp_r.Text = data.mowrp.release_date;

            mowif_b.Text = data.mowif.build;
            mowif_v.Text = data.mowif.version;
            mowif_m.NavigateUri = new Uri(add_format(data.mowif.more));
            mowif_r.Text = data.mowif.release_date;

            mowis_b.Text = data.mowis.build;
            mowis_v.Text = data.mowis.version;
            mowis_m.NavigateUri = new Uri(add_format(data.mowis.more));
            mowis_r.Text = data.mowis.release_date;

        }
      
    }
}
