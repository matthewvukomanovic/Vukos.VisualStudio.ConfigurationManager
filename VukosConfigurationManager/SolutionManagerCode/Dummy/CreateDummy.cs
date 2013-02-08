using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VukosConfigurationManager.Dummy
{
    public static class CreateDummy
    {
        public static ISolutionView CreateSolution()
        {
            SolutionViewDummy solution = new SolutionViewDummy();
            solution.ActiveConfiguration = "Debug";
            solution.Configurations = new List<string>() { "Debug", "Release" };
            solution.ActivePlatform = "Mixed CPUs";
            solution.Platforms = new List<string>() { "Mixed CPUs", "x86", "64" };

            List<IProjectView> projects = new List<IProjectView>();

            Random rand = new Random(0);

            string[] randomWords = new string[] { "y", "asdf", "hello", "something", "extra", "here", "today", "i", "dont", "know", "what", "to", "expect" };

            for (int i = 0; i < 20; i++)
            {
                ProjectViewDummy p = new ProjectViewDummy();
                p.ConfigurationName = "Debug";
                p.NameSet = "Project " + randomWords[rand.Next(randomWords.Length)] + " " + i.ToString();
                p.PlatformNameSet = "x86";
                p.ShouldBuild = (i % 2) == 0;
                p.ShouldDeploy = (i % 3) == 0;

                p.IsBuildableSet = rand.Next(10) != 0;
                p.IsDeployableSet = false;
                p.IsSelected = false;
                projects.Add(p);
            }
            solution.Projects = projects;
            return solution;
        }

        public static void Show(ISolutionView solution)
        {
            VukosConfigurationManager.ConfigurationWindow w = new VukosConfigurationManager.ConfigurationWindow();
            w.DataContext = solution;
            w.Show();
        }

        public static void ShowDummy()
        {
            Show(CreateSolution());
        }
    }
}
