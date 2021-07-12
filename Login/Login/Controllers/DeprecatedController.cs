using AplicacionBlanco.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class DeprecatedController : Controller
    {
        // GET: Deprecated
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult rpub()
        {
            return View();
        }

        public ActionResult Index2(int id = 1, string id2 = "mapadechile_engeochart_2021.csv")
        {
            ViewBag.grafico = id2;
            Graficos db = new Graficos();
            ViewBag.Resultado = db.BuscarGrafico(id);
            return View();
        }

        public ActionResult Index3()
        {
            ViewBag.Twitter = "@data_int";
            return View();
        }

        public ActionResult mapa(int id = 6, string id2 = "mapa")
        {
            ViewBag.grafico = id2;
            Graficos db = new Graficos();
            ViewBag.Resultado = db.BuscarGrafico(id);
            return View();
        }
    }
}