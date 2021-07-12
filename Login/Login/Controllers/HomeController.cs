using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Microsoft.AspNet.Identity;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.outs = Request.IsAuthenticated;
             if (Request.IsAuthenticated)
                {
                return View("Bienvenida");
                //return Redirect("/Usuario/Index");
            }

                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Correo.SendEmailAsync("ianmeza29@gmail.com", "Hola");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Bienvenida()
        {
            
            return View();
        }

        public ActionResult Mapa(int id = 13)
        {
            ViewBag.region = id;
            return View();
        }

        public string descripcion()
        {
            return APIShopify.BuscarDescripcion();
        }

    }
}
