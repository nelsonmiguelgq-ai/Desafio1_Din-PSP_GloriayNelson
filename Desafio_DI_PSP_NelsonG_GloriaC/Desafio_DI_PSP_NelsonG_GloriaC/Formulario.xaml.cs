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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Desafio_DI_PSP_NelsonG_GloriaC.Models; // Aquí están tus modelos mapeados por Scaffold

namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    /// <summary>
    /// Lógica de interacción para Formulario.xaml
    /// </summary>
    public partial class Formulario : Window
    {
        // 1. Declaramos la fábrica al igual que en el MainWindow para usar la misma conexión centralizada
        private readonly IDbContextFactory<EscuelaBasicContext> _dbContextFactory;

       
        public Formulario()
        {
            InitializeComponent();

            // Obtenemos la fábrica desde el contenedor global de App.xaml.cs
            _dbContextFactory = App.proveedorServicios.GetRequiredService<IDbContextFactory<EscuelaBasicContext>>();
        }

      

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validamos que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtNif.Text))
            {
                MessageBox.Show("Por favor, rellene al menos el Nombre y el NIF.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Abrimos el contexto con la fábrica
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    // PASO A: Creamos primero un registro en la tabla Usuarios para satisfacer la relación de la clave foránea
                    var nuevoUsuario = new Usuario
                    {
                        Nombre = txtNombre.Text,
                        Contraseña = "123456", // Ponle una contraseña por defecto (revisa si en tu modelo se llama Contrasena o Contraseña)
                        Rol = "Cliente"
                        // Si la tabla Usuario tiene más campos requeridos, ponlos aquí. 
                        // Si solo requiere autoincrementar el ID, este objeto vacío bastará para generar el registro padre.
                    };
                    context.Usuarios.Add(nuevoUsuario);
                    context.SaveChanges();
                    int telefonoNumerico = 0;
                    int.TryParse(txtTelefono.Text, out telefonoNumerico);
                   
                    // 3. AQUÍ ESTÁ LA MAGIA: 
                    // Creas un objeto 'Cliente' (que es la clase que generó el Scaffold de tu tabla MySQL).
                    // Asignas cada caja de texto (TextBox) a su propiedad correspondiente.
                    // C# sabe automáticamente que la propiedad 'Nif' va a la columna 'nif' de tu base de datos,
                    // y 'Nombre' va a la columna de nombre.
                    var nuevoCliente = new Cliente
                    {
                        IdUsuario = nuevoUsuario.IdUsuario,
                        Nombre = txtNombre.Text, // El texto de txtNombre se guarda en la columna Nombre
                        Nif = txtNif.Text,      // El texto de txtNif se guarda en la columna Nif 
                        Email = txtEmail.Text,
                        Telefono = telefonoNumerico,
                        Direccion = txtDireccion.Text,
                       
                    };

                    // 4. Añadimos el objeto al DbSet de Clientes
                    context.Clientes.Add(nuevoCliente);

                    // 5. Guardamos los cambios definitivamente en la base de datos MySQL
                    context.SaveChanges();
                }

                // Si todo va bien, indicamos que se guardó con éxito y cerramos el formulario
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                // Esto extraerá el mensaje oculto de MySQL
                string mensajeReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                MessageBox.Show($"ERROR REAL DE MYSQL:\n\n{mensajeReal}",
                                "Diagnóstico de Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cierra el formulario sin hacer cambios
            this.DialogResult = false;
            this.Close();
        }
    }
}