using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class DataController : Controller
    {
        private graficosEntities dbGrafico = new graficosEntities();
        public DataController()
        {

        }
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaginaBusqueda(string id = "1")
        {
            ViewBag.palabra = id;
            IEnumerable<DATA_GRAFICO> union = UtilBusqueda.PaginaBusqueda(id);
            if (union.Count() == 0)
            {
                ViewBag.Concepto = id;
                return View("No_Resultado");
            }
            ViewBag.Resultado = union;

            List<string> Paises = new List<string>();
            List<string> Escala = new List<string>();
            List<string> TipoGrafico = new List<string>();
            List<string> Temporalidad = new List<string>();
            List<string> Producto = new List<string>();
            List<string> Industria = new List<string>();
            List<string> Sector = new List<string>();
            List<string> Categoria = new List<string>();
            List<string> Parametro = new List<string>();
            foreach (var item in union)
            {
                if (!Paises.Contains(item.TERRITORIO.auxiliar))
                {
                    Paises.Add(item.TERRITORIO.auxiliar);
                }
                if (!Escala.Contains(item.TERRITORIO.nombre + " - " + item.TERRITORIO.auxiliar))
                {
                    Escala.Add(item.TERRITORIO.nombre + " - " + item.TERRITORIO.auxiliar);
                }
                if (!TipoGrafico.Contains(item.TIPO_GRAFICO.nombre))
                {
                    TipoGrafico.Add(item.TIPO_GRAFICO.nombre);
                }
                if (!Temporalidad.Contains(item.TEMPORALIDAD.nombre))
                {
                    Temporalidad.Add(item.TEMPORALIDAD.nombre);
                }
                if (!Producto.Contains(item.CATEGORIA.PRODUCTO.nombre))
                {
                    Producto.Add(item.CATEGORIA.PRODUCTO.nombre);
                }
                if (!Industria.Contains(item.CATEGORIA.PRODUCTO.SECTOR.INDUSTRIA.nombre))
                {
                    Industria.Add(item.CATEGORIA.PRODUCTO.SECTOR.INDUSTRIA.nombre);
                }
                if (!Sector.Contains(item.CATEGORIA.PRODUCTO.SECTOR.nombre))
                {
                    Sector.Add(item.CATEGORIA.PRODUCTO.SECTOR.nombre);
                }

                if (!Categoria.Contains(item.CATEGORIA.nombre))
                {
                    Categoria.Add(item.CATEGORIA.nombre);
                }
                if (!Parametro.Contains(item.PARAMETRO.nombre))
                {
                    Parametro.Add(item.PARAMETRO.nombre);
                }
            }
            ViewBag.Paises = Paises;
            ViewBag.Escala = Escala;
            ViewBag.TipoGrafico = TipoGrafico;
            ViewBag.Temporalidad = Temporalidad;
            ViewBag.Producto = Producto;
            ViewBag.Industria = Industria;
            ViewBag.Sector = Sector;
            ViewBag.Categoria = Categoria;
            ViewBag.Parametro = Parametro;
            return View();
        }

        public PartialViewResult VisualizarGraficos(decimal id = 1234)
        {
            ViewBag.time1 = DateTime.Now;
            var rand = new Random();
            DATA_GRAFICO graf = new DATA_GRAFICO();
            try
            {
                graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            }
            catch (Exception)
            {
                graf = null;
            }
            if (graf.TIPO_GRAFICO_id > 1 || graf == null)
            {
                var listaGraficoAuxiliar = dbGrafico.DATA_GRAFICO.Where(x => x.TIPO_GRAFICO_id < 3).ToList();
                graf = listaGraficoAuxiliar[rand.Next(listaGraficoAuxiliar.Count)];
            }
            ViewBag.Elemento = graf;//graficos
            // var listaAsociado = dbGrafico.PRODUCTO.Where(x => x.SECTOR_id == graf.CATEGORIA.PRODUCTO.SECTOR_id).ToList();
            //var listaAsociado = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO.SECTOR_id == graf.CATEGORIA.PRODUCTO.SECTOR_id).ToList();

            //List<int> aux = new List<int>();
            //for (int i = 0; i < 50; i++)
            //{
            //    aux.Add(rand.Next(dbGrafico.DATA_GRAFICO.Min(x => x.id), dbGrafico.DATA_GRAFICO.Max(x => x.id)));
            //}
            //var Graficos = dbGrafico.DATA_GRAFICO.Where(x => aux.Contains(x.id)).ToList();
            //ViewBag.Graficos = Graficos;//carrusel
            ViewBag.time2 = DateTime.Now;
            return PartialView();
        }
        //COLECCIONES
        public PartialViewResult Colecciones()
        {
            return PartialView();
        }
        public PartialViewResult Agricultura()
        {
            return PartialView();
        }
        public PartialViewResult Glaciares()
        {
            return PartialView();
        }
        public PartialViewResult Ganaderia()
        {
            return PartialView();
        }
        public PartialViewResult Salud_Enfermedades()
        {
            return PartialView();
        }
        public PartialViewResult Violencia()
        {
            return PartialView();
        }
        public PartialViewResult Pesca()
        {
            return PartialView();
        }
        public PartialViewResult Educacion()
        {
            return PartialView();
        }
        public PartialViewResult Emisiones()
        {
            return PartialView();
        }
        public PartialViewResult Salud_Pandemia()
        {
            return PartialView();
        }
        public PartialViewResult Elecciones()
        {
            return PartialView();
        }
        public PartialViewResult Contaminacion()
        {
            return PartialView();
        }
        public PartialViewResult IngresosCasen()
        {
            return PartialView();
        }
        //FIN COLECCIONES

        public PartialViewResult Visualizar_colección()
        {
            return PartialView();
        }
        public PartialViewResult BusquedaColecciónUsuario()
        {
            return PartialView();
        }
        public PartialViewResult IndexProductos()
        {
            return PartialView();
        }

    }


}