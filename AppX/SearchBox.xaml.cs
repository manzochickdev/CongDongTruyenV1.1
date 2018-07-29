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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppX
{
    public sealed partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            this.InitializeComponent();
        }

        private void tbSearch_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (tbSearch.Text.Equals("") || tbSearch.Text == null)
                {
                    MessageDialog md = new MessageDialog("Không được để trống");
                    md.ShowAsync();
                }
                else search(tbSearch.Text);
            }
        }

        void search(String keyword)
        {
            if (tbSearch.Text != null || !tbSearch.Equals(""))
            {
                String searchText = "http://congdongtruyen.com/actionsearch?txtsearch=" + keyword + "&go=";
                ((Frame)Window.Current.Content).Navigate(typeof(SearchPage), searchText);
            }

            else ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }
    }
}
