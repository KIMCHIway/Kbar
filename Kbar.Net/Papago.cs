using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// papago API Docs : https://developers.naver.com/docs/nmt/reference/

namespace Kbar.Net
{
    class Papago
    {
		public void CallPapago()
		{
			string url = "https://openapi.naver.com/v1/papago/n2mt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "bX5qcykEM6KdYV9cscfR");
            request.Headers.Add("X-Naver-Client-Secret", "VGzJd570sN  ");
            request.Method = "POST";
            string query = "오늘 날씨는 어떻습니까?";
            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=ko&target=en&text=" + query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            Console.WriteLine(text);
		}
    }
}
