using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Encryptor_Application.Controls;
using Encryptor_Application.Enums;
using Encryptor_Application.Services.Abstract;
using FluentResults;

namespace Encryptor_Application.ViewModels
{
    public partial class EncryptionPageViewModel : ApplicationViewModel
    {
        public EncryptionPageViewModel(IFileConverterService fileConverterService, IEncryptorService encryptorService, IFileManagerService fileManagerService, IStringConverterService stringConverterService)
        {
            this._fileConverterService = fileConverterService;
            this._encryptorService = encryptorService;
            this._fileManagerService = fileManagerService;
            this._stringConverterService = stringConverterService;

            this.LoadingStackIsVisible = false;
            this.SubmitFileToEncryptButtonIsEnabled = true;
            this.ChooseFileToEncryptButtonIsEnabled = true;
        }

        public bool LoadingStackIsVisible
        {
            get => this._loadingStackIsVisible;
            set
            {
                if (_loadingStackIsVisible != value)
                {
                    this._loadingStackIsVisible = value;
                    this.OnPropertyChanged(nameof(LoadingStackIsVisible));
                }
            }
        }
        public bool SubmitFileToEncryptButtonIsEnabled
        {
            get => _submitFileToEncryptButtonIsEnabled;
            set
            {
                if(value!=_submitFileToEncryptButtonIsEnabled)
                {
                    _submitFileToEncryptButtonIsEnabled = value;
                    OnPropertyChanged(nameof(SubmitFileToEncryptButtonIsEnabled));
                }
            }
        }
        public bool ChooseFileToEncryptButtonIsEnabled
        {
            get => _chooseFileToEncryptButtonIsEnabled;
            set
            {
                if (value != _chooseFileToEncryptButtonIsEnabled)
                {
                    _chooseFileToEncryptButtonIsEnabled = value;
                    OnPropertyChanged(nameof(ChooseFileToEncryptButtonIsEnabled));
                }
            }
        }

        [RelayCommand]
        public async Task SubmitFileToEncrypt()
        {
            this.SubmitFileToEncryptButtonIsEnabled = false;
            this.ChooseFileToEncryptButtonIsEnabled = false;

            if (this._dataToEncrypt == null)
            {
                await this.ThrowError("Error", "Please select a file to encrypt.", "OK", ApplicationError.FileNotSelectedToEncrypt);
                return;
            }

            this.LoadingStackIsVisible = true;
            int[] result;
            try
            {
                var encryptedTask = this._encryptorService.EncryptAsync(this._dataToEncrypt);
                result = await encryptedTask;
            }
            catch (Exception)
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.DataToEncryptIsNull);
                return;
            }

            string text;
            try
            {
                text = this._stringConverterService.CreateStringFromIntCollection(result);
            }
            catch (Exception)
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.DataToEncryptIsNull);
                return;
            }

            var tempFileResult = await _fileManagerService.TryCreateTempTxtFileAsync(text);
            if (tempFileResult.IsSuccess)
            {
                var tempPath = tempFileResult.Value;
                if (tempPath is null)
                {
                    this.LoadingStackIsVisible = false;
                    await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.EncTempPathIsNull);
                    return;
                }
                try
                {
                    this.LoadingStackIsVisible = false;
                    await Share.Default.RequestAsync(new ShareFileRequest
                    {
                        Title = "Share your text file",
                        File = new ShareFile(tempPath)
                    });

                }
                catch (Exception)
                {
                    this.LoadingStackIsVisible = false;
                    await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.EncLauncherError);
                    return;
                }

                Reset();
            }
            else
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.EncTempFileResultIsFail);
                return;
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
            catch (Exception)
            {
                await this.ThrowError("Error", $"Failed to pick file.", "OK", ApplicationError.EnFailToPickFile);
            }
        }

        private readonly IFileConverterService _fileConverterService;
        private readonly IEncryptorService _encryptorService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IStringConverterService _stringConverterService;
        private byte[]? _dataToEncrypt;
        private bool _loadingStackIsVisible;
        private bool _submitFileToEncryptButtonIsEnabled;
        private bool _chooseFileToEncryptButtonIsEnabled;

        protected override void Reset()
        {
            this._dataToEncrypt = null;
            this.SubmitFileToEncryptButtonIsEnabled = true;
            this.ChooseFileToEncryptButtonIsEnabled = true;
        }
    }
}
