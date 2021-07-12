using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Login.Models;

namespace Login.Models
{
    public class Producto_Shopify
    {
        public string ID { get; set; }
        public string NOMBRE { get; set; }
        public string SKU { get; set; }
        public string COMPROBANTE { get; set; }
        public string PRODUCT_ID { get; set; }
        //created_at
        public string FECHA_CREADO { get; set; }
        public DateTime FECHA_CREADO2 { get; set; }
        public string URL_IMAGEN { get; set; }

        public string DESCRIPCION { get; set; }

        public int DIAS_DESDE_COMPRA { get; set; }
        //public string AUXILIAR { get; set; }

        public Producto_Shopify(JToken json, string comprobante, JToken ORDEN)
        {
                //AUXILIAR = (string)json;
                ID = (string)json["variant_id"];
                NOMBRE = (string)json["name"];
                SKU = (string)json["sku"];

                PRODUCT_ID = (string)json["product_id"];
                FECHA_CREADO = (string)ORDEN["created_at"];
                FECHA_CREADO2 = DateTime.ParseExact((string)ORDEN["created_at"],  //"11/11/2021 11:00:00",
                            "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                COMPROBANTE = comprobante;
           
            
            //URL_IMAGEN
            try
            {
                JObject jObject = APIShopify.BuscarImagenes((string)json["product_id"]);
                JArray jArray = (JArray)jObject["images"];
                URL_IMAGEN = (string)jArray[0]["src"];

            }
            catch (Exception)
            {

                URL_IMAGEN = "https://pbs.twimg.com/profile_banners/1244018511866925058/1585841185/1500x500";
            }

            try
            {
                string descripcionAux = APIShopify.BuscarDescripcion(this.PRODUCT_ID);
                DESCRIPCION = descripcionAux.Split(new[] { "Características" },
                                                StringSplitOptions.RemoveEmptyEntries).ToList()[0];
            }
            catch (Exception)
            {

                DESCRIPCION = "Sin descripcion";
            }

            DIAS_DESDE_COMPRA = (DateTime.Now-FECHA_CREADO2).Days; //Total de dias desde que se compro (FECHA_CREADO2) hasta hoy Datetime.Now()

        }


        public Producto_Shopify(JToken json, string comprobante)
        {
            ID = (string)json["variant_id"];
            NOMBRE = (string)json["name"];
            SKU = (string)json["sku"];
            PRODUCT_ID = (string)json["product_id"];
            FECHA_CREADO = (string)json["created_at"];
            COMPROBANTE = comprobante;
        }

        public Producto_Shopify(JToken json)
        {
            ID = (string)json["variant_id"];
            NOMBRE = (string)json["name"];
            SKU = (string)json["sku"];
            PRODUCT_ID = (string)json["product_id"];
            FECHA_CREADO = (string)json["created_at"];
        }

    }
}