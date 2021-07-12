using Login.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class ReadAPIController : Controller
    {
        // GET: ReadAPI
        public ActionResult Index(string id)
        { 
            /*
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var url = "https://5b4e5f28876dd9a9bdbc6b1e0b2d6de0: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@data-intelligence.myshopify.com/admin/api/2021-01/orders.json";
            url = "https://5b4e5f28876dd9a9bdbc6b1e0b2d6de0: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@data-intelligence.myshopify.com/admin/api/2020-01/orders.json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["X-Shopify-Access-Token"] = "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";
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return View();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            JObject json = JObject.Parse(responseBody);
                            ViewBag.json = json.GetValue("orders").Count();   //[0];
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.json = "Hola";
                // Handle error
            }
            */
            ViewBag.json = APIShopify.BuscarOrden(id);
            return View();
        }
    }
}