using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Suscripcion
    {
        public INDUSTRIA iNDUSTRIA { get; set; }
        public string NIVEL { get; set; }
        private readonly graficosEntities dbGrafico = new graficosEntities();

        public Suscripcion(int id, string nivel)
        {
            this.iNDUSTRIA = dbGrafico.INDUSTRIA.Where(x => x.id == id).First();
            this.NIVEL = nivel;

        }
    }
}