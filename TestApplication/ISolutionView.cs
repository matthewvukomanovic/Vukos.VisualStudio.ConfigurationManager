using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace VukosConfigurationManager
{
    public interface ISolutionView : INotifyPropertyChanged
    {
        string ActiveConfiguration { get; set; }
        string ActivePlatform { get; set; }
        IEnumerable<string> Configurations { get; set; }
        IEnumerable<string> Platforms { get; set; }
        IList<IProjectView> Projects { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
