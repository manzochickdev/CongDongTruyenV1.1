using AppX.Models;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppX
{
    public sealed partial class FragmentGridView : UserControl
    {
        public FragmentGridView()
        {
            this.InitializeComponent();
        }

        private void lvHomePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 800 && e.NewSize.Width >= 400)
            {
                var panel = (ItemsWrapGrid)lvHomePage.ItemsPanelRoot;
                panel.ItemWidth = e.NewSize.Width / 3;
            }
            else if (e.NewSize.Width < 400)
            {
                var panel = (ItemsWrapGrid)lvHomePage.ItemsPanelRoot;
                panel.ItemWidth = 200;
            }
            else
            {
                var panel = (ItemsWrapGrid)lvHomePage.ItemsPanelRoot;
                panel.ItemWidth = e.NewSize.Width / 5;
            }
        }

        private void lvHomePage_ItemClick(object sender, ItemClickEventArgs e)
        {
            Novel pass = (Novel)e.ClickedItem;
            ((Frame)Window.Current.Content).Navigate(typeof(DetailPage), pass);
        }

    }
}
