using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProyectoFinal.Modelo
{
    public class Oferta
    {
        public int OfertaId { get; set; }
        public int EmpresaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Puesto { get; set; }
        public DateTime FechaOferta { get; set; }


        //Navegacion
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<OfertaTrabajador> OfertaTrabajadores { get; private set; } = new ObservableCollection<OfertaTrabajador>();
    }
}
