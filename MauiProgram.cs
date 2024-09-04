using CommunityToolkit.Maui;
using KeroFruits.Utilities.DataAccess;
using KeroFruits.Utilities.Interfaces;
using KeroFruits.Utilities.Services;
using KeroFruits.View;
using KeroFruits.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace KeroFruits
{
    public static class MauiProgram
    {
        public static SqlConnection con = new SqlConnection("Data Source=192.168.1.150,1433;Initial Catalog=Usine;User ID=sa;Password=Magnos0182163958;Integrated Security=True;MultiSubnetFailover=True;Encrypt=true;TrustServerCertificate=True;Trusted_Connection=false;Pooling=False");
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })

            .UseMauiCommunityToolkit();

            builder.Services.AddSingleton<IAlertService>(new AlertServiceDisplay());
            builder.Services.AddSingleton<DataAccess>(new DataAccessSqlFile());

            builder.Services.AddTransient<Reservations>();
            builder.Services.AddTransient<ReservationsViewModel>();
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientsPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
