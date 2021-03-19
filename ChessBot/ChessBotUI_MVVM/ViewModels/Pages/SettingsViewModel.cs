using ChessBotUI_MVVM.Framework;
using ChessBotUI_MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotUI_MVVM.ViewModels.Pages {
    public class SettingsViewModel : ApplicationViewModelBase, IPageViewModel {
        public string Name { get; private set; } = "Settings";
    }
}
