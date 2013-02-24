using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vukos.VisualStudio.ConfigurationManager;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Vukos.VisualStudio.ConfigurationManager
{
    public sealed class SolutionViewDummy : SolutionViewBase
    {
        #region ActiveConfiguration

        /// <summary>
        /// A backing store for the property <see cref="SelectedConfiguration"/>
        /// </summary>
        private string _activeConfiguration;

        public override string SelectedConfiguration
        {
            get { return _activeConfiguration; }
            set
            {
                //if(!object.ReferenceEquals(_activeConfiguration, value))
                if (_activeConfiguration != value)
                {
                    _activeConfiguration = value;
                    this.RaisePropertyChanged(() => this.SelectedConfiguration);
                }
            }
        }

        #endregion

        public override ICommand SetActiveConfiguration
        {
            get { return null; }
        }

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
