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

        public void Call_NaverDictionary(string language, string[] textArray)
        {
            string text = Combine_NaverBlank(textArray);

            switch (language.ToLower())
            {
                case "en":
                    Connect_Chrome("https://endic.naver.com/search.nhn?query=" + text);
                    break;
                case "enen":
                    Connect_Chrome("https://dict.naver.com/enendict/#/search?query=" + text);
                    break;
                case "ko":
                    Connect_Chrome("https://ko.dict.naver.com/search.nhn?query=" + text);
                    break;
                case "cc":
                    Connect_Chrome("https://hanja.dict.naver.com/search.nhn?query=" + text);
                    break;
                case "ja":
                    Connect_Chrome("https://ja.dict.naver.com/search.nhn?query=" + text);
                    break;
                case "ch":
                case "zh":
                    Connect_Chrome("https://zh.dict.naver.com/search.nhn?query=" + text);
                    break;
                case "fr":
                    Connect_Chrome("https://dict.naver.com/frkodict/#/search?query=" + text);
                    break;
                case "sp":
                case "es":
                    Connect_Chrome("https://dict.naver.com/eskodict/#/search?query=" + text);
                    break;
                case "ge":
                case "de":
                    Connect_Chrome("https://dict.naver.com/dekodict/#/search?query=" + text);
                    break;
                case "vi":
                    Connect_Chrome("https://dict.naver.com/vikodict/#/search?query=" + text);
                    break;
            }
        }

        public void Call_NaverMap(string[] locationArray)
        {
            string location = Combine_NaverBlank(locationArray);
            string url = "https://beta.map.naver.com/search/" + location;
            Connect_Chrome(url);
        }

        public void Call_GoogleMap(string[] locationArray)
        {
            string location = Combine_GoogleBlank(locationArray);
            string url = "https://www.google.com/maps/search/?api=1&query=" + location;
            Connect_Chrome(url);
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

        public void Connect_Chrome(string url)
        {
            Process.Start("Chrome.exe", url);
        }
    }
}
