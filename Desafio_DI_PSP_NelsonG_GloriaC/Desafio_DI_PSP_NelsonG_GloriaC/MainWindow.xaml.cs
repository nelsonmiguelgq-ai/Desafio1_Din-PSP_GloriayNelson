using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Desafio_DI_PSP_NelsonG_GloriaC.Models;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDbContextFactory<EscuelaBasicContext> _dbContextFactory;
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                _dbContextFactory = App.proveedorServicios.GetRequiredService
                    <IDbContextFactory<EscuelaBasicContext>>();
                // 1. Carga inicial asíncrona para que la ventana abra SÍ O SÍ al instante
                _ = CargarDatostablaAsync();

                // 2. Arranque del servidor de Named Pipes en segundo plano (PSP)
                _ = IniciarServidorPipeAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                MessageBox.Show(ex.ToString(), "Error con window");
            }
            }

        // --- SERVIDOR NAMED PIPES (PSP) ---
        private async Task IniciarServidorPipeAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        using (NamedPipeServerStream servidorPipe = new NamedPipeServerStream("NavesPipeCanal", PipeDirection.In))
                        {
                            await servidorPipe.WaitForConnectionAsync();

                            using (StreamReader lector = new StreamReader(servidorPipe))
                            {
                                string mensaje = await lector.ReadToEndAsync();

                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    txtEstado.Text = $"¡Aviso de Pipe recibido!: {mensaje}";
                                    _ = CargarDatostablaAsync();
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error en el pipe servidor: {ex.Message}");
                    }
                }
            });
        }

        // --- NAVEGACIÓN ---
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            clientes ventanaClientes = new clientes();
            ventanaClientes.Show();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            producto produ = new producto();
            produ.Show();
        }

        // --- BOTÓN ACTUALIZAR ---
        private async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            await CargarDatostablaAsync();
        }

        // --- MÉTODO LEER ASÍNCRONO (Para no congelar el arranque) ---
        private async Task CargarDatostablaAsync()
        {
            try
            {
                // Guardar el Id del cliente seleccionado para volver a seleccionarlo después
                int? idSeleccionado = (NavUsuario.SelectedItem as Cliente)?.IdUsuario;

                using (var context = _dbContextFactory.CreateDbContext())
                {
                    // AsNoTracking: solo lectura, siempre trae lo que hay en la BD en este momento
                    var listaClientes = await context.Clientes
                                                     .AsNoTracking()
                                                     .OrderBy(c => c.IdUsuario)
                                                     .ToListAsync();

                    NavUsuario.ItemsSource = listaClientes;

                    // Restaurar la selección si ese cliente sigue existiendo
                    if (idSeleccionado != null)
                        NavUsuario.SelectedItem = listaClientes.FirstOrDefault(c => c.IdUsuario == idSeleccionado);

                    txtEstado.Text = $"Datos actualizados ({listaClientes.Count} clientes) - {DateTime.Now:HH:mm:ss}";
                }
            }
            catch (Exception ex)
            {
                txtEstado.Text = "Error al conectar con la base de datos.";
                System.Diagnostics.Debug.WriteLine($"Error al cargar los datos: {ex.Message}");
            }
        }

        // --- BOTÓN INSERTAR ---
        private async void Insertar_Click(object sender, RoutedEventArgs e)
        {
            var form = new Formulario();

            if (form.ShowDialog() == true)
            {
                await CargarDatostablaAsync(); // Recarga la tabla limpiamente
            }

        }

        
        // --- BOTÓN MODIFICAR ---
        private async void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (NavUsuario.SelectedItem is not Cliente cliente)
            {
                MessageBox.Show("Seleccione correctamente el elemento de la lista", "Aviso",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirma cualquier edición pendiente antes de leer el objeto
            NavUsuario.CommitEdit(DataGridEditingUnit.Row, true);
            if (string.IsNullOrWhiteSpace(cliente.Nombre) ||
                string.IsNullOrWhiteSpace(cliente.Nif) ||
                string.IsNullOrWhiteSpace(cliente.Email) ||
                string.IsNullOrWhiteSpace(cliente.Direccion))
            {
                MessageBox.Show("Nombre, NIF, Email y Dirección no pueden estar vacíos.", "Aviso",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                await CargarDatostablaAsync();
                return;
            }

            try
            {
                
                    using (var context = _dbContextFactory.CreateDbContext())
                {
                    // Update marca todos los campos del objeto como modificados
                    context.Clientes.Update(cliente);
                    await context.SaveChangesAsync();
                }

                await CargarDatostablaAsync();
                txtEstado.Text = "Cliente modificado correctamente en la BD.";
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(
                    "No se pudo modificar. Puede que el NIF ya exista en otro cliente.\n\n" +
                    ex.InnerException?.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                await CargarDatostablaAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                await CargarDatostablaAsync();
            }
        }

        // --- BOTÓN ELIMINAR ---
        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cliente = (Cliente)NavUsuario.SelectedItem;

                using (var context = _dbContextFactory.CreateDbContext())
                {
                    // Borra el Usuario; MySQL elimina en cascada Cliente -> Presupuestos -> Detalles
                    await context.Usuarios
                                 .Where(u => u.IdUsuario == cliente.IdUsuario)
                                 .ExecuteDeleteAsync();
                }

                await CargarDatostablaAsync();
                txtEstado.Text = "Usuario eliminado correctamente de la BD.";
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar. Es posible que el usuario tenga datos relacionados (pedidos, etc.).\n\n" +
                    ex.InnerException?.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el usuario: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnPresupuestos_Click(object sender, RoutedEventArgs e)
        {

            var presu = new WindowPresupuestos();

            if (presu.ShowDialog() == true)
            {
                await CargarDatostablaAsync(); // Recarga la tabla limpiamente
            }

        }
    }
}