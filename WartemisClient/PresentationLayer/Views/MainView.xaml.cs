using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.Views {
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window {
        public MainView() {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void ListView_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            try {
                var listView = sender as ListView;
                ListViewItem selectedItem = ((Button)e.OriginalSource).Parent as ListViewItem;

                if (selectedItem is null) return;

                foreach (ListViewItem listViewItem in listView.Items) {
                    listViewItem.IsSelected = false;
                }
                selectedItem.IsSelected = true;
            } catch (Exception) {
                
            }
        }
    }
}
