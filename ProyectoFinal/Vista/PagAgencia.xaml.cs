using ProyectoFinal.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoFinal.Vista
{
    /// <summary>
    /// Lógica de interacción para PagAgencia.xaml
    /// </summary>
    public partial class PagAgencia : Page
    {
        private readonly AgenciaColocacionContext _contextAgencia = new AgenciaColocacionContext();
        public PagAgencia()
        {
            InitializeComponent();
            
        }
    }
}
