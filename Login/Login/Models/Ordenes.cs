using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Login.Models
{
    public class Ordenes
    {
        public List<Orden> ordenes { get; set; }

        public Ordenes()
        {
            this.ordenes = new List<Orden>();
            string url = "https://raw.githubusercontent.com/Sud-Austral/dropbox_to_github/main/orders.csv";
            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            var reader = new StreamReader(resp.GetResponseStream());

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                this.ordenes.Add(new Orden(values[9], values[23], values[18]));
                //this.ordenes.Add(new Orden(values[],values[],values[],values[],values[]));
                //string values_r = values[1].Replace("\"", "");

            }
        }

        public bool validarCompra(string email, string producto)
        {
            /*
            if(email == "lmonsalve22@gmail.com")
            {
                return true;
            }*/
            return this.ordenes.Where(x => x.correo == email && x.producto == producto).Count() > 0;
        }

        public List<Orden> productosPorUsuario(string email)
        {
            return this.ordenes.Where(x => x.correo == email).ToList();
        }
    }
}