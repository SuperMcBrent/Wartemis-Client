/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Notes.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:Notes.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/
using ChessBotUI_MVVM.ViewModels;
using ChessBotUI_MVVM.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ChessBotUI_MVVM.Framework {
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Notes.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:Notes.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator {
        private static Bootstrapper _bootStrapper;

        static ViewModelLocator() {
            if (_bootStrapper == null)
                _bootStrapper = new Bootstrapper();
        }

        public LoginViewModel Login {
            get { return _bootStrapper.Container.Resolve<LoginViewModel>(); }
        }

        public MainViewModel Main {
            get { return _bootStrapper.Container.Resolve<MainViewModel>(); }
        }

        public DashBoardViewModel DashBoard {
            get { return _bootStrapper.Container.Resolve<DashBoardViewModel>(); }
        }

        public SettingsViewModel Settings {
            get { return _bootStrapper.Container.Resolve<SettingsViewModel>(); }
        }

        public BoardViewModel Board {
            get { return _bootStrapper.Container.Resolve<BoardViewModel>(); }
        }

        public BotViewModel Bot {
            get { return _bootStrapper.Container.Resolve<BotViewModel>(); }
        }
    }
}
