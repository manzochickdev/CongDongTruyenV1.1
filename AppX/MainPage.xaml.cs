using AppX.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Novel> listNovels = new ObservableCollection<Novel>();
        String root = "http://congdongtruyen.com/all/";
        String url;
        HtmlDocument htmlDoc;
        MenuItem mItem;

        public MainPage()
        {
            this.InitializeComponent();
            checkAppData();
            fragmentTop.btnPane.Click += BtnPane_Click;
            fragmentGridView.lvHomePage.Loaded += LvHomePage_Loaded;
            fragmentTop.btnSearch.Click += BtnSearch_Click;
            svLeft.IsPaneOpen = false;

        }

        enum Mode { Refresh,Update};
        int currentPage;

        async void getData(String url, Mode mode)
        {
            fragmentLoad.Visibility = Visibility.Visible;
            if (mode == Mode.Refresh)
            {
                listNovels.Clear();
            }
            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();
            try
            {
                htmlDoc = await htmlWeb.LoadFromWebAsync(url);
                HtmlNode _nod = htmlDoc.DocumentNode.SelectSingleNode(@"//ul[@class='homeListstory']");
                HtmlNodeCollection _mainNode = _nod.SelectNodes("li");
                foreach (var node in _mainNode)
                {
                    String name = node.SelectSingleNode("h3").SelectSingleNode("a").GetAttributeValue("title", null);
                    String imgUrl = node.SelectSingleNode("a").SelectSingleNode("img").GetAttributeValue("src", null);
                    String mainUrl = node.SelectSingleNode("h3").SelectSingleNode("a").GetAttributeValue("href", null);
                    listNovels.Add(new Novel(name, imgUrl, mainUrl));
                }

                fragmentGridView.lvHomePage.ItemsSource = listNovels;
                currentPage = Convert.ToInt16(htmlDoc.DocumentNode.SelectSingleNode(@"//a[@title='current-page']").InnerText);
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



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                mItem = (MenuItem)e.Parameter;
                url = mItem.path;
                fragmentTop.tblTitle.Text = mItem.name;
                getData(url, Mode.Refresh);

            }
            catch (Exception)
            {
                url = root;
                fragmentTop.tblTitle.Text = "TRANG CHỦ";
                getData(url, Mode.Refresh);
            }
            
        }

        async void checkAppData()
        {
            StorageFolder installedFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sfile = await installedFolder.CreateFileAsync("ListFavorite.txt", CreationCollisionOption.OpenIfExists);
        }


        private void BtnPane_Click(object sender, RoutedEventArgs e)
        {
            svLeft.IsPaneOpen = !svLeft.IsPaneOpen;
        }

        public ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer) return depObj as ScrollViewer;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

        private void LvHomePage_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer sv = GetScrollViewer(fragmentGridView.lvHomePage);
            sv.ViewChanged += sv_ViewChanged;
        }

        private void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer sv = GetScrollViewer(fragmentGridView.lvHomePage);
            var verticalOffsetvalue = sv.VerticalOffset;

            if (verticalOffsetvalue == sv.ScrollableHeight)
            {
                getData(url + (currentPage + 1), Mode.Update);
            }
        }

        private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 800)
            {
                
            }
            else
            {
                
                searchBox.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (searchBox.Visibility == Visibility.Visible)
            {
                searchBox.Visibility = Visibility.Collapsed;

            }
            else searchBox.Visibility = Visibility.Visible;
        }
    }
}
