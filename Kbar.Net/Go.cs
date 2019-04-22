using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbar.Net
{
    class Go
    {
        public void Call_Go(string site)
        {
            switch (site.ToLower())
            {
                case "facebook":
                    Connect_Chrome("www.facebook.com");
                    break;
                case "google":
                    Connect_Chrome("www.google.com");
                    break;
                case "youtube":
                    Connect_Chrome("www.youtube.com");
                    break;
                // NAVER
                case "n":
                case "naver":
                    Connect_Chrome("www.naver.com");
                    break;
                case "nd":
                    Connect_Chrome("dict.naver.com");
                    break;
                case "papago":
                case "nt":
                case "ntranslation":
                case "translation":
                    Connect_Chrome("papago.naver.com");
                    break;
                case "nmap":
                case "nm":
                    Connect_Chrome("map.naver.com");
                    break;
                case "daum":
                    Connect_Chrome("www.daum.net");
                    break;
                case "dc":
                    Connect_Chrome("www.dcinside.com");
                    break;
                case "dcpr":
                    Connect_Chrome("gall.dcinside.com/board/lists/?id=programming");
                    break;
            }
        }

        private void Connect_Chrome(string url)
        {
            Process.Start("Chrome.exe", url);
        }
    }
}
