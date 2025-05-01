using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Encryptor_Application.Controls;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.ViewModels
{
    public partial class EncryptionPageViewModel : ObservableObject
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
    }
}
