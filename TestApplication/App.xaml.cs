using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using VukosConfigurationManager;

namespace VukosConfigurationManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SolutionView solution = new SolutionView();
            solution.ActiveConfiguration = "Debug";
            solution.Configurations = new List<string>() { "Debug", "Release" };
            solution.ActivePlatform = "Mixed CPUs";
            solution.Platforms = new List<string>() { "Mixed CPUs", "x86", "64" };

            List<IProjectView> projects = new List<IProjectView>();

            Random rand = new Random(0);
            for (int i = 0; i < 20; i++)
            {
                ProjectView p = new ProjectView();
                p.ConfigurationName = "Debug";
                p.NameSet = "Project " + i.ToString();
                p.PlatformNameSet = "x86";
                p.ShouldBuild = (i % 2) == 0;
                p.ShouldDeploy = (i % 3) == 0;

                p.IsBuildableSet = rand.Next(10) != 0;
                p.IsDeployableSet = rand.Next(10) == 0;
                p.IsSelected = false;
                projects.Add(p);
            }
            solution.Projects = projects;
            VukosConfigurationManager.ConfigurationWindow w = new VukosConfigurationManager.ConfigurationWindow();
            w.DataContext = solution;
            w.Show();
        }
    }
}
