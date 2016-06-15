using System;
using System.Collections.Generic;
using System.IO;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Developer_Hub_For_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Browser : Page
    {
        public Browser()
        {
            this.InitializeComponent();

            var applicationView = ApplicationView.GetForCurrentView();
            var titleBar = applicationView.TitleBar;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.White;

            Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string c = e.Parameter.ToString();
            List<string> codes = new List<string>();
            List<string> names = new List<string>();
            List<string> remarks = new List<string>();
            Uri uri = new Uri("ms-appx:///Assets/Data/data.csv");
            var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
            using (var storageStream = await storageFile.OpenReadAsync())
            {
                using (Stream stream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                       
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            codes.Add(values[0]);
                            names.Add(values[1]);
                            remarks.Add(values[2]);
                        }
                    }
                }
            }
            char[] myChar = c.ToCharArray();
            c = "";
            foreach(char chars in myChar){
                c += ((int)chars).ToString("X4");
            }
           
            foreach (string code in codes) 
            {
                if(c==code)
                {
                    int count = codes.FindIndex(n => n == c);
                    string codestring = codes[count];
                    string remark = remarks[count];
                    int p = int.Parse(code, System.Globalization.NumberStyles.HexNumber);
                    Ficon_l.Text = ((char)p).ToString();
                    Ficon_m.Text = ((char)p).ToString();
                    Ficon_s.Text = ((char)p).ToString();
                    Ficon.Text = ((char)p).ToString();
                    Ftext.Text = names[count];
                    FiconT.Text = "\u0026\u0023x" + codestring + "\u003B";
                    FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022Segoe\u0020MDL2\u0020Assets\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                    codestring = codestring.ToLower();
                    Ucode.Text = "\\u" + code;
                    if (remark =="" ||remark == null)
                    {
                        Rm.Text = "N/A";
                    }
                    else
                    {
                        Rm.Text = remark;
                    }

                }
            }
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            copy(Ficon.Text);
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            copy(FiconT.Text);
        }

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            copy(Ucode.Text);
        }
        public void copy(String a)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(a);
            Clipboard.SetContent(dataPackage);
        }

        private void Button_Click_3(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            copy(FiconS.Text);
        }
    }
}
