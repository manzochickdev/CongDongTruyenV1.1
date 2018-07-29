using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        
        public AboutPage()
        {
            this.InitializeComponent();

            fragmentTop.btnPane.Click += BtnPane_Click;
            fragmentTop.btnSearch.Click += BtnSearch_Click;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MenuItem menuItem = new MenuItem();
            menuItem = (MenuItem)e.Parameter;
            fragmentTop.tblTitle.Text = menuItem.name;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (searchBox.Visibility == Visibility.Visible)
            {
                searchBox.Visibility = Visibility.Collapsed;

            }
            else searchBox.Visibility = Visibility.Visible;
        }

        private void BtnPane_Click(object sender, RoutedEventArgs e)
        {
            svLeft.IsPaneOpen = !svLeft.IsPaneOpen;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            vb1.Width = e.NewSize.Width;
            if (e.NewSize.Width < 800)
            {
                svLeft.IsPaneOpen = false;
            }
            else
            {
                svLeft.IsPaneOpen = true;
                searchBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
