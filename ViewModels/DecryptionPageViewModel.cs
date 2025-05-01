using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.ViewModels
{
    public partial class DecryptionPageViewModel : ObservableObject
    {
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
