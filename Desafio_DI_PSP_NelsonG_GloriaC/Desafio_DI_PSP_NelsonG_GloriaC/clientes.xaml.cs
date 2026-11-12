using System;
using System.Windows;

namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    public partial class clientes : Window
    {
        private string conexion = "Server=localhost;Database=bdatos_inicio;Uid=root;Pwd=271020;";

        public clientes()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                // Aquí harías tu consulta a la tabla Cliente de tu BD
                // SELECT id_usuario AS Id, nombre AS Nombre, NIF AS Nif, email AS Email, telefono AS Telefono, direccion AS Direccion FROM Cliente
                txtEstadoClientes.Text = "Clientes cargados correctamente.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            CargarClientes();
        }
    }
}