using Intense.Presentation;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Developer_Hub_For_UWP.Pages;
using Developer_Hub_For_UWP.Presentation;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.System.Profile;

namespace Developer_Hub_For_UWP
{
    public sealed partial class Shell : UserControl
    {
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
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }
    }
}
