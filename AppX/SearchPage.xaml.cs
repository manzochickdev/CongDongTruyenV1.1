using AppX.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        ObservableCollection<Novel> listNovels = new ObservableCollection<Novel>();
        HtmlDocument htmlDoc;

        String url;
        String realUrl;

        public SearchPage()
        {
            this.InitializeComponent();
            fragmentTop.btnPane.Click += BtnPane_Click;
            fragmentGridView.lvHomePage.Loaded += LvHomePage_Loaded;
            fragmentTop.btnSearch.Click += BtnSearch_Click;
            fragmentTop.tblTitle.Text = "TÌM KIẾM";
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

        int currentPage;
        enum Mode { Refresh, Update };
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

                if (_mainNode != null)
                {
                    foreach (var node in _mainNode)
                    {
                        String name = node.SelectSingleNode("h3").SelectSingleNode("a").GetAttributeValue("title", null);
                        String imgUrl = node.SelectSingleNode("a").SelectSingleNode("img").GetAttributeValue("src", null);
                        String mainUrl = node.SelectSingleNode("h3").SelectSingleNode("a").GetAttributeValue("href", null);
                        listNovels.Add(new Novel(name, imgUrl, mainUrl));
                    }

                    if (realUrl == null || realUrl.Equals(""))
                    {
                        realUrl = htmlDoc.DocumentNode.SelectSingleNode(@"//link[@rel='canonical']").GetAttributeValue("href", null);
                    }

                    fragmentGridView.lvHomePage.ItemsSource = listNovels;
                    currentPage = Convert.ToInt16(htmlDoc.DocumentNode.SelectSingleNode(@"//a[@title='current-page']").InnerText);

                }
                else
                {
                    MessageDialog md = new MessageDialog("Không tìm thấy kết quả");
                    md.ShowAsync();
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
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            url = (String)e.Parameter;
            getData(url, Mode.Refresh);
        }

        private void lvHomePage_ItemClick(object sender, ItemClickEventArgs e)
        {
            Novel pass = (Novel)e.ClickedItem;
            Frame.Navigate(typeof(DetailPage), pass);
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
                getData(realUrl + (currentPage + 1), Mode.Update);
            }
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
