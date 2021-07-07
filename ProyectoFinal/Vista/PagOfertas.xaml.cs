using Microsoft.Data.Sqlite;
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
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Vista
{
    /// <summary>
    /// Lógica de interacción para PagOfertas.xaml
    /// </summary>
    public partial class PagOfertas : Page
    {
        private readonly AgenciaColocacionContext _contextOfertas = new AgenciaColocacionContext();
        private CollectionViewSource ofertasViewSource;
        private bool esNuevo = false;
        private Oferta editOferta = new Oferta();
        SqliteDataReader registro;


        public PagOfertas()
        {
            InitializeComponent();
            ofertasViewSource = (CollectionViewSource)FindResource(nameof(ofertasViewSource));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ofertasViewSource.Source = _contextOfertas.Ofertas.Local.ToObservableCollection();
            _contextOfertas.Ofertas.ToList();
            FillCombobox();
        }

        //Botones:
        private void ClickEditarOferta(object sender, RoutedEventArgs e)
        {
            if(dtOfertas.SelectedItem != CollectionView.NewItemPlaceholder)
            {
                itemsEnabled(true);
                editOferta = (Oferta)dtOfertas.SelectedItem;
                cbEmpresaID.SelectedValue = editOferta.EmpresaId;
                tbTitulo.Text = editOferta.Titulo;
                tbDescripcion.Text = editOferta.Descripcion;
                tbPuestos.Text = editOferta.Puesto.ToString();
                dpFechaOferta.SelectedDate = editOferta.FechaOferta;
                btNuevaOferta.IsEnabled = false;
                btGuardarOferta.IsEnabled = true;
            }
            
        }

        private void ClickBorrarOferta(object sender, RoutedEventArgs e)
        {
            if(dtOfertas.SelectedItem != CollectionView.NewItemPlaceholder)
            {
                Oferta of = (Oferta)dtOfertas.SelectedItem;
                _contextOfertas.Ofertas.Remove(of);
                _contextOfertas.SaveChanges();
            }
        }

        private void ClickNuevaOferta(object sender, RoutedEventArgs e)
        {
            itemsEnabled(true);
            clearForm();
            esNuevo = true;
            btGuardarOferta.IsEnabled = true;
        }

        private void ClickGuardarOferta(object sender, RoutedEventArgs e)
        {
            Oferta of = new Oferta();
            if(esNuevo == true)
            {
                guardarEstadoForm(of);
                _contextOfertas.Ofertas.Add(of);
                esNuevo = false;
            }
            else
            {
                guardarEstadoForm(editOferta);
                dtOfertas.IsReadOnly = true;
                _contextOfertas.Entry(editOferta).State = EntityState.Modified;
            }
            _contextOfertas.SaveChanges();
            clearForm();
            dtOfertas.IsReadOnly = true;
            dtOfertas.Items.Refresh();
            itemsEnabled(false);
            btNuevaOferta.IsEnabled = true;
            btGuardarOferta.IsEnabled = false;
        }

        //Rellenar combobox con la columna EmpresId de la tabla Empresas:
        private void FillCombobox()
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=AgenciaColocaciones.db");
            
            SqliteCommand sc = new SqliteCommand("select EmpresaId from Empresas", conexion);
            conexion.Open();
            registro = sc.ExecuteReader();
            while(registro.Read())
            {
                cbEmpresaID.Items.Add(registro["EmpresaId"]);
            }
            registro.Close();
            conexion.Close();
        }

        //Métodos de ahorro de código:
        private void clearForm()
        {
            cbEmpresaID.SelectedIndex = 0;
            tbTitulo.Clear();
            tbDescripcion.Clear();
            tbPuestos.Clear();
            dpFechaOferta.SelectedDate = Convert.ToDateTime("1/1/2020");
        }
        private void itemsEnabled(bool estado)
        {
            cbEmpresaID.IsEnabled = estado;
            tbTitulo.IsEnabled = estado;
            tbDescripcion.IsEnabled = estado;
            tbPuestos.IsEnabled = estado;
            dpFechaOferta.IsEnabled = estado;
        }
        private void guardarEstadoForm(Oferta of)
        {
            of.EmpresaId = Convert.ToInt32(cbEmpresaID.SelectedValue);
            of.Titulo = tbTitulo.Text;
            of.Descripcion = tbDescripcion.Text;
            of.Puesto = Convert.ToInt32(tbPuestos.Text);
            of.FechaOferta = (DateTime)dpFechaOferta.SelectedDate;
        }
    }
}
