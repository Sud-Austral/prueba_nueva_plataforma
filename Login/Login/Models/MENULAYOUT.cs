using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{

    public class MENULAYOUT
    {
        private static graficosEntities dbGrafico = new graficosEntities();
        public List<RegistroMenu> RegistroMenus { get; set; }

        public MENULAYOUT()
        {
            RegistroMenus = new List<RegistroMenu>();
            foreach (var item in dbGrafico.INDUSTRIA)
            {
                RegistroMenu auxiliar = new RegistroMenu(item);
                if(auxiliar.sECTORs.Count > 0 )
                {
                    RegistroMenus.Add(auxiliar);
                }
                
            }
        }

        public static List<List<DATA_GRAFICO>> Menu()
        {
            //private graficosEntities dbGrafico = new graficosEntities();
            List<List<DATA_GRAFICO>> salida = new List<List<DATA_GRAFICO>>();
            foreach (var item in dbGrafico.INDUSTRIA)
            {
                List<DATA_GRAFICO> auxiliar = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO.SECTOR.INDUSTRIA_id == item.id).ToList();
                /*
                if (auxiliar.Count > 0)
                {

                }
                */
            }

            return salida;
        }
    }

    public class RegistroMenu
    {
        public INDUSTRIA iNDUSTRIA { get; set; }
        public List<GraficoPorSector> sECTORs { get; set; }
        
        public RegistroMenu(INDUSTRIA ind)
        {
            this.iNDUSTRIA = ind;
            sECTORs = new List<GraficoPorSector>();
            foreach (var item in this.iNDUSTRIA.SECTOR)
            {
                GraficoPorSector auxiliar = new GraficoPorSector(item);
                if(auxiliar.gRAFICOs != null)
                {
                    sECTORs.Add(auxiliar);
                }
                
            }

        }
    }

    public class GraficoPorSector
    {
        private static graficosEntities dbGrafico = new graficosEntities();
        public SECTOR sECTOR { get; set; }
        public List<DATA_GRAFICO> gRAFICOs { get; set; }
        public GraficoPorSector(SECTOR se)
        {
            var rand = new Random();
            this.sECTOR = se;
            gRAFICOs = new List<DATA_GRAFICO>();
            var TodoGrafico = dbGrafico.DATA_GRAFICO.Where(x => x.CATEGORIA.PRODUCTO.SECTOR_id == se.id).ToList();
            if(TodoGrafico.Count > 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    gRAFICOs.Add(TodoGrafico[rand.Next(TodoGrafico.Count)]);
                }
            }
            else
            {
                gRAFICOs = null;
            }
            
        }
    }
}