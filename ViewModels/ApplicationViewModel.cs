using CommunityToolkit.Mvvm.ComponentModel;
using Encryptor_Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.ViewModels
{
    public abstract class ApplicationViewModel : ObservableObject
    {
        public ContentPage? BindingPage { get; set; }

        protected abstract void Reset();
        protected async Task ThrowError(string title, string message, string proceed = "OK", ApplicationError error = ApplicationError.NotIdentified)
        {
            var task = this.BindingPage!.DisplayAlert(title, $"{message}\nError Code: {(int)error}", proceed);
            this.Reset();
            await task;
        }
    }
}
