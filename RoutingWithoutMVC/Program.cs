namespace RoutingWithoutMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews(); // service register for controllers

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.MapDefaultControllerRoute(); // it only call index in home controller

            /* Convenion based routing */

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=About}/{id?}"
            //    );
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=User}/{action=Index}/{id?}"
            //    );

            /* Attribute based routing */
            app.MapControllers();

            /* In same project we can use Attrubute and convenstion based routing*/
            app.Run();
        }
    }
}
