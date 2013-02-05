using System;
using System.ComponentModel;
namespace VukosConfigurationManager
{
    public interface IProjectView : INotifyPropertyChanged
    {
        string ConfigurationName { get; set; }
        string Name { get; }
        string PlatformName { get; }
        event PropertyChangedEventHandler PropertyChanged;
        bool ShouldBuild { get; set; }
        bool ShouldDeploy { get; set; }
    }
}
