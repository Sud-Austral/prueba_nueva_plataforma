using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class UtilBusqueda
    {
        private static graficosEntities dbGrafico = new graficosEntities();
        private static Random rand = new Random();


        public static IEnumerable<DATA_GRAFICO> PaginaBusqueda(string concepto)
        {
            concepto = concepto.Trim().ToLower();
            //string accentedStr;
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(concepto);
            concepto = System.Text.Encoding.UTF8.GetString(tempBytes);
            string query = "SELECT * FROM DATA_GRAFICO WHERE titulo LIKE '% " + concepto + " %'";
            var prioridad = dbGrafico.DATA_GRAFICO.SqlQuery(query)
                                                  .Take(200);            
            IEnumerable<DATA_GRAFICO> NEW_GRAFICOS;
            IEnumerable<DATA_GRAFICO> union = prioridad;
            if (prioridad.Count() < 200)
            {
                NEW_GRAFICOS = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(concepto) || x.titulo.Contains(concepto) || x.tags.Contains(concepto))
                                                 .OrderBy(x => x.id)
                                                 .Take(200 - prioridad.Count());
                int ent = NEW_GRAFICOS.Count();
                union = prioridad.Concat(NEW_GRAFICOS); //.Distinct();
            }
            if(union.Count() == 0)
            {
                concepto = concepto.Substring(0, concepto.Length - 3);
                union = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(concepto) || x.titulo.Contains(concepto) || x.tags.Contains(concepto))
                                                 .Take(200);
            }
            return union;
        }

        public static IEnumerable<DATA_GRAFICO> PaginaBusquedaUsuario(string concepto, List<decimal> sectorId)
        {
            //sectorId = new List<int>();
            //sectorId.Add(1001);
            concepto = concepto.Trim().ToLower();
            //string accentedStr;
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(concepto);
            concepto = System.Text.Encoding.UTF8.GetString(tempBytes);

            var prioridad = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT TOP 200 * FROM DATA_GRAFICO WHERE titulo LIKE '% " + concepto + " %' AND tipo_grafico_id = 3")
                .Where(x => sectorId.Contains(x.CATEGORIA.PRODUCTO.SECTOR_id));
            //prioridad = prioridad.Where(x => sectorId.Contains(x.CATEGORIA.PRODUCTO.SECTOR_id));
            IEnumerable<DATA_GRAFICO> NEW_GRAFICOS;
            IEnumerable<DATA_GRAFICO> union = prioridad;
            if (prioridad.Count() < 200)
            {
                NEW_GRAFICOS = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(concepto) || x.titulo.Contains(concepto) || x.tags.Contains(concepto))
                                                 .Where(x => x.TIPO_GRAFICO_id < 3)
                                                 .OrderBy(x => x.id)
                                                 .Take(200 - prioridad.Count());
                int ent = NEW_GRAFICOS.Count();
                union = prioridad.Concat(NEW_GRAFICOS); //.Distinct();
            }
            if (union.Count() == 0)
            {
                concepto = concepto.Substring(0, concepto.Length - 3);
                union = dbGrafico.DATA_GRAFICO.Where(x => x.nombre.Contains(concepto) || x.titulo.Contains(concepto) || x.tags.Contains(concepto))
                                                 .Take(200);
            }
            return union;
        }
        public static IEnumerable<DATA_GRAFICO> ResultadoNiveles(int id, int tabla)
        {
            IEnumerable<DATA_GRAFICO> Graficos;
            switch (tabla)
            {
                case 1:
                    Graficos = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO.SECTOR.INDUSTRIA_id == id).Take(200);
                    break;
                case 2:
                    Graficos = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO.SECTOR_id == id).Take(200);
                    break;
                case 3:
                    Graficos = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO_id == id).Take(200);
                    break;
                case 4:
                    Graficos = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA_id == id).Take(200);
                    break;
                default:
                    Graficos = dbGrafico.DATA_GRAFICO.Take(200);
                    break;
            }
            return Graficos;
        }


        public static List<DATA_GRAFICO> Relacionados3importantes(int id, int id2)
        {
            List<DATA_GRAFICO> aux = new List<DATA_GRAFICO>();
            var query = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT TOP 20 * FROM DATA_GRAFICO WHERE categoria_id = " + id.ToString() + " AND id <> " + id2.ToString()).ToList();           
            for (int i = 0; i < 3; i++)
            {
                aux.Add(query[rand.Next(query.Count())]);
            }
            return aux;
        }

        public static List<DATA_GRAFICO> Relacionados12Carrusel(int id, int id2)
        {
            List<DATA_GRAFICO> aux = new List<DATA_GRAFICO>();
            //SELECT* FROM grafico WHERE categoria_id IN(SELECT id FROM categoria WHERE PRODUCTO_id = 100101) AND id<> 1033
            var query = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT TOP 50 * FROM DATA_GRAFICO WHERE categoria_id IN (SELECT id FROM categoria WHERE PRODUCTO_id = " + id.ToString() + ") AND id <> " + id2.ToString()).ToList();
            for (int i = 0; i < 12; i++)
            {
                aux.Add(query[rand.Next(query.Count())]);
            }
            return aux;
        }

        public static List<DATA_GRAFICO> Relacionados3importantes(int id)
        {
            List<DATA_GRAFICO> aux = new List<DATA_GRAFICO>();
            var query = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT TOP 20 * FROM DATA_GRAFICO WHERE categoria_id = " + id.ToString()).ToList();
            for (int i = 0; i < 3; i++)
            {
                aux.Add(query[rand.Next(query.Count())]);
            }
            return aux;
        }

        public static List<DATA_GRAFICO> Relacionados12Carrusel(int id)
        {
            List<DATA_GRAFICO> aux = new List<DATA_GRAFICO>();
            //SELECT* FROM grafico WHERE categoria_id IN(SELECT id FROM categoria WHERE PRODUCTO_id = 100101) AND id<> 1033
            var query = dbGrafico.DATA_GRAFICO.SqlQuery("SELECT TOP 50 * FROM DATA_GRAFICO WHERE categoria_id IN (SELECT id FROM categoria WHERE PRODUCTO_id = " + id.ToString() + ")").Take(50).ToList();
            for (int i = 0; i < 12; i++)
            {
                aux.Add(query[rand.Next(query.Count())]);
            }
            return aux;
        }
    }
}