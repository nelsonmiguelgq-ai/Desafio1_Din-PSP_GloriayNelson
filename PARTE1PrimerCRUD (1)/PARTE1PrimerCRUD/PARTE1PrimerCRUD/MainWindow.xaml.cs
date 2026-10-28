using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PARTE1PrimerCRUD.Models;
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

namespace PARTE1PrimerCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // el contexto lo tiene que sacar de la variable proveedorServicios
        private readonly IDbContextFactory<EscuelaBasicContext> escuelaContext;

        //
        public MainWindow()
        {

            try
            {
                InitializeComponent();

                //necesitamos obtener le servicio
                //- usar GetRequiredService para que devuelva excepcion
                //EscuelaBasicContext al ser DBCONTEXT NO SE PUEDE USAR CON HILOS - NO SE PUEDE USAR EN 
                //VARIOS HILOS  A LA VEZ se necesita IDbContextFactory
                escuelaContext =  App.proveedorServicios.GetRequiredService
                    <IDbContextFactory<EscuelaBasicContext>>();

              
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                MessageBox.Show(ex.ToString(), "Error en MainWindow");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            using var contexto = escuelaContext.CreateDbContext();   // pido un mensajero
            dgEstudiantes.ItemsSource = contexto.Estudiantes.ToList(); // traigo los alumnos a la tabla
                                                                       // al terminar el método, el mensajero se devuelve solo

        }

        private void Insertar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}