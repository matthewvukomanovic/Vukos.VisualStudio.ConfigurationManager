using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
namespace Vukos.VisualStudio.ConfigurationManager
{
    public interface ISolutionViewModel : INotifyPropertyChanged
    {
        ICommand SetActiveConfiguration { get; }
        string SelectedConfiguration { get; set; }
        string ActivePlatform { get; set; }
        IEnumerable<string> Configurations { get; set; }
        IEnumerable<string> Platforms { get; set; }
        IList<IProjectViewModel> Projects { get; set; }
    }
}
