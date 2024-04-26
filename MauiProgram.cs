using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace RUMMY_SCOREKEEPER
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.UseMauiApp<App>().UseMauiCommunityToolkitMarkup();
            
            builder.Services.AddSingleton<GamesPage>();
            builder.Services.AddSingleton<GamesViewModel>();
            
            builder.Services.AddTransient<NewGamePage>();
            builder.Services.AddTransient<NewGameViewModel>();

            builder.Services.AddSingleton<CurrentGamePage>();
            builder.Services.AddSingleton<CurrentGameViewModel>();

            return builder.Build();
        }
    }
}
