using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Login.Models
{
    public class APIShopify
    {
        public static JObject BuscarOrden(string orden)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //var url = "https://5b4e5f28876dd9a9bdbc6b1e0b2d6de0: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@data-intelligence.myshopify.com/admin/api/2021-01/orders.json";
            string url = "https://data-intelligence.myshopify.com/admin/api/2021-01/orders/" + orden + ".json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["X-Shopify-Access-Token"] = "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";
            //HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            JObject json = JObject.Parse(responseBody);
                            return json; //.GetValue("orders").Count();   //[0];
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        public static bool ValidarCorreo(string orden, string correo)
        {
            JObject json = BuscarOrden(orden);
            if(json == null)
            {
                return false;
            }
            return JObject.Parse(json.GetValue("order").ToString()).GetValue("email").ToString() == correo;
        }

        public static JObject BuscarOrdenes()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var url = "https://5b4e5f28876dd9a9bdbc6b1e0b2d6de0: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@data-intelligence.myshopify.com/admin/api/2021-01/orders.json";
            //string url = "https://data-intelligence.myshopify.com/admin/api/2021-01/orders/" + orden + ".json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["X-Shopify-Access-Token"] = "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";
            //HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            JObject json = JObject.Parse(responseBody);
                            return json; //.GetValue("orders").Count();   //[0];
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return null;
            }
        }
        public static List<JToken> BuscarOrdenesPorMail()
        {
            JObject json = BuscarOrdenes();
            //return JObject.Parse(json.GetValue("order").ToString());
            //return json.GetValue("orders").ToString();
            JArray categories = (JArray)json["orders"];
            //return categories.Select(c => (string)c).ToList(); 
            return categories.Where(c => (string)c["email"] == "clentebanks0@gmail.com").ToList(); //.Select(c => (string)c["email"] == "viviandrg7@gmail.com").ToList()[0];
            //return json;
            //mvcmacia@gmail.com
            //clentebanks0@gmail.com
            //lmonsalve22@gmail.com            
        }

        public static List<JToken> BuscarOrdenesPorMail(string correo)
        {
            JObject json = BuscarOrdenes();
            //return JObject.Parse(json.GetValue("order").ToString());
            //return json.GetValue("orders").ToString();
            JArray categories = (JArray)json["orders"];
            //return categories.Select(c => (string)c).ToList(); ;
            return categories.Where(c => (string)c["email"] == correo).ToList(); //.Select(c => (string)c["email"] == "viviandrg7@gmail.com").ToList()[0];
            //return categories.Where(c => (string)c["email"] == "clentebanks0@gmail.com").ToList(); //.Select(c => (string)c["email"] == "viviandrg7@gmail.com").ToList()[0];
            //return json;
        }

        public static JObject BuscarImagenes(string idProduct = "5997704741018")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var url = "https://5b4e5f28876dd9a9bdbc6b1e0b2d6de0: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@data-intelligence.myshopify.com/admin/api/2021-07/products/" + idProduct  + "/images.json";
            //var url = "https://data-intelligence.myshopify.com/admin/api/2021-01/products/" + idProduct + "/images.json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["X-Shopify-Access-Token"] = "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";
            //HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            JObject json = JObject.Parse(responseBody);
                            //return json; //.GetValue("orders").Count();   //[0];
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            return json;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                string error = ex.Message;
                //return url;
                return null;
                // Handle error
            }
            //return View();
        }

        public static string BuscarDescripcion(string idProduct = "5997704741018")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var url = "https://5b4e5f28876dd9a9bdbc6b1e0b2d6de0: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@data-intelligence.myshopify.com/admin/api/2021-07/products/" + idProduct + ".json";
            //var url = "https://data-intelligence.myshopify.com/admin/api/2021-01/products/" + idProduct + "/images.json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["X-Shopify-Access-Token"] = "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";
            //HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            JObject json = JObject.Parse(responseBody);
                            //return json; //.GetValue("orders").Count();   //[0];
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            return (string)json["product"]["body_html"];
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                string error = ex.Message;
                //return url;
                return null;
                // Handle error
            }
            //return View();
        }
    }
}