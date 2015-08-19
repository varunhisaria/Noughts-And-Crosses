using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//added for settings flyout
using Noughts_And_Crosses;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Noughts_And_Crosses
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //added for settings flyout
            _window = Window.Current.Bounds;
            Window.Current.SizeChanged += OnWindowSizeChanged;
            SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;
        }

        //added for settings flyout
        private Rect _window;
        private Popup _popUp;
        private const double WIDTH = 346;

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            _window = Window.Current.Bounds;
        }

        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            if(args.Request.ApplicationCommands.Count==0)
                args.Request.ApplicationCommands.Add(new SettingsCommand("help", "Help", Handler));
        }

        private void Handler(IUICommand command)
        {
            _popUp = new Popup
            {
                Width = WIDTH,
                Height = _window.Height,
                IsLightDismissEnabled = true,
                IsOpen = true
            };
            _popUp.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _popUp.Child = new AboutSettings { Width = WIDTH, Height = _window.Height };
            _popUp.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (_window.Width - WIDTH) : 0);
            _popUp.SetValue(Canvas.TopProperty, 0);
        }

        private void OnWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
                _popUp.IsOpen = false;
        }

        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }
        //end of settings flyout additive

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        private async void singleMode_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TapAnimation.Stop();
            TapAnimation.SetValue(Storyboard.TargetNameProperty, "singleMode");
            TapAnimation.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            MainPage.GlobalVars.GameMode = "single";
            this.Frame.Navigate(typeof(SinglePage));
            //this.Frame.Navigate(typeof(FileView), real.Text);
        }
        public static class GlobalVars
        {
            public static string Player1Name = "";
            public static string Player2Name = "";
            public static string Player1Avatar = "";
            public static string Player2Avatar = "";
            public static string GameMode = "";
            public static string amode = "";
        }

        private async void multiMode_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TapAnimation.Stop();
            TapAnimation.SetValue(Storyboard.TargetNameProperty, "multiMode");
            TapAnimation.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            MainPage.GlobalVars.GameMode = "multi";
            this.Frame.Navigate(typeof(MultiPage));
        }
    }
}
