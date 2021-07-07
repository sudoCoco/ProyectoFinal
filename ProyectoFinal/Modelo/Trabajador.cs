using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProyectoFinal.Modelo
{
    public class Trabajador
    {
        public int TrabajadorId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Dni { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }
        public string NivelFormativo { get; set; }
        public char Discapacidad { get; set; }
        public char Inmigrante { get; set; }
        public char Prestacion { get; set; }

        //Navegacion
        public virtual ICollection<OfertaTrabajador> OfertaTrabajadores { get; private set; } = new ObservableCollection<OfertaTrabajador>();
    }
}
