using Desafio_DI_PSP_NelsonG_GloriaC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Desafio_DI_PSP_NelsonG_GloriaC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CadenaConexion=
            "Server=localhost;Database=bdatos_inicio;Uid=root;Pwd=271020;";

        public static IServiceProvider proveedorServicios { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs arg)
        {
            try
            {
                base.OnStartup(arg);

                ServiceCollection servicios = new ServiceCollection();

                servicios.AddPooledDbContextFactory<EscuelaBasicContext>(ConfiContext);

                proveedorServicios = servicios.BuildServiceProvider();


                new MainWindow().Show();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error al arrancar");
        }
    }

        //Este metodo nos configura la conexion 

        private void ConfiContext(DbContextOptionsBuilder opciones)
        {
            opciones.UseMySql(CadenaConexion, ServerVersion.AutoDetect(CadenaConexion));
        }
    }

}
