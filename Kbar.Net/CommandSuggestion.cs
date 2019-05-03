using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbar.Net
{
    class CommandSuggestion
    {
        public IReadOnlyDictionary<string, string> modules = new Dictionary<string, string>()
        {
            {"papago", "Use Naver NMT Translator"},
            {"nt", "Use Naver NMT Translator" },
            {"ntranslation", "Use Naver NMT Translator" },
            {"translation", "Use Naver NMT Translator" },
            //
            {"gt", "Use Google SMT Translator" },
            {"gtranslation", "Use Google SMT Translator" },
            //
            {"nm", "Open Naver Map" },
            {"nmap", "Open Naver Map" },
            //
            {"gm", "Open Google Map" },
            {"gmap", "Open Google Map" },
            {"map", "Open Google Map" },
            //
            {"go", "Connect Website directly" },
            //
            {"calc", "Calculate formula with [+ - * / ( ) ]" },
            {"calculator", "Calculate formula with [+ - * / ( ) ]" }
            //
            //{"wiki", "Wiki Pedia" },
            //
            //{"money", "Exchange Rate" },
            //{"currency", "Exchange Rate" },
            //
            //{"weather", "World Weather" },
            //
            //{"time", "World Time" },
            //
            //{"note", "Notepad" },
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

        public IReadOnlyDictionary<string, string> go = new Dictionary<string, string>()
        {
            {"facebook", "Facebook" },
            {"g", "Google Main" },
            {"google", "Google Main" },
            {"youtube", "Youtube" },
            {"n", "Naver Main" },
            {"naver", "Naver Main" },
            {"nd", "Naver Dictionary Home" },
            {"papago", "Naver NMT Translator" },
            {"ntranslation", "Naver NMT Translator" },
            {"nmap", "Naver Map" },
            {"daum", "Daum Main" },
            {"dc", "Dcinside Main" },
            {"dcpr", "Dcinside Programming Gallery" }
        };


        public Dictionary<string, string> Search_Command(string text)
        {
            // Search
            Dictionary<string, string> matchingValue = (from module in modules
                                                        where module.Key.StartsWith(text)
                                                        select module).ToDictionary(i => i.Key, i => i.Value);

            if (matchingValue.Count() > 0)
            {
                return matchingValue;
            }
            else return null;
        }

        public Dictionary<string, string> Search_Command(string module, string text)
        {
            // Search
            Dictionary<string, string> matchingValue = null;
            switch (module)
            {
                // Papago Module
                case "papago":
                case "nt":
                case "ntranslation":
                case "translation":
                    matchingValue = (from command in papago
                                     where command.Key.StartsWith(text)
                                     select command).ToDictionary(i => i.Key, i => i.Value);
                    break;

                // Naver Dictionary Module
                case "nd":
                case "ndictionary":
                case "dictionary":
                    matchingValue = (from command in naverDictionary
                                     where command.Key.StartsWith(text)
                                     select command).ToDictionary(i => i.Key, i => i.Value);
                    break;
                case "go":
                    matchingValue = (from command in go
                                     where command.Key.StartsWith(text)
                                     select command).ToDictionary(i => i.Key, i => i.Value);
                    break;
                // if there is no module name that is related
                default:
                    return null;
            }

            if (matchingValue.Count() > 0)
            {
                return matchingValue;
            }
            else return null;
        }
    }
}
