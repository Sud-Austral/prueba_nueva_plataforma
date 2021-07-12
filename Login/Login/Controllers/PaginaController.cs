using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace Login.Controllers
{
    [Authorize]
    public class PaginaController : Controller
    {
        Correos correos = new Correos();
        // GET: Pagina
        public ActionResult Index(string Hola)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.saludo = "Paso por aca";
            }
            //ViewBag.saludo = Hola;
            return View();
        }

        public ActionResult Validador(string url, string idOrden, string saturno, string nombre, string ordenCliente)
        //public string Validador(string url, string idOrden, string saturno, string nombre)
        {
            string ola = url;
            ViewBag.enlaceShopify = "https://dataintelligence.store" + nombre;
            if (!Util.fechaLimite(saturno, nombre))
            {    
                return View("errorTiempo");
            }
            //url = "https://sud-austral.maps.arcgis.com/apps/View/index.html?appid=8968a78812d644858916532e46c7dec3&extent=-120.5127,6.3355,-45.2343,37.5955";
            //var url = db.pedidos.Where(x => x.cliente.id == id && x.producto.id == producto).FirstOrDefault();
            string user = User.Identity.GetUserName();
            /*
            if(!APIShopify.ValidarCorreo(ordenCliente, user))
            {
                return View("errorCompra");
            }
            */
            //Ordenes ordenes = new Ordenes();
            /*
            if (!ordenes.validarCompra(user, url))
            {
                ViewBag.Lista = ordenes.productosPorUsuario("clentebanks0@gmail.com");
                ViewBag.User = user;
                return View("errorCompra");
            }
            */

            if (url == null)
            {
                return View("Restriccion");
            }
            if (url.Contains("showLayers"))
            {
                bool flag = true;
            }
            Session["url"] = url;
            Session.Timeout = 1;

            return Redirect("Dashboard");
        }

        public ActionResult Restriccion(int cliente=0, int producto=0)
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.url = (string)Session["url"];
            string user = User.Identity.GetUserName();
            
            /*
            ViewBag.user = user;
            List<string> aux = correos.correos;
            if (!aux.Contains(user))
            {
                return View("Restriccion");
            }
            */
            if (ViewBag.url == null)
            {
                return View("Restriccion");
            }
            return View();
        }
        [OutputCache(Duration =1, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult DashboardDataImpacto()
        {
            return View();
        }
        [AllowAnonymous]
        [OutputCache(Duration = 1, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult DashboardDataImpactoFree()
        {
            return View("DashboardDataImpacto");
        }
        [AllowAnonymous]
        public ActionResult DashboardArgis()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Trayectorias()
        {
            return View();
        }

        public ActionResult errorTiempo()
        {
            return View();
        }

        public ActionResult errorCompra()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult malomalo()
        {
            Ordenes ordenes = new Ordenes();
            ViewBag.Primo = ordenes.ordenes[0];
            ViewBag.Primo1 = ordenes.validarCompra("clentebanks0@gmail.com", "https://www.dataintelligence-group.com/salud-24/7-chile?prodId=8/0");
            //string url = "https://www.dataintelligence-group.com/213123241234";

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //var code = resp.StatusCode.ToString();
            //ViewBag.Code = code;
            return View();
        }

        public string ejemploRapido(string orden, string correo = "")
        {
            
            return APIShopify.ValidarCorreo(orden,correo).ToString();
        }


        public ActionResult ValidadorProductosInternos(string id = null)
        {
            List<Producto_Shopify> productos = (List<Producto_Shopify>)Session["Productos"];
            string url = productos.Where(x => x.ID == id).First().SKU;
            Session["url"] = url; //"https://www.c-sharpcorner.com/article/html-action-and-html-renderaction-in-Asp-Net-mvc/";
            //return Redirect("Dashboard");
            return Redirect("../Dashboard");
            //return productos.Where(x => x.ID == id).First().SKU;
            //return url;
            //return Dashboard();
        }


    }
}