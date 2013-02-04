using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using EnvDTE;

namespace VukosConfigurationManager
{
    public class ProjectView : INotifyPropertyChanged
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

        #region Project

        /// <summary>
        /// A backing store for the property <see cref="Project"/>
        /// </summary>
        private Project _project;

        public Project Project
        {
            get { return _project; }
            set
            {
                //if(!object.ReferenceEquals(_project, value))
                if (_project != value)
                {
                    _project = value;
                    this.RaisePropertyChanged(() => this.Project);
                }
            }
        }

        #endregion

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
                    this.RaisePropertyChanged(() => this.Solution);
                }
            }
        }

        #endregion

        #region Context

        /// <summary>
        /// A backing store for the property <see cref="Context"/>
        /// </summary>
        private SolutionContext _context;

        public SolutionContext Context
        {
            get { return _context; }
            set
            {
                //if(!object.ReferenceEquals(_context, value))
                if (_context != value)
                {
                    _context = value;
                    this.RaisePropertyChanged(() => this.Context);
                    RefreshProjectValue();
                }
            }
        }

        #endregion

        #region Name

        public string Name
        {
            get { return _project == null ? null : _project.Name; }
        }

        #endregion

        #region ShouldBuild

        public bool ShouldBuild
        {
            get { return _context == null ? false : _context.ShouldBuild; }
            set
            {
                if (_context != null)
                {
                    if (ShouldBuild != value)
                    {
                        _context.ShouldBuild = value;
                        this.RaisePropertyChanged(() => this.ShouldBuild);
                    }
                }
            }
        }

        #endregion

        #region ShouldDeploy

        public bool ShouldDeploy
        {
            get { return _context == null ? false : _context.ShouldDeploy; }
            set
            {
                if (_context != null)
                {
                    if (ShouldDeploy != value)
                    {
                        _context.ShouldDeploy = value;
                        this.RaisePropertyChanged(() => this.ShouldDeploy);
                    }
                }
            }
        }

        #endregion

        #region ConfigurationName

        public string ConfigurationName
        {
            get { return _context == null ? null : _context.ConfigurationName; }
            set
            {
                if (_context != null)
                {
                    if (ConfigurationName != value)
                    {
                        _context.ConfigurationName = value;
                        this.RaisePropertyChanged(() => this.ConfigurationName);
                    }
                }
            }
        }

        #endregion

        #region PlatformName

        public string PlatformName
        {
            get { return _context == null ? null : _context.PlatformName; }
        }

        #endregion

        private void RefreshProjectValue()
        {
            if (_context != null && _solution != null)
            {
                // Project.Kind == "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"
                Project = FindProject(_context.ProjectName, _solution.Projects);
            }
            else
            {
                Project = null;
            }
        }

        private EnvDTE.Project FindProject(string projectName, Projects projects)
        {
            try
            {
                return projects.Item(projectName);
            }
            catch
            {
                return FindProjectIterrative(projectName, projects);
            }

        }

        private EnvDTE.Project FindProjectIterrative(string projectName, Projects projects)
        {
            var enumerator = projects.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Project p = (Project)enumerator.Current;
                if (p.Kind == "{66A26722-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    if (p.UniqueName == projectName)
                    {
                        return p;
                    }
                }
                else if (p.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    Project found = FindProject(projectName, p.ProjectItems);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        private EnvDTE.Project FindProject(string projectName, ProjectItems projectItems)
        {
            if (projectItems == null)
            {
                return null;
            }
            var enumerator = projectItems.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ProjectItem pi = (ProjectItem)enumerator.Current;
                if (pi.SubProject != null)
                {
                    if (pi.Kind == "{66A26722-8FB5-11D2-AA7E-00C04F688DDE}")
                    {
                        if (pi.SubProject.UniqueName == projectName)
                        {
                            return pi.SubProject;
                        }
                    }
                    if (pi.SubProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                    {
                        Project found = FindProject(projectName, pi.SubProject.ProjectItems);
                        if (found != null)
                        {
                            return found;
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
