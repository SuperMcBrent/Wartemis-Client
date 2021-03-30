using PresentationLayer.Framework;
using PresentationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels.Pages {
    public class BoardViewModel : ApplicationViewModelBase, IPageViewModel {
        public string Name { get; private set; } = "Board";
    }
}
