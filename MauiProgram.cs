using Encryptor_Application.Pages;
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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<MainPageViewModel>()
			.AddSingleton<MainPage>();

        return builder.Build();
	}
}
