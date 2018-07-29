using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppX.Models
{
    class Novel
    {
        public String name { get; set; }
        public String imgUrl { get; set; }
        public String mainUrl { get; set; }

        public Novel(String name, String imgUrl,String mainUrl)
        {
            this.name = name;
            this.imgUrl = imgUrl;
            this.mainUrl = mainUrl;
        }

    }
}
