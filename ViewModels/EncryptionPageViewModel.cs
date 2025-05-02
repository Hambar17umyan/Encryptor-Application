using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Encryptor_Application.Controls;
using Encryptor_Application.Services.Abstract;
using FluentResults;

namespace Encryptor_Application.ViewModels
{
    public partial class EncryptionPageViewModel : ObservableObject
    {
        public EncryptionPageViewModel(IFileConverterService fileConverterService, IEncryptorService encryptorService)
        {
            _fileConverterService = fileConverterService;
            _encryptorService = encryptorService;
        }
        [RelayCommand]
        public Task SubmitFileToEncrypt()
        {
            // Logic to submit file for encryption
            return Task.CompletedTask;
        }

        [RelayCommand]
        public async Task ChooseFileToEncrypt()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select a file to encrypt"
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    _dataToEncrypt = memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions like permission denied, user cancel, etc.
                await Shell.Current.DisplayAlert("Error", $"Failed to pick file: {ex.Message}", "OK");
            }
        }

        private readonly IFileConverterService _fileConverterService;
        private readonly IEncryptorService _encryptorService;

        private byte[]? _dataToEncrypt;
    }
}
