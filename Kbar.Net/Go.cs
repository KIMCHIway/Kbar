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
                    Connect_Chrome("https://www.facebook.com");
                    break;
                case "google":
                    Connect_Chrome("https://www.google.com");
                    break;
                case "youtube":
                    Connect_Chrome("https://www.youtube.com");
                    break;
                // NAVER
                case "n":
                case "naver":
                    Connect_Chrome("https://www.naver.com");
                    break;
                case "nd":
                    Connect_Chrome("https://dict.naver.com");
                    break;
                case "papago":
                case "nt":
                case "ntranslation":
                case "translation":
                    Connect_Chrome("https://papago.naver.com");
                    break;
                case "nmap":
                case "nm":
                    Connect_Chrome("https://map.naver.com");
                    break;
                case "daum":
                    Connect_Chrome("https://www.daum.net");
                    break;
                case "dc":
                    Connect_Chrome("https://www.dcinside.com");
                    break;
                case "dcpr":
                    Connect_Chrome("https://gall.dcinside.com/board/lists/?id=programming");
                    break;
            }
        }

        public void Call_NaverMap(string[] locations)
        {
            string location = Combine_NaverBlank(locations);
            string url = "https://beta.map.naver.com/search/" + location;
            Process.Start("Chrome.exe", url);
        }

        public void Call_GoogleMap(string[] locations)
        {
            string location = Combine_GoogleBlank(locations);
            string url = "https://www.google.com/maps/search/?api=1&query=" + location;
            Process.Start("Chrome.exe", url);
        }

        private string Combine_NaverBlank(string[] array)
        {
            string text = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i] + "%20";
            }

            return text;
        }

        private string Combine_GoogleBlank(string[] array)
        {
            string text = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i] + "+";
            }

            return text;
        }

        private void Connect_Chrome(string url)
        {
            Process.Start("Chrome.exe", url);
        }
    }
}
