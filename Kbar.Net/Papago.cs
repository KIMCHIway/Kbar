using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// papago API Docs : https://developers.naver.com/docs/nmt/reference/

namespace Kbar.Net
{
    class Papago
    {
        string sourceCode;
        string targetCode;
        string sourceText;

		public string Call_Papago(string sCode, string tCode, string sText)
		{
            // Save parameter
            sourceCode = sCode;
            targetCode = tCode;
            sourceText = sText;


            // Set API setting
            string url = "https://openapi.naver.com/v1/papago/n2mt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "bX5qcykEM6KdYV9cscfR");
            request.Headers.Add("X-Naver-Client-Secret", "VGzJd570sN");
            request.Method = "POST";

            // Handle data ( Source language , Target language , Source text )
            string data = string.Format("source={0}&target={1}&text={2}", sCode, tCode, sText);
            byte[] byteDataParams = Encoding.UTF8.GetBytes(data);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            // Target text
            string[] result = reader.ReadToEnd().Split(':');
            string tText = result[8].Split('"')[1];

            stream.Close();
            response.Close();
            reader.Close();

            return tText;
		}
    }
}
