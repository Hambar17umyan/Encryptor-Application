using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Encryptor_Application.Enums;
using Encryptor_Application.Services.Abstract;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.ViewModels
{
    public partial class DecryptionPageViewModel : ApplicationViewModel
    {
        public DecryptionPageViewModel(IFileConverterService fileConverterService, IEncryptorService encryptorService, IFileManagerService fileManagerService, IStringConverterService stringConverterService)
        {
            this._fileConverterService = fileConverterService;
            this._encryptorService = encryptorService;
            this._fileManagerService = fileManagerService;
            this._stringConverterService = stringConverterService;

            this.LoadingStackIsVisible = false;
            this.SubmitFileToDecryptButtonIsEnabled = true;
            this.ChooseFileToDecryptButtonIsEnabled = true;
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
        public bool SubmitFileToDecryptButtonIsEnabled
        {
            get => _submitFileToDecryptButtonIsEnabled;
            set
            {
                if (value != _submitFileToDecryptButtonIsEnabled)
                {
                    _submitFileToDecryptButtonIsEnabled = value;
                    OnPropertyChanged(nameof(SubmitFileToDecryptButtonIsEnabled));
                }
            }
        }
        public bool ChooseFileToDecryptButtonIsEnabled
        {
            get => _chooseFileToDecryptButtonIsEnabled;
            set
            {
                if (value != _chooseFileToDecryptButtonIsEnabled)
                {
                    _chooseFileToDecryptButtonIsEnabled = value;
                    OnPropertyChanged(nameof(ChooseFileToDecryptButtonIsEnabled));
                }
            }
        }

        [RelayCommand]
        public async Task SubmitFileToDecrypt()
        {
            this.SubmitFileToDecryptButtonIsEnabled = false;
            this.ChooseFileToDecryptButtonIsEnabled = false;

            if (this._dataToDecrypt == null)
            {
                await this.ThrowError("Error", "Please select a file to decrypt.", "OK", ApplicationError.FileNotSelectedToDecrypt);
                return;
            }

            this.LoadingStackIsVisible = true;

            Result<int[]> intResult;
            try
            {
                intResult = this._stringConverterService.TryRetrieveIntCollection(_dataToDecrypt);
            }
            catch (Exception)
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "Be sure that you have uploaded the right file.", "OK", ApplicationError.DecIntCollWrongRetrievedExc);
                return;
            }

            int[] ints;
            if (intResult.IsSuccess)
            {
                ints = intResult.Value;
            }
            else
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "Be sure that you have uploaded the right file.", "OK", ApplicationError.DecIntCollWrongRetrieved);
                return;
            }

            byte[] result;
            try
            {
                var decryptedResult = await this._encryptorService.TryDecryptAsync(ints);
                if (decryptedResult.IsSuccess)
                {
                    result = decryptedResult.Value;
                }
                else
                {
                    this.LoadingStackIsVisible = false;
                    await this.ThrowError("Error", "Be sure that you have uploaded the right file.", "OK", ApplicationError.DecDecryptionFail);
                    return;
                }
            }
            catch (Exception)
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.DataToDecryptIsInvalid);
                return;
            }
            string input;
            try
            {
                input = await BindingPage!.DisplayPromptAsync(
                    "Enter File Extension(jpg, png, pdf...)",              // Title
                    "", // Message
                    "OK",                      // Accept button text
                    "Cancel",                  // Cancel button text
                    "Type here...",            // Placeholder
                    maxLength: 100,            // Optional max length
                    keyboard: Keyboard.Text    // Keyboard type
                    );
            }
            catch (Exception)
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "Some error occurred while reading input.", "OK", ApplicationError.DecReadFileExtensionExc);
                return;
            }
            if (string.IsNullOrEmpty(input))
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "The extension is empty.", "OK", ApplicationError.DecInputExtensionIsEmpty);
                return;
            }
            if (input.Length > 10)
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "The extension is too long.", "OK", ApplicationError.DecInputExtensionIsTooLong);
                return;
            }
            input = input.ToLower();
            if (!input.All(x => (x >= 'a' && x <= 'z') || (x >= '0' && x <= '9')))
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "The extension should contain only letters and numbers.", "OK", ApplicationError.DecInputExtensionIsNotNumOrLet);
                return;
            }

            input = $"DecryptedFile.{input}";

            string tempPath = FileSystem.CacheDirectory;
            var tempFilePath = Path.Combine(tempPath, input);

            var tempFileResult = _fileConverterService.TryConvertByteCollectionToFile(result, tempFilePath);
            if (tempFileResult.IsSuccess)
            {
                var tempPath1 = tempFileResult.Value;
                if (tempPath is null)
                {
                    this.LoadingStackIsVisible = false;
                    await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.DecTempPathIsNull);
                    return;
                }
                try
                {
                    this.LoadingStackIsVisible = false;
                    await Launcher.OpenAsync(new OpenFileRequest
                    {
                        File = new ReadOnlyFile(tempPath1)
                    });
                }
                catch (Exception)
                {
                    this.LoadingStackIsVisible = false;
                    await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.DecLauncherError);
                    return;
                }

                Reset();
            }
            else
            {
                this.LoadingStackIsVisible = false;
                await this.ThrowError("Error", "An internal error has occurred. Try again!", "OK", ApplicationError.DecTempFileResultIsFail);
                return;
            }
        }

        [RelayCommand]
        public async Task ChooseFileToDecrypt()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select a text file",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, new[] { ".txt" } },
                        { DevicePlatform.MacCatalyst, new[] { ".txt" } },
                        { DevicePlatform.iOS, new[] { "public.plain-text" } },
                        { DevicePlatform.Android, new[] { "text/plain" } }
                    })
                });

                if (result != null)
                {
                    if (!Path.GetExtension(result.FileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        await ThrowError("Error", "You should only upload txt files.", "OK", ApplicationError.DecNotTxtUploaded);
                        return;
                    }

                    using var stream = await result.OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    this._dataToDecrypt = await reader.ReadToEndAsync();
                }
            }
            catch (Exception)
            {
                await this.ThrowError("Error", $"Failed to pick file.", "OK", ApplicationError.DecFailToPickFile);
            }
        }

        private readonly IFileConverterService _fileConverterService;
        private readonly IEncryptorService _encryptorService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IStringConverterService _stringConverterService;
        private string? _dataToDecrypt;
        private bool _loadingStackIsVisible;
        private bool _submitFileToDecryptButtonIsEnabled;
        private bool _chooseFileToDecryptButtonIsEnabled;

        protected override void Reset()
        {
            this._dataToDecrypt = null;
            this.SubmitFileToDecryptButtonIsEnabled = true;
            this.ChooseFileToDecryptButtonIsEnabled = true;
        }
    }
}
