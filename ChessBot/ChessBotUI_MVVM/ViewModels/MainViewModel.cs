using GalaSoft.MvvmLight.Command;
using PresentationLayer.Framework;
using PresentationLayer.Interfaces;
using PresentationLayer.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels {
    public class MainViewModel : ApplicationViewModelBase, IPageViewModel {

        public string Name { get; } = "Main";

        public RelayCommand ExitCommand { get; private set; }
        public RelayCommand<string> ChangePageCommand { get; private set; }

        private ObservableCollection<IPageViewModel> _pageViewModels;
        private IPageViewModel _currentPageViewModel;

        public MainViewModel() {
            PageViewModels.Add(new DashBoardViewModel());
            PageViewModels.Add(new SettingsViewModel());
            PageViewModels.Add(new BotViewModel());
            PageViewModels.Add(new ChessBoardViewModel());
            PageViewModels.Add(new StatesViewModel());

            CurrentPageViewModel = PageViewModels[0];

            ExitCommand = new RelayCommand(Exit);
            ChangePageCommand = new RelayCommand<string>(ChangeViewModel);
        }

        public ObservableCollection<IPageViewModel> PageViewModels {
            get {
                if (_pageViewModels == null)
                    _pageViewModels = new ObservableCollection<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel {
            get {
                return _currentPageViewModel;
            }
            set {
                if (_currentPageViewModel != value) {
                    _currentPageViewModel = value;
                    RaisePropertyChanged(() => CurrentPageViewModel);
                }
            }
        }

        private void ChangeViewModel(string vmName) {
            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm.Name == vmName);
            Debug.WriteLine($"Changing to {CurrentPageViewModel?.Name}");
        }

        private void Exit() {
            Application.Current.Shutdown();
        }
    }
}
