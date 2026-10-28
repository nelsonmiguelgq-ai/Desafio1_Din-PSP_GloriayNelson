using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PARTE1PrimerCRUD.Models;
using System.Windows;

namespace PARTE1PrimerCRUD
{
    public partial class App : Application
    {
        //SIN LANDA
        private const string cadenaConexion =
            "server=127.0.0.1;database=CRUDBASICOESCUELA;user=root;password=271020";

        public static IServiceProvider proveedorServicios { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs args)
        {
            try
            {
                base.OnStartup(args);

                ServiceCollection servicios = new ServiceCollection();

                // Le paso el NOMBRE del método (sin paréntesis)
                servicios.AddPooledDbContextFactory<EscuelaBasicContext>(ConfigurarContexto);

                proveedorServicios = servicios.BuildServiceProvider();

                new MainWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al arrancar");
            }
        }

        // Método aparte que configura la conexión
        private void ConfigurarContexto(DbContextOptionsBuilder opciones)
        {
            opciones.UseMySql(cadenaConexion, ServerVersion.AutoDetect(cadenaConexion));
        }
    }
}
//CON LANDA
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using PARTE1PrimerCRUD.Models;
//using System.Windows;

//namespace PARTE1PrimerCRUD
//{
//    public partial class App : Application
//    {
//        private const string cadenaConexion =
//            "server=127.0.0.1;database=CRUDBASICOESCUELA;user=root;password=TU_CONTRASEÑA";

//        public static IServiceProvider proveedorServicios { get; private set; } = null!;

//        protected override void OnStartup(StartupEventArgs args)
//        {
//            try
//            {
//                base.OnStartup(args);

//                ServiceCollection servicios = new ServiceCollection();

//                // La configuración va aquí mismo, con la flecha =>
//                servicios.AddPooledDbContextFactory<EscuelaBasicContext>(opciones =>
//                    opciones.UseMySql(cadenaConexion, ServerVersion.AutoDetect(cadenaConexion)));

//                proveedorServicios = servicios.BuildServiceProvider();

//                new MainWindow().Show();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.ToString(), "Error al arrancar");
//            }
//        }
//    }
//}
