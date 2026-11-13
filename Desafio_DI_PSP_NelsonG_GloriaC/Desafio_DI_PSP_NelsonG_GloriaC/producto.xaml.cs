using Desafio_DI_PSP_NelsonG_GloriaC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    public partial class producto : Window
    {
        private readonly IDbContextFactory<EscuelaBasicContext> _dbContextFactory;


        public producto()
        {
            InitializeComponent();
            _dbContextFactory = App.proveedorServicios.GetRequiredService<IDbContextFactory<EscuelaBasicContext>>();
            _=CargarProductosAsync();
        }

        private async Task CargarProductosAsync()
            {
                try
                {
                    // Guardar el Id del cliente seleccionado para volver a seleccionarlo después
                    int? idSeleccionado = (NavProd.SelectedItem as Producto)?.IdProducto;

                    using (var context = _dbContextFactory.CreateDbContext())
                    {
                        // AsNoTracking: solo lectura, siempre trae lo que hay en la BD en este momento
                        var listaClientes = await context.Productos
                                                         .AsNoTracking()
                                                         .OrderBy(c => c.IdProducto)
                                                         .ToListAsync();

                    NavProd.ItemsSource = listaClientes;

                        // Restaurar la selección si ese cliente sigue existiendo
                        if (idSeleccionado != null)
                        NavProd.SelectedItem = listaClientes.FirstOrDefault(c => c.IdProducto == idSeleccionado);

                    txtEstadoProductos.Text = $"Datos actualizados ({listaClientes.Count} clientes) - {DateTime.Now:HH:mm:ss}";
                    }
                }
                catch (Exception ex)
                {
                txtEstadoProductos.Text = "Error al conectar con la base de datos.";
                    System.Diagnostics.Debug.WriteLine($"Error al cargar los datos: {ex.Message}");
                }
            }

        private void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            CargarProductosAsync();
        }
    }
}