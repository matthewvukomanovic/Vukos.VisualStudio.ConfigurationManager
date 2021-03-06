﻿using System;
using System.ComponentModel;
namespace Vukos.VisualStudio.ConfigurationManager
{
    public interface IProjectViewModel : INotifyPropertyChanged
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
