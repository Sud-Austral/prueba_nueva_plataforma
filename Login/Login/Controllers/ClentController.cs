using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class ClentController : Controller
    {
        // GET: Clent
        public ActionResult Index(string fecha)
        {
           
            //ViewBag.Message = "Your application description page.";

            DateTime fechaInicio = DateTime.Parse(fecha);
            DateTime fechaActual = DateTime.Now;

             //Calculo de dias.
             TimeSpan td = fechaActual - fechaInicio;

            // Total de dias.
           //int differenceInDays = td.Days;   
            ViewBag.totalDias = Util.fechaLimite(fecha);
            if (ViewBag.totalDias)
            {
                return View();

            }
            else
            {
                return View("error");
            }



            
        }

        public ActionResult Error()
        {

            return View();
        }

        //Test producto
        public ActionResult Producto(string url, string idOrden)
        {
            return View();
        }
    }
}