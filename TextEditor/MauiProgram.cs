using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazorise.RichTextEdit;
using Microsoft.Extensions.Logging;
using TextEditor.Classes;
using TextEditor.Data;
using TextEditor.Models;
using TextEditor.Services;

namespace TextEditor
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
                });
            builder.Services.AddDbContextFactory<Context>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazorise(options =>
                             {
                                 options.Immediate = true;
                             })
                             .AddBootstrap5Providers()
                             .AddFontAwesomeIcons();
            builder.Services .AddBlazoriseRichTextEdit( op => op.QuillJsVersion = "1.3.7");

            builder.Services.AddScoped<EditorService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            House house = new House();
            house.DoSomethingWithTheDog();

            return builder.Build();
        }
    }
}
