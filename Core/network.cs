using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;

namespace Core
{
    public class Network
    {
        private static StorageFolder storageFolder;
        public enum Response
        {
            DownloadToLocalFolder = 1,
            DownloadToRemoteFolder = 2,
            DownloadToTemporaryFolder = 3,
        }

        
        public async static Task<HttpResponseMessage> ReachURL(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));
            return response;
        }
        /// <summary>
        /// Get File And Download it.
        /// </summary>
        /// <param name="url">URL of the file</param>
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
        /// <param name="url">URL of the file</param>
        public async static Task<HttpResponseMessage> ReachHeader(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            httpResponse = await httpClient.SendRequestAsync(request);
            return httpResponse;
        }

        public async static Task<string> ReachStatusCode(string url)
        {
            HttpResponseMessage httpResponse = await ReachHeader(url);

        }
    }
}
