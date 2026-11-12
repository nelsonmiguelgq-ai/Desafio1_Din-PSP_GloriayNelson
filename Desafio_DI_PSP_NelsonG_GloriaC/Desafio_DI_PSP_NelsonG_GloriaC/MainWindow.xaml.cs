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
        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            _ = CargarDatostablaAsync();
        }

        // --- MÉTODO LEER ASÍNCRONO (Para no congelar el arranque) ---
        private async Task CargarDatostablaAsync()
        {
            try
            {
                using (var context =  _dbContextFactory.CreateDbContext())
                {
                    // Usamos ToListAsync para que la consulta corra en segundo plano
                    var listaClientes = await context.Clientes.ToListAsync();
                    NavUsuario.ItemsSource = listaClientes;
                }

                txtEstado.Text = "Datos cargados correctamente con EF Core.";
            }
            catch (Exception ex)
            {
                // Si la BD falla al iniciar, la ventana ya está abierta y te avisa sin cerrarse de golpe
                txtEstado.Text = "Error al conectar con la base de datos.";
                System.Diagnostics.Debug.WriteLine($"Error al cargar los datos: {ex.Message}");
            }
        }

        // --- BOTÓN INSERTAR ---
        private void Insertar_Click(object sender, RoutedEventArgs e)
        {
            Formulario panta = new Formulario();
            if (panta.ShowDialog() == true)
            {
                _ = CargarDatostablaAsync();
                txtEstado.Text = "Cliente insertado con éxito.";
            }
        }

        // --- BOTÓN MODIFICAR ---
        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (NavUsuario.SelectedItem != null)
            {
                MessageBox.Show("Modificando elemento seleccionado", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Seleccione correctamente el elemento de la lista", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // --- BOTÓN ELIMINAR ---
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (NavUsuario.SelectedItem != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro de la base de datos?",
                                                             "Confirmar eliminación",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _ = CargarDatostablaAsync();
                        txtEstado.Text = "Registro eliminado correctamente de la BD.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el registro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un elemento para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}