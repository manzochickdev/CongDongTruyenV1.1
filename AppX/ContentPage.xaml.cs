 using AppX.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public sealed partial class ContentPage : Page
    {
        public ContentPage()
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

        String nextPage;
        String prePage;
        HtmlDocument htmlDoc;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String url = ((Chapter)e.Parameter).chapterUrl;
            getData(url);

        }

        void getInfo(HtmlDocument htmlDoc)
        {
            HtmlNode _nodTitle = htmlDoc.DocumentNode.SelectSingleNode(@"//h1[@class='title']");
            tblTitle.Text = _nodTitle.InnerText;
            HtmlNode _nodChapter = htmlDoc.DocumentNode.SelectSingleNode(@"//div[@class='author']");
            tblChapter.Text = _nodChapter.SelectSingleNode("h3").InnerText;

        }

        async void getData(String url) {
            fragmentLoad.Visibility = Visibility.Visible;
            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();
            try
            {
                htmlDoc = await htmlWeb.LoadFromWebAsync(url);
                getInfo(htmlDoc);
                HtmlNode _nod = htmlDoc.DocumentNode.SelectSingleNode(@"//div[@id='detailcontent']");
                String html = _nod.InnerHtml.Replace("\t", "");
                StringBuilder sb = new StringBuilder(html);
                sb.Replace("<br>", "\n");
                sb.Replace("&quot;", "\"");
                tblContent.Text = sb.ToString()+"\n\n\n\n\n";
                checkPageState(htmlDoc);
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

        void checkPageState(HtmlDocument htmlDoc)
        {
            HtmlNode _nodPre = htmlDoc.DocumentNode.SelectSingleNode(@"//a[@id='prechap']");
            if (_nodPre != null)
            {
                prePage = _nodPre.GetAttributeValue("href", null);
                previous.Visibility = Visibility.Visible;
            }
            else previous.Visibility = Visibility.Collapsed;

            HtmlNode _nodNext = htmlDoc.DocumentNode.SelectSingleNode(@"//a[@id='nextchap']");
            if (_nodNext != null)
            {
                nextPage = _nodNext.GetAttributeValue("href", null);
                next.Visibility = Visibility.Visible;
            }
            else next.Visibility = Visibility.Collapsed;
        }

        private void BtnPane_Click(object sender, RoutedEventArgs e)
        {
            svLeft.IsPaneOpen = !svLeft.IsPaneOpen;
        }

        private void zoomin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tblContent.FontSize += 2;
        }

        private void zoomOut_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (tblContent.FontSize >= 10)
            {
                tblContent.FontSize -= 2;
            }
        }

        private void previous_Tapped(object sender, TappedRoutedEventArgs e)
        {
            getData(prePage);
            scrollviewer.ChangeView(null, 0, null);
        }

        private void next_Tapped(object sender, TappedRoutedEventArgs e)
        {
            getData(nextPage);
            scrollviewer.ChangeView(null, 0, null);
        }

        private void setting_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (spControl.Visibility == Visibility.Collapsed)
            {
                spControl.Visibility = Visibility.Visible;
            }
            else spControl.Visibility = Visibility.Collapsed;
        }

        private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 800 )
            {
                setting.Visibility = Visibility.Visible;
                spControl.Visibility = Visibility.Collapsed;
                gridContent.Width = e.NewSize.Width;
            }
            else
            {
                setting.Visibility = Visibility.Collapsed;
                spControl.Visibility = Visibility.Visible;
                searchBox.Visibility = Visibility.Collapsed;
            }
        }

    }
}
