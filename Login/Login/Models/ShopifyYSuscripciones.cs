using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class ShopifyYSuscripciones
    {
        private graficosEntities dbGrafico = new graficosEntities();
        public List<Producto_Shopify> producto_Shopifies { get; set; }
        public List<decimal> sector { get; set; }
        public List<Suscripcion> suscripcions { get; set; }
        public List<Producto_Shopify> todo_producto { get; set; }
        public List<DATA_GRAFICO> informe_interactivo { get; set; }
        public List<DATA_GRAFICO> reporte_360 { get; set; }
        public List<INDUSTRIA> menu { get; set; }

        public ShopifyYSuscripciones(List<Producto_Shopify> entrada)
        {
            this.todo_producto = entrada;
            this.producto_Shopifies = new List<Producto_Shopify>();
            this.sector = new List<decimal>();
            this.suscripcions = new List<Suscripcion>();
            foreach (var item in entrada)
            {                
                try
                {
                    if (item.SKU.Contains("datasuscripcion"))
                    {
                        int aux = Int32.Parse(item.SKU.Split('-')[2]);
                        string nivel = item.SKU.Split('-')[1];
                        this.sector.Add(aux);
                        this.suscripcions.Add(new Suscripcion(aux, nivel));
                    }
                    else
                    {
                        this.producto_Shopifies.Add(item);
                    }
                }
                catch (Exception)
                {
                }                      
            }
        }

        public ShopifyYSuscripciones(List<JToken> jtoken)
        {
            List<Producto_Shopify> entrada = new List<Producto_Shopify>();
            this.informe_interactivo = new List<DATA_GRAFICO>();
            this.reporte_360 = new List<DATA_GRAFICO>();
            //foreach (var item in APIShopify.BuscarOrdenesPorMail(User.Identity.GetUserName()))
            foreach (var item in jtoken)
            {
                foreach (var item2 in item["line_items"])
                {
                    try
                    {
                        entrada.Add(new Producto_Shopify(item2, (string)item["order_status_url"], item));
                    }
                    catch (Exception)
                    {
                        string hola = "";
                    }
                }
            }
            this.todo_producto = entrada;
            this.producto_Shopifies = new List<Producto_Shopify>();
            this.sector = new List<decimal>();
            this.suscripcions = new List<Suscripcion>();

            int idGenerico = 3000;
            foreach (var item in entrada)
            {
                if (item.SKU.Contains("https://www.dataintelligence-group.com"))
                {
                    this.producto_Shopifies.Add(item);
                }
                else
                {
                    List<string> sku = item.SKU.Split('-').ToList();
                    switch (item.SKU.Split('-')[0])
                    {
                        case "datasuscripcion":
                            //datasuscripcion-A-10-01/05/2021
                            idGenerico = Int32.Parse(item.SKU.Split('-')[2]);
                            string nivel = item.SKU.Split('-')[1];
                            this.sector.Add(idGenerico);
                            this.suscripcions.Add(new Suscripcion((int)idGenerico, nivel));
                            break;
                        case "datainformeinteractivo":
                            //datainformeinteractivo-1234
                            idGenerico = Int32.Parse(sku[1]);                
                            this.informe_interactivo.Add(dbGrafico.DATA_GRAFICO.Where(x => x.id == idGenerico).First());                       
                            break;
                        case "datareporte360":
                            //datareporte360-1234
                            idGenerico = Int32.Parse(item.SKU.Split('-')[1]);
                            this.reporte_360.Add(dbGrafico.DATA_GRAFICO.Where(x => x.id == idGenerico).First());                                                    
                            break;
                        default:
                            //this.producto_Shopifies.Add(item);
                            break;
                    }
                }



                /*
                 
                switch (item.SKU.Split('-')[0])
                {                 
                    case "datasuscripcion":
                        //datasuscripcion-A-10-01/05/2021
                        idGenerico = Int32.Parse(item.SKU.Split('-')[2]);
                        string nivel = item.SKU.Split('-')[1];
                        this.sector.Add(idGenerico);
                        this.suscripcions.Add(new Suscripcion(idGenerico, nivel));                   
                        break;
                    case "datainformeinteractivo":
                        ayuda = sku[1];
                        idGenerico = int.Parse(sku[1]);
                        //datainformeinteractivo-1234
                        //int aux2 = int.Parse("1234");
                        //idGenerico = int.Parse(sku[1]);
                        //this.informe_interactivo.Add(dbGrafico.DATA_GRAFICO.Where(x => x.id == aux2).First());
                        //this.informe_interactivo.Add(dbGrafico.DATA_GRAFICO.Where(x => x.id == idGenerico).First());                       
                        break;
                    case "datareporte360":
                        ayuda = sku[1];

                        //idGenerico = Int32.Parse(item.SKU.Split('-')[1]);
                        //this.reporte_360.Add(dbGrafico.DATA_GRAFICO.Where(x => x.id == idGenerico).First());
                        //datareporte360-1234                        
                        break;
                    default:
                        this.producto_Shopifies.Add(item);
                        break;
                }
                */
            }
            sector.Add(1001);
            sector.Add(1002);
            List<INDUSTRIA> indAux = new List<INDUSTRIA>(); 
            foreach (var item in sector)
            {
                int aux = (int)(item / 100);
                var grafico = dbGrafico.INDUSTRIA.Where(x => x.id == aux ).First();
                indAux.Add(grafico);
            }
            indAux = indAux.Distinct().ToList();
            foreach (var item in indAux)
            {
                item.SECTOR = (ICollection<SECTOR>)item.SECTOR.Where(x => sector.Contains(x.id)).ToArray();
            }
            this.menu = indAux;

        }

        public ShopifyYSuscripciones()
        {

        }
    }
}