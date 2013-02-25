using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vukos.VisualStudio.ConfigurationManager.Dummy
{
    public static class CreateDummy
    {
        public static ISolutionViewModel CreateSolution()
        {
            SolutionViewModelDummy solution = new SolutionViewModelDummy();
            solution.SelectedConfiguration = "Debug";
            solution.Configurations = new List<string>() { "Debug", "Release" };
            solution.ActivePlatform = "Mixed CPUs";
            solution.Platforms = new List<string>() { "Mixed CPUs", "x86", "64" };

            List<IProjectViewModel> projects = new List<IProjectViewModel>();

            Random rand = new Random(0);

            string[] randomWords = new string[] { "y", "asdf", "hello", "something", "extra", "here", "today", "i", "dont", "know", "what", "to", "expect" };

            for (int i = 0; i < 20; i++)
            {
                ProjectViewModelDummy p = new ProjectViewModelDummy();
                p.ConfigurationName = "Debug";
                p.NameSet = "Project " + randomWords[rand.Next(randomWords.Length)] + " " + i.ToString();
                p.PlatformNameSet = "x86";
                p.ShouldBuild = (i % 2) == 0;
                p.ShouldDeploy = (i % 3) == 0;

                p.IsBuildableSet = rand.Next(10) != 0;
                p.IsDeployableSet = rand.Next(10) == 0;
                p.IsSelected = false;
                projects.Add(p);
            }
            solution.Projects = projects;
            return solution;
        }

        public static void ShowDummy()
        {
            Vukos.VisualStudio.ConfigurationManager.ConfigurationWindow.Show(CreateSolution());
        }
    }
}
