using GalaSoft.MvvmLight.Threading;
using PresentationLayer.ViewModels;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private static void InitializeCultures(CultureInfo cultureInfo) {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        static App() {
            InitializeCultures(new CultureInfo(/*"de-de"*/"en-en"));
            DispatcherHelper.Initialize();
        }

        //protected override void OnStartup(StartupEventArgs e) {
        //    base.OnStartup(e);

        //    MainView app = new MainView();
        //    MainViewModel context = new MainViewModel();
        //    app.DataContext = context;
        //    app.Show();
        //}
    }
}
