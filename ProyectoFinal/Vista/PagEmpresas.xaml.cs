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
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Modelo;

namespace ProyectoFinal.Vista
{
    /// <summary>
    /// Lógica de interacción para PagEmpresas.xaml
    /// </summary>
    public partial class PagEmpresas : Page
    {
        private readonly AgenciaColocacionContext _contextEmpresa = new AgenciaColocacionContext();
        private CollectionViewSource empresasViewSource;
        private bool esNuevo = false;
        private Empresa editEmp = new Empresa();

        public PagEmpresas()
        {
            InitializeComponent();
            empresasViewSource = (CollectionViewSource)FindResource(nameof(empresasViewSource));
            
        }      

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            empresasViewSource.Source = _contextEmpresa.Empresas.Local.ToObservableCollection();
            _contextEmpresa.Empresas.ToList();
        }

        //Botones:
        private void btBorrarEmpresa(object sender, RoutedEventArgs e)
        {
            if(dtEmpresas.SelectedItem != CollectionView.NewItemPlaceholder)
            {
                Empresa emp = (Empresa)dtEmpresas.SelectedItem;
                _contextEmpresa.Empresas.Remove(emp);
                _contextEmpresa.SaveChanges();
            }            
        }

        private void btEditarEmpresa(object sender, RoutedEventArgs e)
        {
            if (dtEmpresas.SelectedItem != CollectionView.NewItemPlaceholder)
            {
                tbEnabled();
                editEmp = (Empresa)dtEmpresas.SelectedItem;
                tbCif.Text = editEmp.CifEmpresa;
                tbRazonSocial.Text = editEmp.RazonSocial;
                btNuevaEmpresa.IsEnabled = false;
                btGuardarEmpresa.IsEnabled = true;
            }                
        }

        private void btClickNuevaEmpresa(object sender, RoutedEventArgs e)
        {
            tbEnabled();
            clearForm();
            esNuevo = true;
            btGuardarEmpresa.IsEnabled = true;
        }

        private void btClickGuardarEmpresa(object sender, RoutedEventArgs e)
        {
            Empresa emp = new Empresa();
            if (esNuevo == true)
            {
                emp.CifEmpresa = tbCif.Text;
                emp.RazonSocial = tbRazonSocial.Text;
                _contextEmpresa.Empresas.Add(emp);
                esNuevo = false;
            }
            else
            {
                editEmp.CifEmpresa = tbCif.Text;
                editEmp.RazonSocial = tbRazonSocial.Text;
                dtEmpresas.IsReadOnly = false;

                _contextEmpresa.Entry(editEmp).State = EntityState.Modified;
            }            
            _contextEmpresa.SaveChanges();
            clearForm();
            dtEmpresas.IsReadOnly = true;
            dtEmpresas.Items.Refresh();
            tbCif.IsEnabled = false;
            tbRazonSocial.IsEnabled = false;
            btNuevaEmpresa.IsEnabled = true;
            btGuardarEmpresa.IsEnabled = false;
        }
        private void clearForm()
        {
            tbCif.Clear();
            tbRazonSocial.Clear();
        }
        private void tbEnabled()
        {
            tbCif.IsEnabled = true;
            tbRazonSocial.IsEnabled = true;
        }        
    }
}
