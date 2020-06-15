using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using Typewriter.Core;
using Typewriter.Services;
using Typewriter.Views;
using TypeWriter.Windows.Interfaces;
using TypeWriter.Windows.Services;

namespace Typewriter
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            var mainWindow = new Window();

            var contentControl = new ContentControl();

            mainWindow.Content = contentControl;
            mainWindow.MinHeight = 400;
            mainWindow.MinWidth = 400;
            mainWindow.WindowState = WindowState.Maximized;
            mainWindow.Loaded += (s, e) => { Container.Resolve<IRegionManager>().RequestNavigate(RegionKey.Main.ToString(), ViewKey.Main.ToString()); };
            contentControl.SetValue(RegionManager.RegionNameProperty, RegionKey.Main.ToString());

            return mainWindow;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IManuscriptService>(new ManuscriptService());

            containerRegistry.RegisterForNavigation<ProjectStructureView>(ViewKey.ProjectStructure.ToString());
            containerRegistry.RegisterForNavigation<ProjectItemContentView>(ViewKey.ProjectItemContent.ToString());

            containerRegistry.RegisterForNavigation<MainView>(ViewKey.Main.ToString());
            containerRegistry.Register<IWindowsDialogService, WindowsDialogService>();
        }
    }
}
