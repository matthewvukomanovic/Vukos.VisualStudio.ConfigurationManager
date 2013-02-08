using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using EnvDTE;
using VukosConfigurationManager;
using Vukos.Common;

namespace VukosConfigurationManager
{
    public class SolutionView : SolutionViewBase
    {
        #region Properties

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
                    RefreshAllSolutionValues();
                    this.RaisePropertyChanged(() => this.Solution);
                }
            }
        }

        #endregion

        #region ActiveConfiguration

        /// <summary>
        /// A backing store for the property <see cref="ActiveConfiguration"/>
        /// </summary>
        private string _activeConfiguration;

        public override string ActiveConfiguration
        {
            get { return _activeConfiguration; }
            set
            {
                //if(!object.ReferenceEquals(_activeConfiguration, value))
                if (_activeConfiguration != value)
                {
                    _activeConfiguration = value;
                    this.RaisePropertyChanged(() => this.ActiveConfiguration);
                    SetActiveConfiguration();
                    RefreshAllSolutionValues();
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
            if (_solution == null || _solution.SolutionBuild == null || _activeConfiguration == null)
            {
                return;
            }
            else
            {
                // Determine how to change the currently active platform
            }
        }

        private void SetActiveConfiguration()
        {
            if (_solution == null || _solution.SolutionBuild == null || _activeConfiguration == null)
            {
                return;
            }
            else
            {
                // Determine how to change the currently active configuration
                var configuration = _solution.SolutionBuild.SolutionConfigurations.Item(_activeConfiguration);
                if (configuration != null)
                {
                    configuration.Activate();
                }
            }
        }

        #endregion

        Suppression refreshSuppression = new Suppression();

        private void RefreshAllSolutionValues()
        {
            refreshSuppression.Run(() =>
            {
                if (_solution == null || _solution.SolutionBuild == null || _solution.SolutionBuild.ActiveConfiguration == null)
                {
                    Projects = null;
                    ActiveConfiguration = null;
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
                    _activeConfiguration = _solution.SolutionBuild.ActiveConfiguration.Name;

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

                    List<IProjectView> projects = new List<IProjectView>(_solution.SolutionBuild.ActiveConfiguration.SolutionContexts.Count);
                    foreach (SolutionContext solContext in _solution.SolutionBuild.ActiveConfiguration.SolutionContexts)
                    {
                        var projectView = new ProjectView() { Solution = _solution, Context = solContext };
                        projects.Add(projectView);
                    }
                    projects.Sort((x, y) => string.Compare(x.Name, y.Name));
                    Projects = projects;
                    
                }
            });
        }
    }
}
