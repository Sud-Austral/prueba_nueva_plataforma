using Login.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Login.Controllers
{   
    
    public class ProductoController : Controller
    {
        // GET: Producto_Shopify
        public ActionResult Index()
        {
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            ViewBag.User = User.Identity.GetUserName();
            //ViewBag.Resultado = APIShopify.BuscarOrdenes();
            //ViewBag.Resultado = APIShopify.BuscarOrdenesPorMail();
            //var test = APIShopify.BuscarOrdenesPorMail();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail("lmonsalve22@gmail.com"))
            {
                foreach (var item2 in item["line_items"])
                {
                    try
                    {
                        productos.Add(new Producto_Shopify(item2, (string)item["order_status_url"], item));
                    }
                    catch (Exception)
                    {

                        string hola = "";
                    }
                    //productos.Add(new Producto_Shopify(item2,(string)item["order_status_url"],item));
                }
            }
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            ViewBag.Resultado = productos;
            return View();
        }
        
        public ActionResult Elecciones_HN()
        {
            ViewBag.url = "https://odooutil.azurewebsites.net/design/eleccioneshn";

            return View();
        }

        public string Producto()
        {
            //return APIShopify.BuscarOrden("5997704741018");
            JObject jObject = APIShopify.BuscarImagenes("5997704741018");
            JArray jArray = (JArray)jObject["images"];

            return (string)jArray[0]["src"];
        }

        public JToken BuscarOrdenesPorMail()
        {
            JObject json = APIShopify.BuscarOrdenes();
            JArray categories = (JArray)json["orders"];
            return categories.Where(c => (string)c["email"] == "lmonsalve22@gmail.com").ToList()[0];
        }
    }
}