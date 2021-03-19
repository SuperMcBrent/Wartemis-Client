using GalaSoft.MvvmLight.Command;
using ChessBotUI_MVVM.Framework;
using ChessBotUI_MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotUI_MVVM.ViewModels {
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
