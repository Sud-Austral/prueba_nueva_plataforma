using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AplicacionBlanco.Controllers
{
    public class InfoController : Controller
    {
        private graficosEntities dbGrafico = new graficosEntities();
        public InfoController()
        {
            
        }

        // GET: Info
        public ActionResult Index(int id = 100, string id2 = "grafico", string id3 = "Geo_CL_provinces_.csv")
        {
            var rand = new Random();
            ViewBag.grafico = id2;
            ViewBag.file = id3;
            Graficos db = new Graficos();
            ViewBag.Resultado = null;  //db.BuscarGrafico(id);
            ViewBag.menu = dbGrafico.INDUSTRIA.ToList();
            ViewBag.menu2 = dbGrafico.SECTOR.ToList();
            ViewBag.menu3 = dbGrafico.PRODUCTO.ToList();
            DATA_GRAFICO graf = new DATA_GRAFICO();
            try
            {
                graf = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            }
            catch (Exception)
            {
                graf = null;
            } 
            if(graf.TIPO_GRAFICO_id > 1 || graf == null)
            {
                var listaGraficoAuxiliar = dbGrafico.DATA_GRAFICO.Where(x => x.TIPO_GRAFICO_id < 3).ToList();
                graf = listaGraficoAuxiliar[rand.Next(listaGraficoAuxiliar.Count)];
            }            
            ViewBag.Elemento = graf;
            // var listaAsociado = dbGrafico.PRODUCTO.Where(x => x.SECTOR_id == graf.CATEGORIA.PRODUCTO.SECTOR_id).ToList();
            var listaAsociado = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO.SECTOR_id == graf.CATEGORIA.PRODUCTO.SECTOR_id).ToList();
            ViewBag.listaAsociado = listaAsociado;
            /* var listaOtrosContenidos = dbGrafico.CATEGORIA.Where(x => x.PRODUCTO_id == graf.CATEGORIA.PRODUCTO_id).ToList(); */
            //List<int> idproductos = new List<int>();
            
            List<CATEGORIA> listaCategorias = new List<CATEGORIA>();
            foreach (var item in dbGrafico.INDUSTRIA)
            {
               var listcatAuxiliar = dbGrafico.CATEGORIA.Where(x => x.PRODUCTO.SECTOR.INDUSTRIA_id == item.id).Take(10).ToList();
                try
                {
                    CATEGORIA catAuxiliar = listcatAuxiliar[rand.Next(listcatAuxiliar.Count)];
                    listaCategorias.Add(catAuxiliar);
                }
                catch (Exception)
                {                    
                }                
            }
            var listaOtrosContenidos = listaCategorias;  
            ViewBag.listaOtrosContenidos=listaOtrosContenidos ;

            var Graficos = dbGrafico.DATA_GRAFICO.ToList();
            var rand2 = new Random();
            List<DATA_GRAFICO> listaGraficos = new List<DATA_GRAFICO>();
            for (int i = 0; i < 15; i++)
            {
                listaGraficos.Add(Graficos[rand2.Next(Graficos.Count)]);
            }
            ViewBag.Graficos = listaGraficos;

            return View();
        }

        //public ActionResult Index2(int id = 1, string id2 = "grafico")

        public ActionResult PaginaBusqueda(string id = "1")
        {
            ViewBag.palabra = id;
            IEnumerable<DATA_GRAFICO> union = UtilBusqueda.PaginaBusqueda(id);
            if(union.Count() == 0)
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

        public PartialViewResult Paginacion(string id = "1", int id2 = 1)
        {            
            //var NEW_GRAFICOS
            ViewBag.Resultado = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(id) || x.titulo.Contains(id) || x.tags.Contains(id))
                                                .OrderBy(x => x.id)
                                                .Skip(200 + 50 * id2)
                                                .Take(50);            
            return PartialView();
        }

        public ActionResult PaginaBusqueda2(string id)
        {
            ViewBag.Resultado = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(id)).ToList();
            List<DATA_GRAFICO> salida = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(id)).ToList();
            List<string> nombres = new List<string>();
            foreach (var item in salida)
            {
                nombres.Add(item.nombre);
            }
            return Json(nombres, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HomeBusqueda()
        {
            return View();
        }
        public ActionResult gustavo()
        {
            ViewBag.test = dbGrafico.INDUSTRIA.ToList();
            return View();
        }

        //[OutputCache(Duration = int.MaxValue)]
        public ActionResult HomeOdoo()
        {
            var rand = new Random();
            List<decimal> aux = new List<decimal>();
            for (int i = 0; i < 20; i++)
            {
                //aux.Add(rand.Next(Int64.Parse(dbGrafico.DATA_GRAFICO.Min(x => x.id).ToString()), Int64.Parse(dbGrafico.DATA_GRAFICO.Max(x => x.id).ToString())));
                //aux.Add(rand.Next(500, 10000000));
            }
            var Graficos = dbGrafico.DATA_GRAFICO.Take(15); //Where(x => aux.Contains(x.id)).ToList();

            //var Graficos = dbGrafico.DATA_GRAFICO.ToList();

            List<DATA_GRAFICO> listaGraficos = Graficos.ToList();  //new List<DATA_GRAFICO>();
            /*
            for (int i = 0; i < 15; i++)
            {
                listaGraficos.Add(Graficos[rand.Next(Graficos.Count())]);
            }
            */
            ViewBag.Graficos = listaGraficos;
            return View();
        }

        public PartialViewResult menuLayout()
        {
            ViewBag.MenuLayout = new MENULAYOUT();
            return PartialView();
        }

        public ActionResult Dashboard(decimal id =100)
        {
            string url = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First().url;
            ViewBag.url = url;
            //ViewBag.url = "https://analytics.zoho.com/open-view/2395394000000697928";           
            return View();
        }
       
        public ActionResult ResultadoNiveles(int id = 10, int id2 = 1)
        {
            IEnumerable<DATA_GRAFICO> Graficos = UtilBusqueda.ResultadoNiveles(id, id2);
            ViewBag.Resultado = Graficos;
            ViewBag.palabra = Graficos.First().CATEGORIA.nombre;
            List<string> Paises = new List<string>();
            List<string> Escala = new List<string>();
            List<string> TipoGrafico = new List<string>();
            List<string> Temporalidad = new List<string>();
            List<string> Producto = new List<string>();
            List<string> Industria = new List<string>();
            List<string> Sector = new List<string>();
            List<string> Categoria = new List<string>();
            List<string> Parametro = new List<string>();
            foreach (var item in Graficos)
            {
                if (!Paises.Contains(item.TERRITORIO.auxiliar))
                {
                    Paises.Add(item.TERRITORIO.auxiliar);
                }
                if (!Escala.Contains(item.TERRITORIO.nombre))
                {
                    Escala.Add(item.TERRITORIO.nombre);
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
            return View("PaginaBusqueda");
        }
        
        public PartialViewResult VisualizarGraficos(decimal id =1234)
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

        public PartialViewResult ContenidoGrafico(int id)
        {
            //id = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First().CATEGORIA_id;
            //var listaNum = dbGrafico.CATEGORIA.Where(x => x.id == id).First().auxiliar.Split(',');
            DATA_GRAFICO gAux = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            //ViewBag.tresRelacionados = dbGrafico.DATA_GRAFICO.Where(x => listaNum.Contains(x.id.ToString())).ToList();
            ViewBag.tresRelacionados = UtilBusqueda.Relacionados3importantes(gAux.CATEGORIA_id,id);
            var algo = UtilBusqueda.Relacionados3importantes(gAux.CATEGORIA_id, id);
            int id2 = id;
            return PartialView();
        }
        public PartialViewResult CarrucelBusqueda(int id)
        {
            id = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First().CATEGORIA.PRODUCTO_id;
            //var listaNum = dbGrafico.PRODUCTO.Where(x => x.id == id).First().auxiliar.Split(',');
            //ViewBag.Carrusel = dbGrafico.DATA_GRAFICO.Where(x => listaNum.Contains(x.id.ToString())).ToList();
            ViewBag.Carrusel = UtilBusqueda.Relacionados12Carrusel(id);
            return PartialView();
        }

        public ActionResult index2(decimal id = 1234)
        {
            //int id = 1234;
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
            return View();
        }

        public PartialViewResult tresRelacionados(int id)
        {
            //var listaNum = dbGrafico.CATEGORIA.Where(x => x.id == id).First().auxiliar.Split(',');
            //ViewBag.tresRelacionados = dbGrafico.DATA_GRAFICO.Where(x => listaNum.Contains(x.id.ToString())).ToList();
            ViewBag.tresRelacionados = UtilBusqueda.Relacionados3importantes(id);
            var algo = UtilBusqueda.Relacionados3importantes(id); 
            int id2 = id;
            return PartialView();
        }

        public PartialViewResult carruselRelacionados(int id)
        {
            //var listaNum = dbGrafico.PRODUCTO.Where(x => x.id == id).First().auxiliar.Split(',');
            //ViewBag.Carrusel = dbGrafico.DATA_GRAFICO.Where(x => listaNum.Contains(x.id.ToString())).ToList();
            ViewBag.Carrusel = UtilBusqueda.Relacionados12Carrusel(id);
            return PartialView();
        }

        public ActionResult VivianIfram()
        {
            return View();
        }

        public ActionResult nose()
        {
            ViewBag.time1 = DateTime.Now;
            //ViewBag.resultado = UtilBusqueda.IdCategoria(100101001,1234);
            //ViewBag.resultado = UtilBusqueda.IdProducto(100101, 1234);
            ViewBag.time2 = DateTime.Now;
            return View();
        }

        public ActionResult No_Resultado()
        {            
            return View();
        }

        public PartialViewResult CarruselVetical()
        {

            return PartialView();
        }
        public PartialViewResult CarruselVetical2()
        {

            return PartialView();
        }
        public PartialViewResult CarruselVetical3()
        {

            return PartialView();
        }

        public ActionResult HomeOdoo2()
        {
            var rand = new Random();
            List<int> aux = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                aux.Add(rand.Next(dbGrafico.GRAFICO.Min(x => x.id), dbGrafico.GRAFICO.Max(x => x.id)));
            }
            var Graficos = dbGrafico.GRAFICO.Where(x => aux.Contains(x.id)).ToList();

            //var Graficos = dbGrafico.GRAFICO.ToList();

            List<GRAFICO> listaGraficos = new List<GRAFICO>();
            for (int i = 0; i < 15; i++)
            {
                listaGraficos.Add(Graficos[rand.Next(Graficos.Count)]);
            }
            ViewBag.Graficos = listaGraficos;
            return View();
        }

        public ActionResult PaginaBusquedaNuevo(string id = "1")
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
    }


}