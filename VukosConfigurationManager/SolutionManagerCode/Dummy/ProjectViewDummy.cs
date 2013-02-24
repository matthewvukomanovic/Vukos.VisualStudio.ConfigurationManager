using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vukos.VisualStudio.ConfigurationManager;

namespace Vukos.VisualStudio.ConfigurationManager
{
    public sealed class ProjectViewDummy : ProjectViewBase
    {
        #region ConfigurationName

        /// <summary>
        /// A backing store for the property <see cref="ConfigurationName"/>
        /// </summary>
        private string _configurationName;

        public override string ConfigurationName
        {
            get { return _configurationName; }
            set
            {
                //if(!object.ReferenceEquals(_configurationName, value))
                if (_configurationName != value)
                {
                    _configurationName = value;
                    this.RaisePropertyChanged(() => this.ConfigurationName);
                }
            }
        }

        #endregion

        #region Name

        /// <summary>
        /// A backing store for the property <see cref="Name"/>
        /// </summary>
        private string _name;

        public string NameSet
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.RaisePropertyChanged(() => this.Name);
                }
            }
        }

        public override string Name
        {
            get { return _name; }
        }

        #endregion

        #region PlatformName

        /// <summary>
        /// A backing store for the property <see cref="PlatformName"/>
        /// </summary>
        private string _platformName;

        public override string PlatformName
        {
            get { return _platformName; }
        }

        public string PlatformNameSet
        {
            get { return _platformName; }
            set
            {
                //if(!object.ReferenceEquals(_platformName, value))
                if (_platformName != value)
                {
                    _platformName = value;
                    this.RaisePropertyChanged(() => this.PlatformName);
                }
            }
        }

        #endregion

        #region ShouldBuild

        /// <summary>
        /// A backing store for the property <see cref="ShouldBuild"/>
        /// </summary>
        private bool _shouldBuild;

        public override bool ShouldBuild
        {
            get { return _shouldBuild; }
            set
            {
                //if(!object.ReferenceEquals(_shouldBuild, value))
                if (_shouldBuild != value)
                {
                    _shouldBuild = value;
                    this.RaisePropertyChanged(() => this.ShouldBuild);
                }
            }
        }

        #endregion

        #region ShouldDeploy

        /// <summary>
        /// A backing store for the property <see cref="ShouldDeploy"/>
        /// </summary>
        private bool _shouldDeploy;

        public override bool ShouldDeploy
        {
            get { return _shouldDeploy; }
            set
            {
                //if(!object.ReferenceEquals(_shouldDeploy, value))
                if (_shouldDeploy != value)
                {
                    _shouldDeploy = value;
                    this.RaisePropertyChanged(() => this.ShouldDeploy);
                }
            }
        }

        #endregion

        #region IsBuildable

        /// <summary>
        /// A backing store for the property <see cref="IsBuildable"/>
        /// </summary>
        private bool _isBuildable;

        public override bool IsBuildable
        {
            get { return _isBuildable; }
        }

        public bool IsBuildableSet
        {
            get { return _isBuildable; }
            set
            {
                //if(!object.ReferenceEquals(_isBuildable, value))
                if (_isBuildable != value)
                {
                    _isBuildable = value;
                    this.RaisePropertyChanged(() => this.IsBuildable);
                }
            }
        }

        #endregion

        #region IsDeployable

        /// <summary>
        /// A backing store for the property <see cref="IsDeployable"/>
        /// </summary>
        private bool _isDeployable;

        public override bool IsDeployable
        {
            get { return _isDeployable; }

        }

        public bool IsDeployableSet
        {
            get { return _isDeployable; }
            set
            {
                //if(!object.ReferenceEquals(_isDeployable, value))
                if (_isDeployable != value)
                {
                    _isDeployable = value;
                    this.RaisePropertyChanged(() => this.IsDeployable);
                }
            }
        }

        #endregion
    }
}
