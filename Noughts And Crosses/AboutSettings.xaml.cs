using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI.ApplicationSettings;
using Windows.System;

namespace Noughts_And_Crosses
{
    public sealed partial class AboutSettings : UserControl
    {
        public AboutSettings()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Parent is Popup)
                ((Popup)Parent).IsOpen = false;
            SettingsPane.Show();
        }

       
    }
}
