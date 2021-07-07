using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProyectoFinal.Modelo;

namespace ProyectoFinal.Vista
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AgenciaColocacionContext _context = new AgenciaColocacionContext();
        PagAgencia pAgencia = new PagAgencia();
        PagEmpresas pEmpresas = new PagEmpresas();
        PagTrabajadores pTrabajadores = new PagTrabajadores();
        PagOfertas pOfertas = new PagOfertas();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Cargar la ventana:
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
        }
        //Botones y paginación:
        private void buttonSalirClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btClickAgencia(object sender, RoutedEventArgs e)
        {
            frPaginas.NavigationService.Navigate(pAgencia);
        }
        private void btClickEmpresas(object sender, RoutedEventArgs e)
        {
            frPaginas.NavigationService.Navigate(pEmpresas);
        }

        private void btClickTrabajadores(object sender, RoutedEventArgs e)
        {
            frPaginas.NavigationService.Navigate(pTrabajadores);
        }

        private void btClickOfertas(object sender, RoutedEventArgs e)
        {
            frPaginas.NavigationService.Navigate(pOfertas);
        }
    }
}
