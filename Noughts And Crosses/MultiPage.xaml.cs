using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Noughts_And_Crosses
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MultiPage : Noughts_And_Crosses.Common.LayoutAwarePage
    {
        public MultiPage()
        {
            this.InitializeComponent();
            Player1Name.Text = "Player 1";
            Player2Name.Text = "Player 2";
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void zero1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (mode.IsOn == false)
            {
                zero1.Foreground = new SolidColorBrush(Colors.Green);
                axe1.Foreground = new SolidColorBrush(Colors.Red);
                zero2.Foreground = new SolidColorBrush(Colors.Red);
                axe2.Foreground = new SolidColorBrush(Colors.Green);
            }
        }

        private void axe1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (mode.IsOn == false)
            {
                zero1.Foreground = new SolidColorBrush(Colors.Red);
                axe1.Foreground = new SolidColorBrush(Colors.Green);
                zero2.Foreground = new SolidColorBrush(Colors.Green);
                axe2.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void zero2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (mode.IsOn == false)
            {
                zero1.Foreground = new SolidColorBrush(Colors.Red);
                axe1.Foreground = new SolidColorBrush(Colors.Green);
                zero2.Foreground = new SolidColorBrush(Colors.Green);
                axe2.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void axe2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (mode.IsOn == false)
            {
                zero1.Foreground = new SolidColorBrush(Colors.Green);
                axe1.Foreground = new SolidColorBrush(Colors.Red);
                zero2.Foreground = new SolidColorBrush(Colors.Red);
                axe2.Foreground = new SolidColorBrush(Colors.Green);
            }
        }

        private async void done_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TapAnimation.Stop();
            TapAnimation.SetValue(Storyboard.TargetNameProperty, "done");
            TapAnimation.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            MessageDialog err1 = new MessageDialog("Both the names can't be same", "Noughts And Crosses");
            MessageDialog err2 = new MessageDialog("Please choose a Avatar", "Noughts And Crosses");
            int chk = 1;
            MainPage.GlobalVars.Player1Name = Player1Name.Text;
            MainPage.GlobalVars.Player2Name = Player2Name.Text;
            if (Player1Name.Text == "")
                MainPage.GlobalVars.Player1Name = "Player 1";
            if (Player2Name.Text == "")
                MainPage.GlobalVars.Player2Name = "Player 2";
            if (Player1Name.Text == Player2Name.Text)
            {
                await err1.ShowAsync();
                chk = 0;
            }
            SolidColorBrush az = zero1.Foreground as SolidColorBrush;
            SolidColorBrush ax = axe1.Foreground as SolidColorBrush;
            if (az.Color == Windows.UI.Colors.Green && mode.IsOn==false)
            {
                MainPage.GlobalVars.Player1Avatar = "O";
                MainPage.GlobalVars.Player2Avatar = "X";
            }
            else if (ax.Color == Windows.UI.Colors.Green && mode.IsOn==false)
            {
                MainPage.GlobalVars.Player1Avatar = "X";
                MainPage.GlobalVars.Player2Avatar = "O";
            }
            else if(mode.IsOn==false)
            {
                await err2.ShowAsync();
                chk = 0;
            }
            else if (mode.IsOn == true)
            {
                MainPage.GlobalVars.Player1Avatar = "O";
                MainPage.GlobalVars.Player2Avatar = "X";
            }
            if (chk == 1)
            {
                if (mode.IsOn == true)
                    MainPage.GlobalVars.amode = "auto";
                else
                    MainPage.GlobalVars.amode = "manual";
                this.Frame.Navigate(typeof(GamePage));
            }
        }

        private async void cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TapAnimation.Stop();
            TapAnimation.SetValue(Storyboard.TargetNameProperty, "cancel");
            TapAnimation.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.1)); Player1Name.Text = "Player 1";
            Player2Name.Text = "Player 2";
            zero1.Foreground = new SolidColorBrush(Colors.Red);
            axe1.Foreground = new SolidColorBrush(Colors.Red);
            zero2.Foreground = new SolidColorBrush(Colors.Red);
            axe2.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void mode_Toggled(object sender, RoutedEventArgs e)
        {
            if (mode.IsOn == false)
            {
                zero1.Foreground = new SolidColorBrush(Colors.Red);
                axe1.Foreground = new SolidColorBrush(Colors.Red);
                zero2.Foreground = new SolidColorBrush(Colors.Red);
                axe2.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                zero1.Foreground = new SolidColorBrush(Colors.Gray);
                axe1.Foreground = new SolidColorBrush(Colors.Gray);
                zero2.Foreground = new SolidColorBrush(Colors.Gray);
                axe2.Foreground = new SolidColorBrush(Colors.Gray); 
            }
        }
        private async void home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TapAnimation.Stop();
            TapAnimation.SetValue(Storyboard.TargetNameProperty, "home");
            TapAnimation.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            this.Frame.Navigate(typeof(MainPage));
        }

        private void tb1c_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Player1Name.Text = "";
            Player1Name.Focus(Windows.UI.Xaml.FocusState.Pointer);
        }

        private void tb2c_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Player2Name.Text = "";
            Player2Name.Focus(Windows.UI.Xaml.FocusState.Pointer);
        }
    }
}
