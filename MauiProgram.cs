using CommunityToolkit.Maui;
using Encryptor_Application.Pages;
using Encryptor_Application.Services.Abstract;
using Encryptor_Application.Services.Concrete;
using Encryptor_Application.ViewModels;
using Microsoft.Extensions.Logging;

namespace Encryptor_Application;

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
            })
        .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.ConfigureInjection();

        return builder.Build();
    }

    private static MauiAppBuilder InjectServices(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<IEncryptorService, EncryptorService>()
            .AddSingleton<IFileConverterService, FileConverterService>()
            .AddSingleton<IFileManagerService, FileManagerService>()
            .AddSingleton<IStringConverterService, StringConverterService>();
        return builder;
    }

    private static MauiAppBuilder InjectPages(this MauiAppBuilder builder)
    {
        builder.Services
            .AddTransient<MainPage>()
            .AddTransient<EncryptionPage>()
            .AddTransient<DecryptionPage>();

        return builder;
    }

    private static MauiAppBuilder InjectViewModels(this MauiAppBuilder builder)
    {
        builder.Services
            .AddTransient<MainPageViewModel>()
            .AddTransient<EncryptionPageViewModel>()
            .AddTransient<DecryptionPageViewModel>();

        return builder;
    }

    private static MauiAppBuilder ConfigureInjection(this MauiAppBuilder builder)
    {
        return builder
            .InjectServices()
            .InjectPages()
            .InjectViewModels();
    }
}
