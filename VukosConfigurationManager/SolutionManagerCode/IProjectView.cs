using System;
using System.ComponentModel;
namespace VukosConfigurationManager
{
    public interface IProjectView : INotifyPropertyChanged
    {
        string ConfigurationName { get; set; }
        string Name { get; }
        string PlatformName { get; }
        bool ShouldBuild { get; set; }
        bool ShouldDeploy { get; set; }
        bool IsBuildable { get; }
        bool IsDeployable { get; }
        bool IsSelected { get; set; }
    }
}
