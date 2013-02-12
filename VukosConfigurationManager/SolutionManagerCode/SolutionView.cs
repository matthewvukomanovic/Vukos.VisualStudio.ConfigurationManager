using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using EnvDTE;
using VukosConfigurationManager;
using Vukos.Common;
using System.Windows.Input;
using System.Threading.Tasks;

namespace VukosConfigurationManager
{
    public class SolutionView : SolutionViewBase
    {

        public SolutionView() : base()
        {
            _setActiveConfiguration = new ToggleDelegateCommand(SetActiveConfigurationMethod);
        }

        #region Properties

        public override System.Windows.Input.ICommand SetActiveConfiguration
        {
            get
            {
                return _setActiveConfiguration;
            }
        }

        private readonly ICommand _setActiveConfiguration;

        #region Solution

        /// <summary>
        /// A backing store for the property <see cref="Solution"/>
        /// </summary>
        private Solution _solution;

        public Solution Solution
        {
            get { return _solution; }
            set
            {
                //if(!object.ReferenceEquals(_solution, value))
                if (_solution != value)
                {
                    _solution = value;
                    RefreshAllSolutionValues(true);
                    this.RaisePropertyChanged(() => this.Solution);
                }
            }
        }

        #endregion

        #region SelectedConfiguration

        /// <summary>
        /// A backing store for the property <see cref="SelectedConfiguration"/>
        /// </summary>
        private string _selectedConfiguration;

        public override string SelectedConfiguration
        {
            get { return _selectedConfiguration; }
            set
            {
                //if(!object.ReferenceEquals(_activeConfiguration, value))
                if (_selectedConfiguration != value)
                {
                    _selectedConfiguration = value;
                    this.RaisePropertyChanged(() => this.SelectedConfiguration);
                    RefreshAllSolutionValues(false);
                }
            }
        }

        #endregion

        #region ActivePlatform

        /// <summary>
        /// A backing store for the property <see cref="ActivePlatform"/>
        /// </summary>
        private string _activePlatform;

        public override string ActivePlatform
        {
            get { return _activePlatform; }
            set
            {
                if (_activePlatform != value)
                {
                    _activePlatform = value;
                    this.RaisePropertyChanged(() => this.ActivePlatform);
                    SetActivePlatform();
                }
            }
        }

        #endregion

        private void SetActivePlatform()
        {
            if (_solution == null || _solution.SolutionBuild == null || _selectedConfiguration == null)
            {
                return;
            }
            else
            {
                // Determine how to change the currently active platform
            }
        }

        private void SetActiveConfigurationMethod()
        {
            if (_solution == null || _solution.SolutionBuild == null || _selectedConfiguration == null)
            {
                return;
            }
            else
            {
                // Determine how to change the currently active configuration
                var configuration = _solution.SolutionBuild.SolutionConfigurations.Item(_selectedConfiguration);
                if (configuration != null)
                {
                    Task.Factory.StartNew(configuration.Activate);
                }
            }
        }

        #endregion

        Suppression refreshSuppression = new Suppression();

        private void RefreshAllSolutionValues(bool refreshAll)
        {
            refreshSuppression.Run(() =>
            {
                if (_solution == null || _solution.SolutionBuild == null || _solution.SolutionBuild.ActiveConfiguration == null)
                {
                    Projects = null;
                    SelectedConfiguration = null;
                    Configurations = null;
                    return;
                }
                else
                {
                    List<string> configurations = new List<string>();
                    List<string> platforms = new List<string>();
                    foreach (SolutionConfiguration config in _solution.SolutionBuild.SolutionConfigurations)
                    {
                        configurations.Add(config.Name);
                        foreach (SolutionContext context in config.SolutionContexts)
                        {
                            platforms.Add(context.PlatformName);
                        }
                    }

                    Configurations = configurations.Distinct();
                    if (refreshAll)
                    {
                        _selectedConfiguration = _solution.SolutionBuild.ActiveConfiguration.Name;
                    }

                    string active_config = (string)_solution.Properties.Item("ActiveConfig").Value;

                    bool platformSet = false;
                    if (!string.IsNullOrEmpty(active_config))
                    {
                        var splitConfig = active_config.Split('|');
                        if (splitConfig.Length == 2)
                        {
                            _activePlatform = splitConfig[1];
                            platforms.Add(_activePlatform);
                            platformSet = true;
                        }
                    }
                    if (!platformSet)
                    {
                        _activePlatform = null;
                    }

                    Platforms = platforms.Distinct();

                    SolutionConfiguration configuration = _solution.SolutionBuild.SolutionConfigurations.Item(_selectedConfiguration);
                    if (configuration == null || configuration.SolutionContexts == null)
                    {
                        Projects = null;
                        return;
                    }

                    List<IProjectView> projects = new List<IProjectView>(configuration.SolutionContexts.Count);
                    foreach (SolutionContext solContext in configuration.SolutionContexts)
                    {
                        var projectView = new ProjectView() { Solution = _solution, Context = solContext };
                        projectView.IsSelected = false;
                        projects.Add(projectView);
                    }
                    projects.Sort((x, y) => string.Compare(x.Name, y.Name));
                    Projects = projects;
                }
            });
        }
    }
}
