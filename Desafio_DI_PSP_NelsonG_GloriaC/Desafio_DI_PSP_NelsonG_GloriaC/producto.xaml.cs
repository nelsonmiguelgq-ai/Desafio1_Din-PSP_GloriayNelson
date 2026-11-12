using Desafio_DI_PSP_NelsonG_GloriaC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    public partial class producto : Window
    {
        private string conexion = "Server=localhost;Database=bdatos_inicio;Uid=root;Pwd=271020;";

        public producto()
        {
            InitializeComponent();
            CargarProductosAsync();
        }

        private async void CargarProductosAsync()
        {
           
            // 3. Usas el contexto de la base de datos de Entity Framework Core
            using (var context = new EscuelaBasicContext())
            {
                // 'Productos' es la tabla generada automáticamente por el Scaffold
                var lista = await context.Productos.ToListAsync();

                // 4. Se lo asignas al DataGrid de tu interfaz visual (producto.xaml)
                NavProd.ItemsSource = lista;


            }
        }

        private void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            CargarProductosAsync();
        }
    }
}