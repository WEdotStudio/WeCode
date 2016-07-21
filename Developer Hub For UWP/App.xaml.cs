using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Developer_Hub_For_UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }


        protected override void OnActivated(IActivatedEventArgs args)
        {
            // TODO: Initialize root frame just like in OnLaunched

            // Handle toast activation
            if (args.Kind == ActivationKind.ToastNotification)
            {
                var toastArgs = args as ToastNotificationActivatedEventArgs;
                ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
                // Get arguments corresponding to this activation;
                // When tapping the body of the toast caused this activation, the app receives the value of “launch” property of ;
                // When the activation is caused by using tapping on an action inside the toast, the app receives the value of “arguments” property of ; 
                var arguments = toastArgs.Argument;
                string[] arg = arguments.Split(Convert.ToChar("="));
                switch(arg[1])
                {
                    case "disableNoti":
                        _localSettings.Containers["Settings"].Values["IsUpdatePopupIgnored"] = true;
                        break;
                }
                // Navigate accordingly
            }

            // TODO: Handle other types of activation
        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // disabled, obscures the hamburger button, enable if you need it
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            var shell = Window.Current.Content as Shell;
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (shell == null)
            {
                // Create a Shell which navigates to the first page
                shell = new Shell();

                // hook-up shell root frame navigation events
                shell.RootFrame.NavigationFailed += OnNavigationFailed;
                shell.RootFrame.Navigated += OnNavigated;
     

                // set the Shell as content
                Window.Current.Content = shell;

                // listen for back button clicks (both soft- and hardware)
                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

                if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    HardwareButtons.BackPressed += OnBackPressed;
                }

                UpdateBackButtonVisibility();
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        // handle hardware back button press
        void OnBackPressed(object sender, BackPressedEventArgs e)
        {
            var shell = (Shell)Window.Current.Content;
            if (shell.RootFrame.CanGoBack) {
                e.Handled = true;
                shell.RootFrame.GoBack();
            }
        }

        // handle software back button press
        void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            var shell = (Shell)Window.Current.Content;
            if (shell.RootFrame.CanGoBack) {
                e.Handled = true;
                shell.RootFrame.GoBack();
            }
        }

        void OnNavigated(object sender, NavigationEventArgs e)
        {
            UpdateBackButtonVisibility();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void UpdateBackButtonVisibility()
        {
            var shell = (Shell)Window.Current.Content;

            var visibility = AppViewBackButtonVisibility.Collapsed;
            if (shell.RootFrame.CanGoBack) {
                visibility = AppViewBackButtonVisibility.Visible;
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = visibility;
        }
    }
}
