using Core.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Developer_Hub_For_UWP.Pages
{
    public sealed partial class Browser : Page
    {
        public Browser()
        {
            this.InitializeComponent();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            ToolTipService.SetToolTip(Cp1, loader.GetString("copy"));
            ToolTipService.SetToolTip(Cp2, loader.GetString("copy"));
            ToolTipService.SetToolTip(Cp3, loader.GetString("copy"));
            ToolTipService.SetToolTip(Cp4, loader.GetString("copy"));

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
            Character Item = e.Parameter as Character;
            string c = Item.Char;
            
            char[] myChar = c.ToCharArray();
            c = "";
            foreach(char chars in myChar){
                c += ((int)chars).ToString("X4");
            }
            //Segoe MDL2 Assets Database
           if(Item.Font == "Segoe MDL2 Assets")
            {
                //Get Info From Database
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
                //SMA Retrive Data
                foreach (string code in codes)
                {
                    //It exists in database
                    if (c == code)
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
                        FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022" + Item.Font + "\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                        codestring = codestring.ToLower();
                        Ucode.Text = "\\u" + code;
                        if (remark == "" || remark == null)
                        {
                            Rm.Text = "N/A";
                        }
                        else
                        {
                            Rm.Text = remark;
                        }
                    }
                    else
                    {
                        string codestring = c;
                        Rm.Text = "N/A";
                        int p = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                        Ficon_l.Text = ((char)p).ToString();
                        Ficon_m.Text = ((char)p).ToString();
                        Ficon_s.Text = ((char)p).ToString();
                        Ficon.Text = ((char)p).ToString();
                        Ftext.Text = "U+" + c;
                        FiconT.Text = "\u0026\u0023x" + codestring + "\u003B";
                        FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022" + Item.Font + "\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                        codestring = codestring.ToLower();
                        Ucode.Text = "\\u" + c;
                    }
                }
            }
           //Other Fonts
            else
            {
                FontFamily font = new FontFamily(Item.Font);
                Ficon.FontFamily = font;
                Ficon_l.FontFamily = font;
                Ficon_m.FontFamily = font;
                Ficon_s.FontFamily = font;
                string codestring = c;
                Rm.Text = "N/A";
                int p = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                Ficon_l.Text = ((char)p).ToString();
                Ficon_m.Text = ((char)p).ToString();
                Ficon_s.Text = ((char)p).ToString();
                Ficon.Text = ((char)p).ToString();
                Ftext.Text = "U+" + c;
                FiconT.Text = "\u0026\u0023x" + codestring + "\u003B";
                FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022" + Item.Font + "\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                codestring = codestring.ToLower();
                Ucode.Text = "\\u" + c;
                
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
