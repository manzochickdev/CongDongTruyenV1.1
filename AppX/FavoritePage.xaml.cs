using AppX.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class FavoritePage : Page
    {
        List<Novel> listNovels = new List<Novel>();

        public FavoritePage()
        {
            this.InitializeComponent();
            fragmentTop.btnPane.Click += BtnPane_Click;
            fragmentTop.btnSearch.Click += BtnSearch_Click;
            svLeft.IsPaneOpen = false;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (searchBox.Visibility == Visibility.Visible)
            {
                searchBox.Visibility = Visibility.Collapsed;

            }
            else searchBox.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MenuItem menuItem = new MenuItem();
            menuItem = (MenuItem)e.Parameter;
            fragmentTop.tblTitle.Text = menuItem.name;
            getData();
        }

        private async void getData()
        {
            fragmentLoad.Visibility = Visibility.Visible;
            StorageFolder installedFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile sfile = await installedFolder.GetFileAsync("ListFavorite.txt");
                string data = await FileIO.ReadTextAsync(sfile);
                if (data.Equals("") || data == null)
                {
                    MessageDialog md = new MessageDialog("Hiện không có truyện đã thích");
                    md.ShowAsync();
                }
                else
                {
                    listNovels = JsonConvert.DeserializeObject<List<Novel>>(data);
                    fragmentGridView.lvHomePage.ItemsSource = listNovels;
                }
            }
            catch (Exception)
            {
                MessageDialog md = new MessageDialog("Lỗi hệ thống , vui lòng thử lại sau");
                md.ShowAsync();
            }
            finally
            {
                fragmentLoad.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnPane_Click(object sender, RoutedEventArgs e)
        {
            svLeft.IsPaneOpen = !svLeft.IsPaneOpen;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 800)
            {
                
            }
            else
            {
                
                searchBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
