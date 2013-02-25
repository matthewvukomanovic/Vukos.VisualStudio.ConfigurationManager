using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Vukos.VisualStudio.ConfigurationManager
{
    public abstract class ProjectViewModelBase : ViewModelBase, IProjectViewModel
    {
        #region IsSelected

        /// <summary>
        /// A backing store for the property <see cref="IsSelected"/>
        /// </summary>
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                //if(!object.ReferenceEquals(_isSelected, value))
                if (_isSelected != value)
                {
                    _isSelected = value;
                    this.RaisePropertyChanged(() => this.IsSelected);
                }
            }
        }

        #endregion

        public abstract string ConfigurationName { get; set; }
        public abstract string Name { get; }
        public abstract string PlatformName { get; }
        public abstract bool ShouldBuild { get; set; }
        public abstract bool ShouldDeploy { get; set; }
        public abstract bool IsBuildable { get; }
        public abstract bool IsDeployable { get; }
    }
}
