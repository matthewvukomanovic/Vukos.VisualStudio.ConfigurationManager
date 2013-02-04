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
                Project = _solution.Projects.Item(_context.ProjectName);
            }
            else
            {
                Project = null;
            }
        }

        #endregion
    }
}
