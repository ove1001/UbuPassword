
using System;

namespace LibreriaDeClases
{
    public class LineaLog
    {
        private DateTime fecha;
        private String mensaje;
        private Int32 idLineaLog;
        private String emailUsuario;
        private Int32 idEntrada;

        public LineaLog(string mensaje, int idLineaLog, String emailUsuario=null, int idEntrada=-1)
        {
            this.Fecha = DateTime.Now;
            this.Mensaje = mensaje;
            this.IdLineaLog = idLineaLog;
            this.emailUsuario = emailUsuario;
            this.IdEntrada = idEntrada;
        }

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public int IdLineaLog { get => idLineaLog; set => idLineaLog = value; }
        public string EmailUsuario { get => emailUsuario; set => emailUsuario = value; }
        public int IdEntrada { get => idEntrada; set => idEntrada = value; }

        //¿necesita id?
        public override String ToString()
        {
            String linea = IdLineaLog.ToString() + " : " + fecha.ToString() + " | " + Mensaje;
            if (EmailUsuario != null)
                linea = linea + " | Usuario: " + EmailUsuario;
            if (IdEntrada != -1)
                linea = linea + " | Entrada: " + IdEntrada;
            return linea;
        }

    }
}
