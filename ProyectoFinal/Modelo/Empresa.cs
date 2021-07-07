using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProyectoFinal.Modelo
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string CifEmpresa { get; set; }
        public string RazonSocial { get; set; }

        //Una empresa muchos trabajadores:      
        public virtual ICollection<Oferta> Ofertas { get; private set; } = new ObservableCollection<Oferta>();
    }
}
