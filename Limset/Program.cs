using Limset.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Limset
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MINE
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<LimSet_DbContext>();
            serviceCollection.AddScoped<Ilogin_service, login_service>();
            serviceCollection.AddScoped<Iadmin_service, admin_service>();
            var serviceProvider = serviceCollection.BuildServiceProvider();


            using (var context = new LimSet_DbContext())
            {
                var admin_user = context.users.Any(x => x.role == "Admin");
                if(admin_user)
                {
                    Application.Run(new Login());
                }
                else
                {
                    Application.Run(new Add_User());
                }
            }            
        }               
    }
}