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
using System;
using System.Windows;

namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    /// <summary>
    /// Lógica de interacción para Formulario.xaml
    /// </summary>
    public partial class Formulario : Window
    {
        public Formulario()
        {
            InitializeComponent();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Aquí puedes realizar las validaciones básicas de campos vacíos si lo deseas
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtNif.Text))
            {
                MessageBox.Show("Por favor, rellene al menos el Nombre y el NIF.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TODO: Aquí pondrás tu sentencia SQL (INSERT INTO Cliente ...) usando la conexión a MySQL

            // Esto es clave: al poner DialogResult en true, la ventana principal sabe que se guardó con éxito
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cierra el formulario sin hacer cambios (DialogResult será false o null)
            this.DialogResult = false;
            this.Close();
        }
    }
}
