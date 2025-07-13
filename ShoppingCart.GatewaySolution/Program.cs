using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using ShoppingCart.GatewaySolution.Extentions;
using System.Threading.Tasks;

namespace ShoppingCart.GatewaySolution
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("ocelot.json",optional:false,reloadOnChange:true);
            builder.Services.AddOcelot(builder.Configuration);

            builder.AddAppAuthentication();
            var app = builder.Build();
           
            app.MapGet("/", () => "Hello World!");
            await app.UseOcelot();
            await app.RunAsync();
        }
    }
}
