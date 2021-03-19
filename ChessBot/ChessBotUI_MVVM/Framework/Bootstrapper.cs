﻿using ChessBotUI_MVVM.Interfaces;
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
    /// Here the DI magic come on.
    /// </summary>
    public class Bootstrapper {
        public IUnityContainer Container { get; set; }

        public Bootstrapper() {
            Container = new UnityContainer();

            ConfigureContainer();
        }

        /// <summary>
        /// We register here every service / interface / viewmodel.
        /// </summary>
        private void ConfigureContainer() {
            Container.RegisterType<LoginViewModel>();
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<DashBoardViewModel>();
            Container.RegisterType<SettingsViewModel>();
            Container.RegisterType<BoardViewModel>();
            Container.RegisterType<BotViewModel>();
            Container.RegisterType<IPageViewModel>();
        }
    }
}
