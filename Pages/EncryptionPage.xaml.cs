
using Encryptor_Application.Pages;
namespace Encryptor_Application.Pages;

public partial class EncryptionPage : ContentPage
{
	public EncryptionPage(EncryptionPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}