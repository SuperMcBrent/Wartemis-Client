using GalaSoft.MvvmLight.Command;
using PresentationLayer.Framework;
using PresentationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels {
    public class LoginViewModel : ApplicationViewModelBase, IPageViewModel {

        public string Name { get; private set; } = "Login";

        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand RegisterCommand { get; private set; }

        public LoginViewModel() {
            RegisterCommand = new RelayCommand(Register);
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private void Register() {
            Debug.WriteLine("Register");
        }

        private void Login() {
            Debug.WriteLine("Login");
        }

        private bool CanLogin() {
            return true;
        }

    }
}
