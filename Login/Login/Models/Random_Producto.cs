using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Random_Producto
    {
        public int id { get; set; }
        public GRAFICO gRAFICO { get; set; }

        public Random_Producto(int id, GRAFICO gRAFICO)
        {
            this.id = id;
            this.gRAFICO = gRAFICO;
        }

    }

    public class Lista_Productos_Random
    {
        public List<Random_Producto> lista { get; set; }
        public Lista_Productos_Random()
        {
            lista = new List<Random_Producto>();
            graficosEntities dbGrafico = new graficosEntities();
            foreach (var item in dbGrafico.PRODUCTO)
            {
                //lista.Add()
            }

        }
    }
}