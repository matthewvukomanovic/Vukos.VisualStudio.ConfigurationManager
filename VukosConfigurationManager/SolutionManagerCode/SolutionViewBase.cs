using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace VukosConfigurationManager
{
    public abstract class SolutionViewBase : ViewModelBase, ISolutionView
    {
        public SolutionViewBase()
        {
            ToggleBuild = new ToggleDelegateCommand(ToggleSelectedBuilds);
            ToggleDeploy = new ToggleDelegateCommand(ToggleSelectedDeploys);
        }

        public abstract string ActiveConfiguration { get; set; }
        public abstract string ActivePlatform { get; set; }

        #region Projects

        /// <summary>
        /// A backing store for the property <see cref="Projects"/>
        /// </summary>
        private IList<IProjectView> _projects;

        public IList<IProjectView> Projects
        {
            get { return _projects; }
            set
            {
                //if(!object.ReferenceEquals(_projects, value))
                if (_projects != value)
                {
                    _projects = value;
                    this.RaisePropertyChanged(() => this.Projects);
                }
            }
        }

        #endregion

        #region Configurations

        /// <summary>
        /// A backing store for the property <see cref="Configurations"/>
        /// </summary>
        private IEnumerable<string> _configurations;

        public IEnumerable<string> Configurations
        {
            get { return _configurations; }
            set
            {
                //if(!object.ReferenceEquals(_configurations, value))
                if (_configurations != value)
                {
                    _configurations = value;
                    this.RaisePropertyChanged(() => this.Configurations);
                }
            }
        }

        #endregion

        #region Platforms

        /// <summary>
        /// A backing store for the property <see cref="Platforms"/>
        /// </summary>
        private IEnumerable<string> _platforms;

        public IEnumerable<string> Platforms
        {
            get { return _platforms; }
            set
            {
                //if(!object.ReferenceEquals(_configurations, value))
                if (_platforms != value)
                {
                    _platforms = value;
                    this.RaisePropertyChanged(() => this.Platforms);
                }
            }
        }

        #endregion

        public ICommand ToggleBuild { get; private set; }

        public ICommand ToggleDeploy { get; private set; }

        #region Methods

        private void ToggleSelectedDeploys()
        {
            var selectedProjects = from p in Projects
                                   where p.IsSelected && p.IsDeployable
                                   select p;
            int selectedCount = selectedProjects.Count();
            if (selectedCount == 0)
            {
                return;
            }

            var uncheckedCollection = from p in selectedProjects
                                      where !p.ShouldDeploy
                                      select p;

            bool shouldDeploy = uncheckedCollection.Count() != 0;

            foreach (var project in selectedProjects)
            {
                project.ShouldDeploy = shouldDeploy;
            }
        }

        private void ToggleSelectedBuilds()
        {
            var selectedProjects = from p in Projects
                                   where p.IsSelected && p.IsBuildable
                                   select p;
            int selectedCount = selectedProjects.Count();
            if (selectedCount == 0)
            {
                return;
            }

            var uncheckedCollection = from p in selectedProjects
                                      where !p.ShouldBuild
                                      select p;

            bool shouldBuild = uncheckedCollection.Count() != 0;

            foreach (var project in selectedProjects)
            {
                project.ShouldBuild = shouldBuild;
            }
        }

        #endregion
    }
}
