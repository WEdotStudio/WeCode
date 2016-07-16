using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;

namespace Core.Network
{
    public enum Response
    {
        DownloadToLocalFolder = 1,
        DownloadToRemoteFolder = 2,
        DownloadToTemporaryFolder = 3,
    }
    public struct StatusCode
    {
        public string code { get; set; }
        public string status { get; set; }
    }
    public struct Status
    {
        public Brush color { get; set; }
        public string status { get; set; }
        public string code { get; set; }
        public string details { get; set; }

    }

    /// <summary>
    /// URL Praser
    /// </summary>
    public class UrlPhraser
    {
        private static StorageFolder storageFolder;

        /// <summary>
        /// Reach URL and get response
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public async static Task<HttpResponseMessage> ReachURL(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));
            return response;
        }

        /// <summary>
        /// Get File And Download it.
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="way">Ways to deal with the file</param>
        public async static Task ReachFile(string url, Response way)
        {
            HttpResponseMessage response = await ReachURL(url);

            string filename = url.Split("/".ToCharArray()).Last();
            switch (way)
            {
                case Response.DownloadToLocalFolder:
                    storageFolder = ApplicationData.Current.LocalFolder;
                    break;
                case Response.DownloadToRemoteFolder:
                    storageFolder = ApplicationData.Current.RoamingFolder;
                    break;
                case Response.DownloadToTemporaryFolder:
                    storageFolder = ApplicationData.Current.TemporaryFolder;
                    break;
            }

            StorageFile storageFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            var streamout = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = streamout.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    var DataString = await response.Content.ReadAsStringAsync();
                    dataWriter.WriteString(DataString);
                    await dataWriter.StoreAsync();
                    await outputStream.FlushAsync();
                }
            }
            streamout.Dispose();

        }

        /// <summary>
        /// Get Header 
        /// </summary>
        /// <param name="url">URL</param>
        public async static Task<HttpResponseMessage> ReachHeader(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            httpResponse = await httpClient.SendRequestAsync(request);
            return httpResponse;
        }

        /// <summary>
        /// Reach Status Code
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public async static Task<StatusCode> ReachStatusCode(string url)
        {
            StatusCode code = new StatusCode();
            HttpResponseMessage httpResponse = await ReachHeader(url);
            string[] text = httpResponse.ToString().Split(Convert.ToChar(","));
            code.code = (text[0].Split(Convert.ToChar(":")))[1].Substring(1);
            code.status = (text[1].Split(Convert.ToChar(":")))[1].Replace(Convert.ToChar("'"), Convert.ToChar(" ")).Trim();
            return code;
        }
    }

    /// <summary>
    /// Get the status of the network.
    /// </summary>
    public class NetworkStatus: UrlPhraser
    {
        /// <summary>
        /// Get the detail status of a url
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Status</returns>
        public async Task<Status> GetStatus(string url)
        {
            Status status = new Status();
            StatusCode code = await ReachStatusCode(url);
            status.details = code.status;
            status.code = code.code;
            if (code.code.StartsWith("1"))
            {
                status.status = "On Process";
                status.color = GetStatusColor("b");
            }
            else if (code.code.StartsWith("2"))
            {
                status.status = "Operational";
                status.color = GetStatusColor("g");
            }
            else if (code.code.StartsWith("3"))
            {
                status.status = "Redirection Needed";
                status.color = GetStatusColor("y");
            }
            else if (code.code.StartsWith("4"))
            {
                status.status = "Client Error";
                status.color = GetStatusColor("r");
            }
            else if (code.code.StartsWith("5"))
            {
                status.status = "Server Error";
                status.color = GetStatusColor("r");
            }
            else
            {
                status.status = "Unknown";
                status.color = GetStatusColor("b");
            }
            return status;
        }

        /// <summary>
        /// Set the proper color of network status
        /// </summary>
        /// <param name="color">Color indicator:r as red,g as green, b as blue, y as yellow</param>
        /// <returns></returns>
        private Brush GetStatusColor(string color)
        {
            Color c;
            switch (color)
            {
                case "r":
                    c = Colors.Red;
                    break;
                case "g":
                    c = Colors.LightGreen;
                    break;
                case "y":
                    c = Colors.Yellow;
                    break;
                case "b":
                    c = Colors.Blue;
                    break;
            }
            Brush brush = new SolidColorBrush(c);
            return brush;
        }
    }
}
