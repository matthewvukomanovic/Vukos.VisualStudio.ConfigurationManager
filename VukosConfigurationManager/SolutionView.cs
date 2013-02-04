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
    public class SolutionView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(((MemberExpression)propertyExpression.Body).Member.Name));
            }
        }

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

        #region Projects

        /// <summary>
        /// A backing store for the property <see cref="Projects"/>
        /// </summary>
        private IList<ProjectView> _projects;

        public IList<ProjectView> Projects
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

        #region ActiveConfiguration

        /// <summary>
        /// A backing store for the property <see cref="ActiveConfiguration"/>
        /// </summary>
        private string _activeConfiguration;

        public string ActiveConfiguration
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

        #region ActivePlatform

        /// <summary>
        /// A backing store for the property <see cref="ActivePlatform"/>
        /// </summary>
        private string _activePlatform;

        public string ActivePlatform
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
                    Platforms = platforms.Distinct();

                    _activeConfiguration = _solution.SolutionBuild.ActiveConfiguration.Name;

                    string active_config = (string)_solution.Properties.Item("ActiveConfig").Value;

                    bool platformSet = false;
                    if (!string.IsNullOrEmpty(active_config))
                    {
                        var splitConfig = active_config.Split('|');
                        if (splitConfig.Length == 2)
                        {
                            _activePlatform = splitConfig[1];
                            platformSet = true;
                        }
                    }
                    if (!platformSet)
                    {
                        _activePlatform = null;
                    }

                    foreach (Property property in _solution.Properties)
                    {
                    }

                    List<ProjectView> projects = new List<ProjectView>(_solution.SolutionBuild.ActiveConfiguration.SolutionContexts.Count);
                    foreach (SolutionContext solContext in _solution.SolutionBuild.ActiveConfiguration.SolutionContexts)
                    {
                        var projectView = new ProjectView() { Solution = _solution, Context = solContext };
                        projects.Add(projectView);
                    }
                    Projects = projects;
                }
            });
        }
    }
}
