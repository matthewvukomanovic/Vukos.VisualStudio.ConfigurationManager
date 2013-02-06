using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VukosConfigurationManager;

namespace TestApplication
{
    public sealed class ProjectView : IProjectView
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

        #region ConfigurationName

        /// <summary>
        /// A backing store for the property <see cref="ConfigurationName"/>
        /// </summary>
        private string _configurationName;

        public string ConfigurationName
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

        public string Name
        {
            get { return _name; }
            set
            {
                //if(!object.ReferenceEquals(_name, value))
                if (_name != value)
                {
                    _name = value;
                    this.RaisePropertyChanged(() => this.Name);
                }
            }
        }

        #endregion

        #region PlatformName

        /// <summary>
        /// A backing store for the property <see cref="PlatformName"/>
        /// </summary>
        private string _platformName;

        public string PlatformName
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

        public bool ShouldBuild
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

        public bool ShouldDeploy
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

    }
}
