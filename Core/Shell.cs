using Core.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace Core
{
    /// <summary>
    /// Work done in shell.xaml.cs ans settings.xaml.cs
    /// </summary>
    public class CoreAction
    {

        /// <summary>
        /// Check Update And Display A Notification
        /// </summary>
        /// <returns>Returns A Notification </returns>
        public async static Task CheckUpdate()
        {
            string url = "http://ap.westudio.ml/sources/json/wecode-update.json";
#if DEBUG
            url = "http://ap.westudio.ml/sources/json/wecode-update-test.json";
#endif
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(new Uri(url));

            var jsonString = await response.Content.ReadAsStringAsync();
            Update.RootObject data = JsonConvert.DeserializeObject<Update.RootObject>(jsonString);
            string version = data.version;
            string[] versionnum = version.Split('.');
            int versioncount = Convert.ToInt32(versionnum[0]) * 10000 + Convert.ToInt32(versionnum[1]) * 100 + Convert.ToInt32(versionnum[2]);
            if (versioncount > 20500)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                HttpResponseMessage detailstring;
                switch (loader.GetString("nr_lan"))
                {
                    case "en":
                        detailstring = await client.GetAsync(new Uri(data.detail.en));
                        break;
                    case "zh-hans":
                        detailstring = await client.GetAsync(new Uri(data.detail.zh_hans));
                        break;
                    case "zh-hant":
                        detailstring = await client.GetAsync(new Uri(data.detail.zh_hant));
                        break;
                    default:
                        detailstring = await client.GetAsync(new Uri(data.detail.en));
                        break;
                }
                string detailstringin = await detailstring.Content.ReadAsStringAsync();

                string xmlContent = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                xmlContent = string.Format(
                    "<toast>" +
                        "<visual>" +
                            "<binding template = 'ToastGeneric'>" +
                                   "<image placement = 'appLogoOverride' src = '' />" +
                                   "<text> {0} {1} {2}</text>" +
                                    "<text>{3}</text>" +
                                    "<image  placement = 'hero' src = 'Assets/new-ver.png' />" +
                            "</binding>" +
                        "</visual>" +
                        "<actions>" +
                            "<action" +
                             " content = '{4}'" +
                             " activationType='protocol'" +
                             " arguments = 'ms-windows-store://pdp/?ProductId=9nblggh5p90f' />" +
                             "<action" +
                             " content = '{5}'" +
                             " arguments = 'action=disableNoti' />" +
                             "<action" +
                             " content = '{6}'" +
                             " activationType='system'" +
                             " arguments = 'dismiss' />" +
                         "</actions>" +
                    "</toast>",
                     loader.GetString("nr_1"), version, loader.GetString("nr_2"), detailstringin, loader.GetString("nr_3"), loader.GetString("nr_5"), loader.GetString("nr_4")
                );
                xdoc.LoadXml(xmlContent);
                ToastNotification toast1 = new ToastNotification(xdoc);
                ToastNotificationManager.CreateToastNotifier().Show(toast1);
            }
        }   
    }
}
