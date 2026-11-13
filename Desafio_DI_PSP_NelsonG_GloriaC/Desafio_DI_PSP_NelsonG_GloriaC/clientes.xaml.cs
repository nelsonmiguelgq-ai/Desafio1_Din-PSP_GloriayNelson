using Desafio_DI_PSP_NelsonG_GloriaC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    public partial class clientes : Window
    {
        private readonly IDbContextFactory<EscuelaBasicContext> _dbContextFactory;


        public clientes()
        {
            InitializeComponent();
            _dbContextFactory = App.proveedorServicios.GetRequiredService<IDbContextFactory<EscuelaBasicContext>>();
            _ = CargarClientesAsync();
        }
           

  

        private async void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            await CargarClientesAsync();
        }

        private async Task CargarClientesAsync()
        {
            try
            {
                // Guardar el Id del cliente seleccionado para volver a seleccionarlo después
                int? idSeleccionado = (Navclientes.SelectedItem as Cliente)?.IdUsuario;

                using (var context = _dbContextFactory.CreateDbContext())
                {
                    // AsNoTracking: solo lectura, siempre trae lo que hay en la BD en este momento
                    var listaClientes = await context.Clientes
                                                     .AsNoTracking()
                                                     .OrderBy(c => c.IdUsuario)
                                                     .ToListAsync();

                    Navclientes.ItemsSource = listaClientes;

                    // Restaurar la selección si ese cliente sigue existiendo
                    if (idSeleccionado != null)
                        Navclientes.SelectedItem = listaClientes.FirstOrDefault(c => c.IdUsuario == idSeleccionado);

                    txtEstadoClientes.Text = $"Datos actualizados ({listaClientes.Count} clientes) - {DateTime.Now:HH:mm:ss}";
                }
            }
            catch (Exception ex)
            {
                txtEstadoClientes.Text = "Error al conectar con la base de datos.";
                System.Diagnostics.Debug.WriteLine($"Error al cargar los datos: {ex.Message}");
            }
        }
    }
}