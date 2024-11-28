using Microsoft.Extensions.Logging;
using UTS_72210454.Data;

namespace UTS_72210454
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            string dbPath = FileAccessHelper.GetLocalFilePath("Categories.db3");
            builder.Services.AddSingleton<DatabaseHelper>(s => ActivatorUtilities.CreateInstance<DatabaseHelper>(s, dbPath));
            return builder.Build();
        }
    }
}
