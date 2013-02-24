using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using EnvDTE;

namespace Vukos.VisualStudio.ConfigurationManager
{
    public class ProjectView : ProjectViewBase
    {
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

        public override string Name
        {
            get { return _project == null ? null : _project.Name; }
        }

        #endregion

        #region ShouldBuild

        public override bool ShouldBuild
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

        public override bool ShouldDeploy
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

        #region IsDeployable

        public override bool IsDeployable
        {
            get {
                bool ret = _project == null ? false : _project.ConfigurationManager.ActiveConfiguration.IsDeployable;
                return ret;
            }
        }

        #endregion

        #region IsBuildable

        public override bool IsBuildable
        {
            get { return _project == null ? false : _project.ConfigurationManager.ActiveConfiguration.IsBuildable; }
        }

        #endregion


        #region ConfigurationName

        public override string ConfigurationName
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

        public override string PlatformName
        {
            get { return _context == null ? null : _context.PlatformName; }
        }

        #endregion

        private void RefreshProjectValue()
        {
            if (_context != null && _solution != null)
            {
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

        private Project FindProjectIterrative(string projectName, Projects projects)
        {
            var enumerator = projects.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Project p = (Project)enumerator.Current;
                Project found = FindProject(projectName, p);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }

        private Project FindProject(string projectName, Project project)
        {
            if (project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
            {
                return FindProject(projectName, project.ProjectItems);
            }
            else
            {
                if (project.UniqueName == projectName)
                {
                    return project;
                }
            }

            return null;
        }

        private Project FindProject(string projectName, ProjectItems projectItems)
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
                    Project found = FindProject(projectName, pi.SubProject);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
