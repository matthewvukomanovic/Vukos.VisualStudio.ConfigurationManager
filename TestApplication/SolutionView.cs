using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VukosConfigurationManager;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace VukosConfigurationManager
{
    public sealed class SolutionView : SolutionViewBase
    {
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
                //if(!object.ReferenceEquals(_activePlatform, value))
                if (_activePlatform != value)
                {
                    _activePlatform = value;
                    this.RaisePropertyChanged(() => this.ActivePlatform);
                }
            }
        }

        #endregion
    }
}
