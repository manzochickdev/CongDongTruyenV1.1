using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppX
{
    public sealed partial class FragmentTop : UserControl
    {

        public FragmentTop()
        {
            this.InitializeComponent();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 800)
            {
                searchBox.Visibility = Visibility.Collapsed;
                btnSearch.IsHitTestVisible = true;
                btnSearchIcon.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                btnSearchIcon.FontSize = 35;

            }
            else
            {
                searchBox.Visibility = Visibility.Visible;
                btnSearch.IsHitTestVisible = false;
                btnSearchIcon.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                btnSearchIcon.FontSize = 30;

            }
        }
    }


}
