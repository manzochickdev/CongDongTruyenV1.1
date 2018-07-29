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
    public sealed partial class FragmentLeft : UserControl
    {

        public FragmentLeft()
        {
            this.InitializeComponent();
            List<MenuItem> list = new List<MenuItem>();

            list.Add(new MenuItem(1, "TRANG CHỦ", "http://congdongtruyen.com/all/", "Assets/trangchu.png"));
            list.Add(new MenuItem(2, "TIÊN HIỆP", "http://congdongtruyen.com/tien-hiep/", "Assets/tienhiep.png"));
            list.Add(new MenuItem(3, "KIẾM HIỆP", "http://congdongtruyen.com/kiem-hiep/", "Assets/kiemhiep.png"));
            list.Add(new MenuItem(4, "NGÔN TÌNH", "http://congdongtruyen.com/ngon-tinh/", "Assets/ngontinh.png"));
            list.Add(new MenuItem(5, "ĐÔ THỊ", "http://congdongtruyen.com/do-thi/", "Assets/dothi.png"));
            list.Add(new MenuItem(6, "XUYÊN KHÔNG", "http://congdongtruyen.com/xuyen-khong/", "Assets/xuyenkhong.png"));
            list.Add(new MenuItem(7, "ƯA THÍCH", "", "Assets/uathich.png"));
            list.Add(new MenuItem(8, "VỀ TÁC GIẢ", "", ""));
            lvMenu.ItemsSource = list;


        }

        private void lvMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            MenuItem menuItem = (MenuItem)e.ClickedItem;
            if (menuItem.id < 7)
            {
                ((Frame)Window.Current.Content).Navigate(typeof(MainPage), menuItem);
            }
            else if(menuItem.id==7)                
            {
                ((Frame)Window.Current.Content).Navigate(typeof(FavoritePage), menuItem);
            }
            else ((Frame)Window.Current.Content).Navigate(typeof(AboutPage), menuItem);
        }
    }

    class MenuItem
    {
        public int id { get; set; }
        public String name { get; set; }
        public String path { get; set; }
        public String uri { get; set; }

        public MenuItem(int id, string name, string path,string uri)
        {
            this.id = id;
            this.name = name;
            this.path = path;
            this.uri = uri;
        }

        public MenuItem() { }
    }



}
