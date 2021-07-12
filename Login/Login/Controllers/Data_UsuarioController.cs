using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class Data_UsuarioController : Controller
    {
        // GET: Data_Usuario
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Data_UsuarioGrafico()
        {
            return PartialView();
        }
        public PartialViewResult VisualizarGraficoColecion_Usuario()
        {
            return PartialView();
        }
        public PartialViewResult ContenidoColección()
        {
            return PartialView();
        }
        public PartialViewResult Gráfico_Contenido_Colección()
        {
            return PartialView();
        }
        public PartialViewResult FuentesUsuario()
        {
            return PartialView();
        }
        public PartialViewResult Recursos_Usuario()
        {
            return PartialView();
        }
    }
}