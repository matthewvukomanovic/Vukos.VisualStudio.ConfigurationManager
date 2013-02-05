using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VukosConfigurationManager;
using System.ComponentModel;
using System.Linq.Expressions;

namespace TestApplication
{
    public class SolutionView : ISolutionView
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
                //if(!object.ReferenceEquals(_activePlatform, value))
                if (_activePlatform != value)
                {
                    _activePlatform = value;
                    this.RaisePropertyChanged(() => this.ActivePlatform);
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
                //if(!object.ReferenceEquals(_platforms, value))
                if (_platforms != value)
                {
                    _platforms = value;
                    this.RaisePropertyChanged(() => this.Platforms);
                }
            }
        }

        #endregion


        //public IList<IProjectView> Projects
    }
}
