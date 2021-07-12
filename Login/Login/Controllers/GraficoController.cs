using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class GraficoController : Controller
    {
        private graficosEntities dbGrafico = new graficosEntities();


        // GET: Grafico
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prueba(string id, string id2)
        {
            ViewBag.URL = id;
            ViewBag.grafico = id2;
            return View();
        }

        public ActionResult Luis1()
        {
            return View();
        }

        public ActionResult Luis2()
        {
            return View();
        }

        public ActionResult Grafico(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            ViewBag.id = graf.id;
            ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());
            return View();
        }

        public string GraficoString(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            //ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL())[4];
            string Hola = graf.GetURL();
            return Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());
            //return Hola;
        }

        public ActionResult Grafico2(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            ViewBag.id = graf.id;
            ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());

            return View();
        }
        public ActionResult PopUPClent(string id = "test.csv", string id2 = "grafico")
        {
            ViewBag.URL = id;
            ViewBag.grafico = id2;
            return View();
        }

        public ActionResult Grafico3(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            ViewBag.id = graf.id;
            ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());

            return View();
        }

        public ActionResult Grafico4(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            ViewBag.id = graf.id;
            ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());

            return View();
        }

        public ActionResult Grafico5(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            ViewBag.id = graf.id;
            ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());

            return View();
        }
        public ActionResult Grafico6(int id = 0)
        {

            DATA_GRAFICO graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.URL = graf.GetURL();
            ViewBag.id = graf.id;
            ViewBag.Data = Util.ObtenerTexto("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/" + graf.GetURL());

            return View();
        }
        public ActionResult Hector(string id = "Geo1.csv")
        {
            ViewBag.URL = id;
            return View();
        }

        public ActionResult Hector2(string id = "Geo1.csv")
        {
            ViewBag.URL = id;
            return View();
        }

        public ActionResult mapa(string id = "Geo_CL_provinces_.csv")
        {
            ViewBag.URL = id;
            return View();
        }

        public ActionResult readText()
        {
            /*
            List<string> listaTextoP = s.Split(new[] { "\n" },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
            */
            //ViewBag.URL = s.Split(new[] { "\n" }).Length; //"áéíóú";
            //ViewBag.URL = listaTextoP.Count();
            return View();
        }
    }
}