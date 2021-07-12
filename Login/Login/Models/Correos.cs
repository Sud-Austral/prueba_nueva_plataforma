using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Login.Models
{
    public class Correos
    {
        public List<string> correos { get; set; }

        public Correos()
        {
            this.correos = new List<string>();
            this.correos.Add("lmonsalve22@gmail.com");
            string url = "https://raw.githubusercontent.com/Sud-Austral/dropbox_to_github/main/customers.csv";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            var reader = new StreamReader(resp.GetResponseStream());

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                this.correos.Add(values[10]);
                //string values_r = values[1].Replace("\"", "");
                
            }

        }
    }
}