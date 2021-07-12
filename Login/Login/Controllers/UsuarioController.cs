using Login.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        //Base de datos de los graficos
        private graficosEntities dbGrafico = new graficosEntities();
        //Constructor del controlador
        public UsuarioController()
        {
        }
        // GET: Usuario
        public ActionResult Index()
        {
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            var ordenesPorMail = APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName());
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(ordenesPorMail);
            Session["mis_recursos"] = shopifyYSuscripciones;
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            var aux = shopifyYSuscripciones.todo_producto;
            ViewBag.Resultado = productos;
            ViewBag.InformeInteractivo = shopifyYSuscripciones.informe_interactivo;
            ViewBag.Reporte360 = shopifyYSuscripciones.reporte_360;
            ViewBag.Menu = shopifyYSuscripciones.menu;  //grafico;
            return View();
        }
        
        public ActionResult Suscripcion()
        {
            ShopifyYSuscripciones shopifyYSuscripciones = (ShopifyYSuscripciones)Session["mis_recursos"];
            /*
            List <Producto_Shopify> productos = new List<Producto_Shopify>();
            ViewBag.User = User.Identity.GetUserName();
            foreach (Newtonsoft.Json.Linq.JToken item in APIShopify.BuscarOrdenesPorMail())
            {
                foreach (var item2 in item["line_items"])
                {

                    productos.Add(new Producto_Shopify(item2, (string)item["order_status_url"], item));
                }
            }
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //ViewBag.Resultado = productos;
            */
            ViewBag.Resultado = shopifyYSuscripciones.todo_producto;
            return View();
        }

        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************
        //
        //                                                    Desde Aqui todos los objetos son de pruebas para desarrollo
        //
        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************
        //**************************************************************************************************************************************************************************

        

        public ActionResult Usuaria2()
        {
            ViewBag.Lista = dbGrafico.INDUSTRIA.ToList();
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail())
            {
                foreach (var item2 in item["line_items"])
                {
                    productos.Add(new Producto_Shopify(item2, (string)item["order_status_url"], item));
                }
            }
            //Objeto que separa Productos y Suscripciones
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            ViewBag.Resultado = productos;
            //ViewBag.Menu = dbGrafico.INDUSTRIA.ToList();
            //Menu que esta suscrito el usuario
            ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => shopifyYSuscripciones.sector.Contains(x.id)).ToList();
            return View();
        }

        public ActionResult Dashboard(string id = null)
        {
            List<Producto_Shopify> productos = (List<Producto_Shopify>)Session["Productos"];
            if (productos == null)
            {
                 productos = new List<Producto_Shopify>();
                //Nombre de Usuario
                ViewBag.User = User.Identity.GetUserName();
                //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
                foreach (var item in APIShopify.BuscarOrdenesPorMail())
                {
                    foreach (var item2 in item["line_items"])
                    {
                        productos.Add(new Producto_Shopify(item2, (string)item["order_status_url"], item));
                    }
                }
                //Objeto que separa Productos y Suscripciones
                ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
                //Productos
                productos = shopifyYSuscripciones.producto_Shopifies;
                //ViewBag.url = (string)Session["url"];
                Session["Productos"] = productos;
                //Suscripciones
                Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            }
            string url = "";
            try
            {
                 url = productos.Where(x => x.ID == id).First().SKU;
            }
            catch (Exception)
            {

                url = productos[0].SKU;
            }
            

            ViewBag.url = url; //"https://www.c-sharpcorner.com/article/html-action-and-html-renderaction-in-Asp-Net-mvc/";
            string user = User.Identity.GetUserName();
            ShopifyYSuscripciones shopifyYSuscripciones2 = new ShopifyYSuscripciones(productos);
            ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => shopifyYSuscripciones2.sector.Contains(x.id)).ToList();
            ViewBag.Resultado = productos;
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
        public PartialViewResult Bienvenido()
        {
            ViewBag.User = User.Identity.GetUserName();
            return PartialView();
        }
        public PartialViewResult GraficoUsuario()
        {
            ShopifyYSuscripciones aux = (ShopifyYSuscripciones)Session["mis_recursos"];
            //ViewBag.InformeInteractivo = aux.informe_interactivo;
            
            ViewBag.Resultado = aux.producto_Shopifies;
            return PartialView();
        }

        public PartialViewResult InformesUsuario()
        {
            ShopifyYSuscripciones aux = (ShopifyYSuscripciones)Session["mis_recursos"];
            ViewBag.InformeInteractivo = aux.informe_interactivo;
            return PartialView();
        }
        public PartialViewResult ReportesUsuario()
        {
            ShopifyYSuscripciones aux = (ShopifyYSuscripciones)Session["mis_recursos"];
            ViewBag.Reporte360 = aux.reporte_360;
            //ViewBag.Resultado = productos;
            return PartialView();
        }
        public PartialViewResult Facturados()
        {
            //Lista de productos de Shopify
            /*
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
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

                }
            }
            */
            ShopifyYSuscripciones aux = (ShopifyYSuscripciones)Session["mis_recursos"];
            //ViewBag.Facturados = productos;
            ViewBag.Facturados = aux.todo_producto;
            ViewBag.User = User.Identity.GetUserName();
            return PartialView();
        }
        public PartialViewResult PerfilUsuario()
        {
            ViewBag.User = User.Identity.GetUserName();
            return PartialView();
        }
        public PartialViewResult UsuarioSelectProducto()
        {
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
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

                }
            }
            //Objeto que separa Productos y Suscripciones
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
            Session["Datos"] = shopifyYSuscripciones;
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            ViewBag.Resultado = productos;
            return PartialView();
        }

        public ActionResult resultados(int id= 220106007)
        {
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
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

                }
            }
            //Objeto que separa Productos y Suscripciones
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            ViewBag.Resultado = productos;
            //ViewBag.Menu = dbGrafico.INDUSTRIA.ToList();
            //Menu que esta suscrito el usuario
            //ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => shopifyYSuscripciones.industrias.Contains(x.id)).ToList();
            ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => x.id == 10).ToList();
            var url = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA_id == id).First();
            ViewBag.Grafico = url;
            return View();
        }
        public ActionResult Resultados2(int id = 2)
        {
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
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

                }
            }
            //Objeto que separa Productos y Suscripciones
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            ViewBag.Resultado = productos;
            ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => x.id == 10).ToList();
            return View();
        }
        public ActionResult Index2()
        {
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
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

                }
            }
            //Objeto que separa Productos y Suscripciones
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            ViewBag.Resultado = productos;
            //ViewBag.Menu = dbGrafico.INDUSTRIA.ToList();
            //Menu que esta suscrito el usuario
            //ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => shopifyYSuscripciones.industrias.Contains(x.id)).ToList();
            ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => x.id == 10).ToList();
            return View();
        }
        public PartialViewResult UsuarioSelectProducto2( int id=1)
        {
            var NEW_GRAFICOS = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA_id == id);
            //var NEW_GRAFICOS = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT * FROM GRAFICO WHERE titulo LIKE '% " + id + " %'");


            ViewBag.Resultado = NEW_GRAFICOS.Where(x => x.TIPO_GRAFICO_id < 3).Take(100); //.ToList();//Liberados/Gratis
            ViewBag.Resultado2 = NEW_GRAFICOS.Where(x => x.TIPO_GRAFICO_id == 3).Take(100); //.ToList();//Informes
            ViewBag.Resultado3 = NEW_GRAFICOS.Where(x => x.TIPO_GRAFICO_id == 4).Take(100); //.ToList();//Reportes
            string NombreCategoria = "No hay información de esta categoria";
            if(NEW_GRAFICOS.Count() > 0)
            {
                NombreCategoria = NEW_GRAFICOS.ToList()[0].CATEGORIA.nombre;
            }
            ViewBag.saludo = NombreCategoria;

            return PartialView();
        }
        public PartialViewResult UsuarioSelectProducto3(int id = 220106007)
        {
            var url = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.Grafico = url;

            return PartialView();
        }

        
        public PartialViewResult FormBuscador(string id = "1")
        {
            ViewBag.palabra = id;
            ShopifyYSuscripciones shopifyYSuscripciones = (ShopifyYSuscripciones)Session["mis_recursos"];
            IEnumerable<DATA_GRAFICO> union = UtilBusqueda.PaginaBusquedaUsuario(id, shopifyYSuscripciones.sector);
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
            return PartialView();
        }
        public ActionResult test2()
        {

            return View();
        }

        public ActionResult test3()
        {
            List<int> suscrip = new List<int>();
            suscrip.Add(1001);
            var resultado = dbGrafico.DATA_GRAFICO.Where(x => x.TIPO_GRAFICO_id == 3).ToList();
            foreach (var item in resultado)
            {
                item.suscripciones = suscrip;
            }
            ViewBag.Resultado = resultado;
            return View();
        }
        public PartialViewResult InformeSuscription(string id, string nombre)
            {
                ViewBag.urlReporte = id;
                ViewBag.TituloReporte = nombre;
            
                return PartialView();
        }

        public PartialViewResult InformeReporte(string id, string nombre)
        {
            ViewBag.urlReporte = id;
            ViewBag.TituloReporte = nombre;

            return PartialView();
        }
        public PartialViewResult UsuarioSelectProducto4(int id = 220106007)
        {
            var url = dbGrafico.DATA_GRAFICO.Where(x => x.id == id).First();
            ViewBag.Grafico = url;

            return PartialView();
        }
        public PartialViewResult Index22(int id = 220106007)
        {
            //Lista de productos de Shopify
            List<Producto_Shopify> productos = new List<Producto_Shopify>();
            //Nombre de Usuario
            ViewBag.User = User.Identity.GetUserName();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
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

                }
            }
            //Objeto que separa Productos y Suscripciones
            ShopifyYSuscripciones shopifyYSuscripciones = new ShopifyYSuscripciones(productos);
            //Productos
            productos = shopifyYSuscripciones.producto_Shopifies;
            //ViewBag.url = (string)Session["url"];
            Session["Productos"] = productos;
            //Suscripciones
            Session["Suscripcion"] = shopifyYSuscripciones.suscripcions;
            ViewBag.Resultado = productos;
            //ViewBag.Menu = dbGrafico.INDUSTRIA.ToList();
            //Menu que esta suscrito el usuario
            //ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => shopifyYSuscripciones.industrias.Contains(x.id)).ToList();
            //ViewBag.Menu = dbGrafico.INDUSTRIA.Where(x => x.id==10).ToList();
            int idExample = 1001;
            var grafico = dbGrafico.INDUSTRIA.Where(x => x.id == idExample / 100).ToList();
            foreach (var item in grafico)
            {
                item.SECTOR = (ICollection<SECTOR>)item.SECTOR.Where(x => x.id == 1001).ToArray();
            }
            ViewBag.Menu = grafico;
            var NEW_GRAFICOS = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA_id == id);
            //var NEW_GRAFICOS = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT * FROM GRAFICO WHERE titulo LIKE '% " + id + " %'");


            ViewBag.Resultado = NEW_GRAFICOS.Where(x => x.TIPO_GRAFICO_id < 3).Take(20); //.ToList();//Liberados/Gratis
            ViewBag.Resultado2 = NEW_GRAFICOS.Where(x => x.TIPO_GRAFICO_id == 3).Take(20); //.ToList();//Informes
            //ViewBag.Resultado3 = NEW_GRAFICOS.Where(x => x.TIPO_GRAFICO_id == 4).Take(50); //.ToList();//Reportes
            string NombreCategoria = "No hay información de esta categoria";
            if (NEW_GRAFICOS.Count() > 0)
            {
                NombreCategoria = NEW_GRAFICOS.ToList()[0].CATEGORIA.nombre;
            }
            ViewBag.saludo = NombreCategoria;
            return PartialView();
        }    
        

        public PartialViewResult Paginacion(string id = "1", int id2 = 1)
        {
            //var NEW_GRAFICOS
            ViewBag.Resultado = dbGrafico.GRAFICO.Where(x => x.TIPO_GRAFICO_id < 3)
                                                 .Where(x => x.nombre.Contains(id) || x.titulo.Contains(id) || x.tags.Contains(id))                                               
                                                .OrderBy(x => x.id)
                                                .Skip(200 + 50 * id2)
                                                .Take(50);
            return PartialView();
        }
    }
}
   