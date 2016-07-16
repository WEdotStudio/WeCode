using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;

namespace Core
{
    public class Network
    {
        public enum Response
        {
            Null =0,
            Download=1
        }
        /// <summary>
        /// reach internet files and modify it.
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="num">Ways to operate the file</param>
        public async static Task<HttpResponseMessage> ReachFile(string url, Response way)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(url));
            if(way == Response.Download)
            {
                string filename = url.Split("/".ToCharArray()).Last();
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
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
            return response;
        }   
    }
}
