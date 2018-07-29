using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppX.Models
{
    class Chapter
    {
        public String displayName { get; set; }
        public String chapterUrl { get; set; }
        public Novel parentNovel { get; set; }

        public Chapter(String displayName,String chapterUrl)
        {
            this.displayName = displayName;
            this.chapterUrl = chapterUrl;
        }
    }
}
