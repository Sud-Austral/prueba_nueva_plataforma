using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Login.Models
{
    public class Util
    {
        public static bool fechaLimite(string fecha)
        {
            bool salida = false;
            int cantidadDias = 1;
            DateTime fechaInicio = DateTime.Now;
            try
            {
                fechaInicio = DateTime.Parse(fecha);
            }
            catch (Exception)
            {
                return false;
            }            
            DateTime fechaActual = DateTime.Now;
            //Calculo de dias.
            TimeSpan td = fechaActual - fechaInicio;
            salida = td.Days <= cantidadDias;
            return salida;
        }

        public static bool fechaLimite(string fecha, int cantidadDias)
        {
            bool salida = false;
            //int cantidadDias = 1;
            DateTime fechaInicio = DateTime.Now;
            try
            {
                fechaInicio = DateTime.Parse(fecha);
            }
            catch (Exception)
            {
                return false;
            }
            DateTime fechaActual = DateTime.Now;
            //Calculo de dias.
            TimeSpan td = fechaActual - fechaInicio;
            salida = td.Days <= cantidadDias;
            return salida;
        }

        public static bool fechaLimite(string fecha, string nombre)
        {
            if (!nombre.Contains("mensual"))
            {
                return true;
            }
            bool salida = false;
            //int cantidadDias = 1;
            DateTime fechaInicio = DateTime.Now;
            try
            {
                fechaInicio = DateTime.Parse(fecha);
            }
            catch (Exception)
            {
                return false;
            }
            DateTime fechaActual = DateTime.Now;
            //Calculo de dias.
            TimeSpan td = fechaActual - fechaInicio;
            salida = td.Days <= 32;
            return salida;
        }

        public static List<string> ListaObtenerTexto(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string s = "Comuna,Cuposáé";
            using (WebClient client = new WebClient())
            {
                //s = client.DownloadString("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/10agricultura_y_ganaderia/1001agricultura/100112hortalizas/100112015calabaza/test_caracteres.csv");
                s = client.DownloadString(url);
            }
            List<string> listaTextoP = s.Split(new[] { "\n" },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
            return listaTextoP;
        }

        public static string ObtenerTexto(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string s = "Comuna,Cuposáé";
            using (WebClient client = new WebClient())
            {
                //s = client.DownloadString("https://raw.githubusercontent.com/Sud-Austral/MPG/main/Datos/10agricultura_y_ganaderia/1001agricultura/100112hortalizas/100112015calabaza/test_caracteres.csv");
                s = client.DownloadString(url);
            }
            byte[] bytes = Encoding.Default.GetBytes(s);
            s = Encoding.UTF8.GetString(bytes);
            return s;
        }
    }
}