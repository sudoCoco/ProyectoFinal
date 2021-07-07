using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Modelo
{
    public class OfertaTrabajador
    {
        public int OfertaTrabajadorId { get; set; }
        public int OfertaId { get; set; }
        public int TrabajadorId { get; set; }
        public DateTime FechaOfertaEnviada { get; set; }

        //Navegacion:
        public virtual Oferta Oferta { get; set; }
        public virtual Trabajador Trabajador { get; set; }
        public virtual Colocacion Colocacion { get; set; }
    }
}
