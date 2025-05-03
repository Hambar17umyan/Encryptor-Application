using Encryptor_Application.ViewModels;
namespace Encryptor_Application.Pages;


public partial class DecryptionPage : ContentPage
{
	public DecryptionPage(DecryptionPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
		viewModel.BindingPage = this;
    }
}