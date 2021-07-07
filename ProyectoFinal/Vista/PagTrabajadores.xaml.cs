using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para PagTrabajadores.xaml
    /// </summary>
    public partial class PagTrabajadores : Page
    {
        private readonly AgenciaColocacionContext _contextTrabajadores = new AgenciaColocacionContext();
        private CollectionViewSource trabajadoresViewSource;
        private bool esNuevo = false;
        private Trabajador editTrab = new Trabajador();
        public PagTrabajadores()
        {
            //Listas para Comboboxes:
            List<char> opcionesSN = new List<char>() { 'S', 'N' };
            List<char> opcionesSexo = new List<char>() { '1', '2' };
            List<string> opcionesFormacion = new List<string>() { "00", "10", "20", "30" };

            InitializeComponent();
            trabajadoresViewSource = (CollectionViewSource)FindResource(nameof(trabajadoresViewSource));
            
            //Cargar Comboboxes
            cbSexo.ItemsSource = opcionesSexo;
            cbNivelFormativo.ItemsSource = opcionesFormacion;
            cbDiscapacidad.ItemsSource = opcionesSN;
            cbInmigrante.ItemsSource = opcionesSN;            
            cbPrestacion.ItemsSource = opcionesSN;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            trabajadoresViewSource.Source = _contextTrabajadores.Trabajadores.Local.ToObservableCollection();
            _contextTrabajadores.Trabajadores.ToList();
        }

        //Botones:
        private void ClickEditarTrabajador(object sender, RoutedEventArgs e)
        {
            if (dtTrabajadores.SelectedItem != CollectionView.NewItemPlaceholder)
            {
                itemsEnabled(true);
                editTrab = (Trabajador)dtTrabajadores.SelectedItem;
                dpFechaRegistro.SelectedDate = editTrab.FechaRegistro;
                tbDni.Text = editTrab.Dni;
                tbApellido1.Text = editTrab.Apellido1;
                tbApellido2.Text = editTrab.Apellido2;
                tbNombre.Text = editTrab.Nombre;
                dpFechaNacimiento.SelectedDate = editTrab.FechaNacimiento;
                cbSexo.SelectedValue = editTrab.Sexo;
                cbNivelFormativo.SelectedValue = editTrab.NivelFormativo;
                cbDiscapacidad.SelectedValue = editTrab.Discapacidad;
                cbInmigrante.SelectedValue = editTrab.Inmigrante;
                cbInmigrante.SelectedValue = editTrab.Prestacion;
                btNuevoTrabajador.IsEnabled = false;
                btGuardarTrabajador.IsEnabled = true;
            }
        }

        private void ClickBorrarTrabajador(object sender, RoutedEventArgs e)
        {
            if(dtTrabajadores.SelectedItem != CollectionView.NewItemPlaceholder)
            {
                Trabajador tra = (Trabajador)dtTrabajadores.SelectedItem;
                _contextTrabajadores.Trabajadores.Remove(tra);
                _contextTrabajadores.SaveChanges();
            }
        }

        private void ClickNuevoTrabajador(object sender, RoutedEventArgs e)
        {
            itemsEnabled(true);
            clearForm();
            esNuevo = true;
            btGuardarTrabajador.IsEnabled = true;            
        }

        private void ClickGuardarTrabajador(object sender, RoutedEventArgs e)
        {
            Trabajador tra = new Trabajador();
            if (esNuevo == true)
            {
                guardarEstadoForm(tra);
                _contextTrabajadores.Trabajadores.Add(tra);
                esNuevo = false;
            }
            else
            {
                guardarEstadoForm(editTrab);
                dtTrabajadores.IsReadOnly = false;
                _contextTrabajadores.Entry(editTrab).State = EntityState.Modified;
            }
            _contextTrabajadores.SaveChanges();
            clearForm();
            dtTrabajadores.IsReadOnly = true;
            dtTrabajadores.Items.Refresh();
            itemsEnabled(false);
            btNuevoTrabajador.IsEnabled = true;
            btGuardarTrabajador.IsEnabled = false;
        }

        //Métodos de ahorro de código:
        private void clearForm()
        {
            tbDni.Clear();
            tbApellido1.Clear();
            tbApellido2.Clear();
            tbNombre.Clear();
            cbSexo.SelectedIndex = 0;
            cbNivelFormativo.SelectedIndex = 0;
            cbDiscapacidad.SelectedIndex = 1;
            cbInmigrante.SelectedIndex = 1;
            cbPrestacion.SelectedIndex = 1;
            dpFechaRegistro.SelectedDate = Convert.ToDateTime("1/1/2020");
            dpFechaNacimiento.SelectedDate = Convert.ToDateTime("1/1/2020");
        }
        private void itemsEnabled( bool estado)
        {
            dpFechaRegistro.IsEnabled = estado;
            tbDni.IsEnabled = estado;
            tbApellido1.IsEnabled = estado;
            tbApellido2.IsEnabled = estado;
            tbNombre.IsEnabled = estado;
            dpFechaNacimiento.IsEnabled = estado;
            cbSexo.IsEnabled = estado;
            cbNivelFormativo.IsEnabled = estado;
            cbDiscapacidad.IsEnabled = estado;
            cbInmigrante.IsEnabled = estado;
            cbPrestacion.IsEnabled = estado;
        }       
        private void guardarEstadoForm( Trabajador tra)
        {
            tra.FechaRegistro = (DateTime)dpFechaRegistro.SelectedDate;
            tra.Dni = tbDni.Text;
            tra.Apellido1 = tbApellido1.Text;
            tra.Apellido2 = tbApellido2.Text;
            tra.Nombre = tbNombre.Text;
            tra.FechaNacimiento = (DateTime)dpFechaNacimiento.SelectedDate;
            tra.Sexo = (char)cbSexo.SelectedValue;
            tra.NivelFormativo = (string)cbNivelFormativo.SelectedValue;
            tra.Discapacidad = (char)cbDiscapacidad.SelectedValue;
            tra.Inmigrante = (char)cbInmigrante.SelectedValue;
            tra.Prestacion = (char)cbPrestacion.SelectedValue;
        }
    }
}
