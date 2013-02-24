using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
namespace Vukos.VisualStudio.ConfigurationManager
{
    public interface ISolutionView : INotifyPropertyChanged
    {
        ICommand SetActiveConfiguration { get; }
        string SelectedConfiguration { get; set; }
        string ActivePlatform { get; set; }
        IEnumerable<string> Configurations { get; set; }
        IEnumerable<string> Platforms { get; set; }
        IList<IProjectView> Projects { get; set; }
    }
}
