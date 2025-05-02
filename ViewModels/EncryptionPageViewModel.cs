using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Encryptor_Application.Controls;
using Encryptor_Application.Services.Abstract;
using FluentResults;

namespace Encryptor_Application.ViewModels
{
    public partial class EncryptionPageViewModel : ObservableObject
    {
        public EncryptionPageViewModel(IFileConverterService fileConverterService, IEncryptorService encryptorService, IFileManagerService fileManagerService, IStringConverterService stringConverterService)
        {
            _fileConverterService = fileConverterService;
            _encryptorService = encryptorService;
            _fileManagerService = fileManagerService;
            _stringConverterService = stringConverterService;
            IsMangoSleepVisible = false;
        }

        public bool IsMangoSleepVisible { get; set; }

        [RelayCommand]
        public async Task SubmitFileToEncrypt()
        {
            if (_dataToEncrypt == null)
            {
                Shell.Current.DisplayAlert(Shell.Current.Title, "Please select a file to encrypt.", "OK").Wait();
                return;
            }

            var encryptedTask = _encryptorService.EncryptAsync(_dataToEncrypt);
            IsMangoSleepVisible = true;

            var result = await encryptedTask;
            var line = _stringConverterService.CreateStringFromIntCollection(result);
            var tempFileResult = await _fileManagerService.TryCreateTempTxtFileAsync(line);
            if (tempFileResult.IsSuccess)
            {
                var res = await _fileManagerService.SaveFileToUserLocationAsync(tempFileResult.Value);
                if (res.IsSuccess)
                {
                    Shell.Current.DisplayAlert(Shell.Current.Title, "File saved successfully.", "OK").Wait();
                }
                else
                {
                    Shell.Current.DisplayAlert(Shell.Current.Title, res.Errors[0].Message, "OK").Wait();
                }
            }
            else
            {
                Shell.Current.DisplayAlert(Shell.Current.Title, tempFileResult.Errors[0].Message, "OK").Wait();
            }
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
                await Shell.Current.DisplayAlert("Error", $"Failed to pick file: {ex.Message}", "OK");
            }
        }

        private readonly IFileConverterService _fileConverterService;
        private readonly IEncryptorService _encryptorService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IStringConverterService _stringConverterService;
        private byte[]? _dataToEncrypt;
    }
}
