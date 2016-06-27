using Intense.Presentation;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Developer_Hub_For_UWP.Pages;
using Developer_Hub_For_UWP.Presentation;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.System.Profile;
using Windows.System;
using Windows.UI.Xaml.Navigation;

namespace Developer_Hub_For_UWP
{
    public sealed partial class Shell : UserControl
    {
        private bool isAltKeyPressed;
        private bool isControlKeyPressed;

        public Shell()
        {

            this.InitializeComponent();

            var applicationView = ApplicationView.GetForCurrentView();
            var titleBar = applicationView.TitleBar;
            titleBar.ButtonInactiveBackgroundColor = Colors.Black;
            titleBar.ButtonInactiveForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Black;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.InactiveBackgroundColor = Colors.Black;
            titleBar.InactiveForegroundColor = Colors.White;
            titleBar.BackgroundColor = Colors.Black;
            titleBar.ForegroundColor = Colors.White;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            var vm = new ShellViewModel();

            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("Home"), PageType = typeof(Home) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("UL"), PageType = typeof(Page2) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("SMI"), PageType = typeof(Page1) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("AG"), PageType = typeof(Page3) });
            vm.TopItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("LBI"), PageType = typeof(Page4) });

            vm.BottomItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("Settings"), PageType = typeof(SettingsPage) });
            vm.BottomItems.Add(new NavigationItem { Icon = "", DisplayName = loader.GetString("About"), PageType = typeof(AboutPage) });
            

            // select the first top item
            vm.SelectedItem = vm.TopItems.First();
            this.ViewModel = vm;

            this.Loaded += delegate { this.Focus(Windows.UI.Xaml.FocusState.Programmatic); };
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }

        private void Grid_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Menu) isAltKeyPressed = true;
            else if (isAltKeyPressed)
            {
                switch (e.Key)
                {
                    case VirtualKey.H: Frame.Navigate(typeof(Home), Frame); break;
                    case VirtualKey.U: Frame.Navigate(typeof(Page2), Frame); break;
                    case VirtualKey.F: Frame.Navigate(typeof(Page1), Frame); break;
                    case VirtualKey.A: Frame.Navigate(typeof(Page3), Frame); break;
                    case VirtualKey.B: Frame.Navigate(typeof(Page4), Frame); break;      
                }
            }
            if (e.Key == VirtualKey.Control) isControlKeyPressed = true;
            else if (isControlKeyPressed)
            {
                switch (e.Key)
                {
                    case VirtualKey.A: Frame.Navigate(typeof(AboutPage), Frame); break;
                    case VirtualKey.S: Frame.Navigate(typeof(SettingsPage), Frame); break;
                }
            }
        }
    }
}
