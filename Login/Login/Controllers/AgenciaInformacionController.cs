using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    
    public class AgenciaInformacionController : Controller
    {
        private agenciaEntities1 db = new agenciaEntities1();
        private graficosEntities dbGrafico = new graficosEntities();
        private prueba_educacion1Entities dbEducacion = new prueba_educacion1Entities();
        public ActionResult Index(int id = 0)
        {
            ViewBag.Grafico = dbEducacion.TABLA_PRUEBA_EDUCACION.Where(x => x.id == id).First();
            return View();
        }

        public ActionResult paginabusqueda(string id = "NO_RESULT")
        {
            IEnumerable<TABLA_PRUEBA_EDUCACION> union;
            if(id == "NO_RESULT")
            {
                union = dbEducacion.TABLA_PRUEBA_EDUCACION;
            }
            else
            {
                union = dbEducacion.TABLA_PRUEBA_EDUCACION.Where(x => x.titulo.Contains(id) || x.tag.Contains(id));
            }
            
            ViewBag.Resultado = union;
            ViewBag.num = union.Count();

            
            
            //                                                     @item.tema
            
            //                                                      @item.contenido
           
            //                                                       @item.escala
            
            //                                                        @item.territorio
            List<string> Paises = new List<string>();
            List<string> Escala = new List<string>();
            List<string> TipoGrafico = new List<string>();
            List<string> Temporalidad = new List<string>();
            List<string> Producto = new List<string>();

            List<string> Tema = new List<string>();
            List<string> Contenido = new List<string>();
            List<string> Coleccion = new List<string>();
            Escala.Add("parche");
            Paises.Add("parche");
            foreach (var item in union)
            {
                if (!Paises.Contains(item.territorio))
                {
                    Paises.Add(item.territorio);
                }
                if (!Escala.Contains(item.escala))
                {
                    Escala.Add(item.escala);
                }
                if (!TipoGrafico.Contains(item.visualizacion))
                {
                    TipoGrafico.Add(item.visualizacion);
                }
                if (!Temporalidad.Contains(item.temporalidad))
                {
                    Temporalidad.Add(item.temporalidad);
                }
                if (!Producto.Contains(item.contenido))
                {
                    Producto.Add(item.contenido);
                }
                ///Nuevos
                if (!Producto.Contains(item.contenido))
                {
                    Producto.Add(item.contenido);
                }
            }
            ViewBag.Paises = Paises;
            ViewBag.Escala = Escala;
            ViewBag.TipoGrafico = TipoGrafico;
            ViewBag.Temporalidad = Temporalidad;
            ViewBag.Producto = Producto;

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
        
        public ActionResult Home()
        {
            return View();
        }

    }
}