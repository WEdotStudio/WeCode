using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Core
{
    public class Network
    {
        /// <summary>
        /// Reach For Internet Files
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="num">0 for just reach the message, 1 for download to local folder Data</param>
        public async static Task DownloadFile(string url, int num)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(url));
            if(num == 1)
            {
                string filename = url.Split("/".ToCharArray()).Last();
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
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
        }
        private async void TransferToStorage()
        {
            // Has the file been copied already?
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync("api.json");
                // No exception means it exists
                return;
            }
            catch (System.IO.FileNotFoundException)
            {
                // The file obviously doesn't exist

            }
            // Cant await inside catch, but this works anyway
            StorageFile stopfile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/api.json"));
            await stopfile.CopyAsync(ApplicationData.Current.LocalFolder);
        }
    }
}
