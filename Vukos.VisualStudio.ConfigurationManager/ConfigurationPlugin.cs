﻿using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace Vukos.VisualStudio.ConfigurationManager
{
    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    public class ConfigurationPlugin : IDTExtensibility2, IDTCommandTarget
    {
        /// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
        public ConfigurationPlugin()
        {
        }

        SolutionEvents _solutionEvents;

        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
        /// <param term='application'>Root object of the host application.</param>
        /// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;

            // TODO: This section here should be made as an option since not everyone might want the start page displaying again when the solution is closed.
            _solutionEvents = _applicationObject.Events.SolutionEvents;
            _solutionEvents.AfterClosing += () => _applicationObject.ExecuteCommand("View.StartPage");

            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                var contextGuids = new object[] { };
                var commands = (Commands2)_applicationObject.Commands;
                const string toolsMenuName = "Tools";

                //Place the command on the tools menu.
                //Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
                var menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

                //Find the Tools command bar on the MenuBar command bar:
                var toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                var toolsPopup = (CommandBarPopup)toolsControl;

                Action<string, string, string> addCommand = (name, buttonText, toolTip) =>
                {
                    //This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
                    //  just make sure you also update the QueryStatus/Exec method to include the new command names.
                    try
                    {
                        //Add a command to the Commands collection:
                        var command = commands.AddNamedCommand2(_addInInstance, name, buttonText, toolTip, false, 1, ref contextGuids, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                        //Add a control for the command to the tools menu:
                        if ((command != null) && (toolsPopup != null))
                        {
                            command.AddControl(toolsPopup.CommandBar, 1);
                        }
                    }
                    catch (System.ArgumentException)
                    {
                        //If we are here, then the exception is probably because a command with that name
                        //  already exists. If so there is no need to recreate the command and we can 
                        //  safely ignore the exception.
                    }
                };

                addCommand(ConstantSelectBuild, "Select Build Configurations", "Window for configurations");
                addCommand(ConstantAllBuild, "All Build", "Adds all projects to be built");
                addCommand(ConstantNoneBuild, "No Build", "Removes all projects from being built");
#if DEBUG
                //addCommand(Constant_ShowDummy, "Show Dummy Build Configurations", "Window for configurations");
#endif
            }
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                switch (commandName)
                {
                    case ConstantLocalisedNameAllBuild:
                    case ConstantLocalisedNameNoneBuild:
                    case ConstantLocalisedNameSelectBuild:
                        Solution solution = _applicationObject.Solution;
                        if (solution == null || solution.SolutionBuild == null || solution.SolutionBuild.ActiveConfiguration == null)
                        {
                            break;
                        }
                        status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                        return;
#if DEBUG
                    case ConstantLocalisedNameShowDummy:
                        status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                        return;
#endif

                }
                status = (vsCommandStatus)vsCommandStatus.vsCommandStatusInvisible | vsCommandStatus.vsCommandStatusUnsupported;
                return;
            }
        }

        /// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
        /// <param term='commandName'>The name of the command to execute.</param>
        /// <param term='executeOption'>Describes how the command should be run.</param>
        /// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
        /// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
        /// <param term='handled'>Informs the caller if the command was handled or not.</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                switch (commandName)
                {
                    case ConstantLocalisedNameAllBuild:
                        SetConfigurationValues(true);
                        handled = true;
                        return;
                    case ConstantLocalisedNameNoneBuild:
                        SetConfigurationValues(false);
                        handled = true;
                        return;
                    case ConstantLocalisedNameSelectBuild:
                        ShowConfigurationWindow();
                        handled = true;
                        return;
#if DEBUG
                    case ConstantLocalisedNameShowDummy:
                        ShowConfigurationWindowDummy();
                        handled = true;
                        return;
#endif
                }
            }
        }

        private void ShowConfigurationWindow()
        {
            Solution solution = _applicationObject.Solution;
            if (solution == null || solution.SolutionBuild == null || solution.SolutionBuild.ActiveConfiguration == null)
            {
                return;
            }

            var solutionView = new SolutionViewModel() { Solution = solution };

            ConfigurationWindow.Show(solutionView);
        }

        private void ShowConfigurationWindowDummy()
        {
            Vukos.VisualStudio.ConfigurationManager.Dummy.CreateDummy.ShowDummy();
        }

        private void SetConfigurationValues(bool buildProjects)
        {
            Solution solution = _applicationObject.Solution;
            if (solution == null || solution.SolutionBuild == null || solution.SolutionBuild.ActiveConfiguration == null)
            {
                return;
            }
            var activeConfiguration = solution.SolutionBuild.ActiveConfiguration;
            var solutionContexts = activeConfiguration.SolutionContexts;
            foreach (SolutionContext solutionContext in solutionContexts)
            {
                solutionContext.ShouldBuild = buildProjects;
            }
        }

        private const string ConstantClassname = "Vukos.VisualStudio.ConfigurationManager.ConfigurationPlugin";

        private const string ConstantLocalisedNameAllBuild = ConstantClassname + "." + ConstantAllBuild;
        private const string ConstantLocalisedNameNoneBuild = ConstantClassname + "." + ConstantNoneBuild;
        private const string ConstantLocalisedNameSelectBuild = ConstantClassname + "." + ConstantSelectBuild;
#if DEBUG
        private const string ConstantLocalisedNameShowDummy = ConstantClassname + "." + ConstantShowDummy;
#endif

        private const string ConstantAllBuild = "AllBuild";
        private const string ConstantNoneBuild = "NoneBuild";
        private const string ConstantSelectBuild = "SelectBuild";
#if DEBUG
        private const string ConstantShowDummy = "ShowDummy";
#endif

        private DTE2 _applicationObject;
        private AddIn _addInInstance;
    }
}