using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbar.Net
{
    class CommandList
    {
        public IReadOnlyDictionary<string, string> modules = new Dictionary<string, string>()
        {
            {"papago", "Naver NMT Translation"},
            {"nt", "Naver NMT Translation" },
            {"ntranslation", "Naver NMT Translation" },
            {"translation", "Naver NMT Translation" },
            //
            {"gt", "Google SMT Translation" },
            {"gtranslation", "Google SMT Translation" },
            //
            {"nm", "Naver Map" },
            {"nmap", "Naver Map" },
            //
            {"gm", "Google Map" },
            {"gmap", "Google Map" },
            {"map", "Google Map" },
            //
            {"wiki", "Wiki Pedia" },
            //
            {"money", "Exchange Rate" },
            {"currency", "Exchange Rate" },
            //
            {"weather", "World Weather" },
            //
            {"time", "World Time" },
            //
            {"note", "Notepad" },
        };

        public IReadOnlyDictionary<string, string> papago = new Dictionary<string, string>()
        {
            {"ko", "Korean" },
            {"en", "English" },
            {"ja", "Japanese" },
            {"zh-cn", "Chinese-simplified" },
            {"ch-cn", "Chinese-simplified" },
            {"zh-tw", "Chinese-traditional" },
            {"ch-tw", "Chinese-traditional" },
            {"vi", "Vietnamese" },
            {"id", "Indonesian" },
            {"in", "Indonesian" },
            {"th", "Thai" },
            {"de", "German" },
            {"ge", "German" },
            {"ru", "Russian" },
            {"es", "Spain" },
            {"sp", "Spain" },
            {"it", "Italy" },
            {"fr", "France" }
        };

        public IReadOnlyDictionary<string, string> naverDictionary = new Dictionary<string, string>()
        {
            {"ko", "Korea Dictionary" },
            {"en", "English Dictionary" },
            {"enen", "English-English Dictionary" },
            {"ja", "Japanese Dictionary" },
            {"ch", "Chinese Dictionary" },
            {"zh", "Chinese Dictionary" },
            {"cc", "Chinese Character Dictionary" },
            {"fr", "France Dictionary" },
            {"vi", "Vietamese Dictionary" },
            {"de", "German Dictionary" },
            {"ge", "German Dictionary" },
            {"es", "Spain Dictionary" },
            {"sp", "Spain Dictionary" }
        };


    }
}
