using Microsoft.Extensions.Logging;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace SGSB.SIG.APP
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .UseSkiaSharp()
                     .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });    ///teste

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}