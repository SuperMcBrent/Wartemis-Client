using ChessBotUI_MVVM.Framework;
using ChessBotUI_MVVM.Interfaces;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotUI_MVVM.ViewModels.Pages {
    public class DashBoardViewModel : ApplicationViewModelBase, IPageViewModel {

        public string Name { get; private set; } = "DashBoard";

    }
}
