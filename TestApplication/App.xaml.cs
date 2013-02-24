using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Vukos.VisualStudio.ConfigurationManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Vukos.VisualStudio.ConfigurationManager.Dummy.CreateDummy.ShowDummy();
        }
    }
}
