using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Orden
    {
        public string id { get; set; }
        public string correo { get; set; }
        public string producto { get; set; }
        public string  sku { get; set; }
        public string cliente { get; set; }

        public Orden(string id, string correo, string producto, string sku, string cliente)
        {
            this.id = id;
            this.correo = correo;
            this.producto = producto;
            this.sku = sku;
            this.cliente = cliente;
        }

        public Orden(string correo, string producto, string nombre)
        {
            this.id = nombre;
            this.correo = correo;
            this.producto = producto;
            this.sku = "";
            this.cliente = "";
        }
    }
}