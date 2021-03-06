﻿using System;
using System.Diagnostics;
using DamnTools.SqlTodoExplorer.Presentation;
using DamnTools.SqlTodoExplorer.Services;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using Microsoft.SqlServer.Management.UI.VSIntegration;
using Microsoft.VisualStudio.CommandBars;

namespace DamnTools.SqlTodoExplorer
{
    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        private AddIn _addInInstance;
        private DTE2 _applicationObject;

        /// <summary>
        ///     Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in
        ///     is being loaded.
        /// </summary>
        /// <param term='application'>Root object of the host application.</param>
        /// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;
            if (connectMode == ext_ConnectMode.ext_cm_Startup)
            {
                object[] contextGUIDS = { };
                var commands = (Commands2)_applicationObject.Commands;
                string toolsMenuName = "Tools";

                //Place the command on the tools menu.
                //Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
                CommandBar menuBarCommandBar = ((CommandBars)_applicationObject.CommandBars)["MenuBar"];

                //Find the Tools command bar on the MenuBar command bar:
                CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                var toolsPopup = (CommandBarPopup)toolsControl;

                //This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
                //  just make sure you also update the QueryStatus/Exec method to include the new command names.
                try
                {
                    //Add a command to the Commands collection:
                    Command command = commands.AddNamedCommand2(_addInInstance, "OpenTodoExplorerGui", "SQL Todo Explorer", "Opens the to-do explorer", false, 1, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                    //Add a control for the command to the tools menu:
                    if ((command != null) && (toolsPopup != null))
                    {
                        command.AddControl(toolsPopup.CommandBar, 1);
                    }
                }
                catch (ArgumentException)
                {
                    //If we are here, then the exception is probably because a command with that name
                    //  already exists. If so there is no need to recreate the command and we can 
                    //  safely ignore the exception.
                }
            }
        }

        /// <summary>
        ///     Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the
        ///     Add-in is being unloaded.
        /// </summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
            var commands = (Commands2)_applicationObject.Commands;
            try
            {
                Command addinCommand = commands.Item("DamnTools.SqlTodoExplorer.Connect.OpenTodoExplorerGui");
                addinCommand.Delete();
            }
            catch (ArgumentException e)
            {
                Debug.Print("Error deleting commands on disconnection: {0}", e);
            }
        }

        /// <summary>
        ///     Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's
        ///     availability is updated
        /// </summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "DamnTools.SqlTodoExplorer.Connect.OpenTodoExplorerGui")
                {
                    status = vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                }
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
                if (commandName == "DamnTools.SqlTodoExplorer.Connect.OpenTodoExplorerGui")
                {
                    var dte = ServiceCache.ExtensibilityModel;
                    var windowsManager = new WindowsManager(dte);
                    var title = SqlTodoExplorer.Properties.Resources.ViewTitle;
                    windowsManager.ShowWindow<SqlTodoExplorerView>(_addInInstance, title, width: 728, height: 300);

                    handled = true;
                }
            }
        }

        /// <summary>
        ///     Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the
        ///     collection of Add-ins has changed.
        /// </summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>
        ///     Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the
        ///     host application has completed loading.
        /// </summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>
        ///     Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host
        ///     application is being unloaded.
        /// </summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }
    }
}