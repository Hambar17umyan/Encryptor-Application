using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Encryptor_Application.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [RelayCommand]
        public Task SubmitFileToEncrypt()
        {
            // Logic to submit file for encryption
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ChooseFileToEncrypt()
        {
            // Logic to choose file for encryption
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task SubmitFileToDecrypt()
        {
            // Logic to submit file for encryption
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task ChooseFileToDecrypt()
        {
            // Logic to choose file for encryption
            return Task.CompletedTask;
        }
    }
}
