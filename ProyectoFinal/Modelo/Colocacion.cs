using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Modelo
{
    public class Colocacion
    {
        public int ColocacionId { get; set; }
        public int OfertaTrabajadorId { get; set; }
        public string TipoContrato { get; set; }

        public DateTime FechaColocacion { get; set; }

        //Navegacion:
        public virtual OfertaTrabajador OfertaTrabajador { get; set; }
    }
}
