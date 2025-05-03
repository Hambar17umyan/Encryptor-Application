
using Encryptor_Application.Pages;
using Encryptor_Application.ViewModels;
namespace Encryptor_Application.Pages;

public partial class EncryptionPage : ContentPage
{
	public EncryptionPage(EncryptionPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
		viewModel.BindingPage = this;
    }
}