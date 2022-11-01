using System;
using System.Collections.Generic;
using System.Diagnostics;
using AlibreAddOn;
using AlibreX;
using System.Windows.Forms;

using Bolsover.Sample;



namespace Bolsover
{
    public class UtilitiesForAlibre : IAlibreAddOn
    {
        private const int MENU_ID_ROOT = 401;
        private const int MENU_ID_FILE = 501;
        private const int SUBMENU_ID_FILE_OPEN = 502;
        private const int SUBMENU_ID_FILE_CLOSE = 503;
        private const int SUBMENU_ID_FILE_EXIT = 504;
        private const int SUBMENU_ID_DATA_BROWSER = 505;
        private const int MENU_ID_UTILS = 601;
        private const int SUBMENU_ID_UTILS_CYCLOIDAL_GEAR = 602;
        private const int SUBMENU_ID_UTILS_PLANE_FINDER = 603;
        private const int SUBMENU_ID_UTILS_DATA_VIEWER = 604;
        private const int SUBMENU_ID_UTILS_3DLINE = 605;
        private const int SUBMENU_ID_UTILS_INVOLUTE_GEAR = 607;
        private const int SUBMENU_ID_UTILS_GEARS = 608;
        private const int SUBMENU_ID_UTILS_SAMPLE = 606;
        private const int MENU_ID_HELP = 701;
        private const int SUBMENU_ID_HELP_ABOUT = 702;
        private int[] MENU_IDS_FILE;
        private int[] MENU_IDS_UTILS;
        private int[] MENU_IDS_ROOT;
        private int[] MENU_IDS_HELP;

        private IADRoot alibreRoot;
        private IntPtr parentWinHandle;


        public UtilitiesForAlibre(IADRoot alibreRoot, IntPtr parentWinHandle)
        {
            this.alibreRoot = alibreRoot;
            this.parentWinHandle = parentWinHandle;
            BuildMenuTree();
        }

        #region Menus

        /// <summary>
        /// Returns the menu ID of the add-on's root menu item
        /// </summary>
        public int RootMenuItem => MENU_ID_ROOT;

        /// <summary>
        /// Builds the menu tree
        /// </summary>
        private void BuildMenuTree()
        {
            MENU_IDS_FILE = new int[3]
            {
                SUBMENU_ID_FILE_OPEN, SUBMENU_ID_FILE_CLOSE, SUBMENU_ID_FILE_EXIT
            };
            MENU_IDS_UTILS = new int[7]
            {
                SUBMENU_ID_DATA_BROWSER,
                SUBMENU_ID_UTILS_GEARS,
                SUBMENU_ID_UTILS_CYCLOIDAL_GEAR,
                SUBMENU_ID_UTILS_PLANE_FINDER,
                SUBMENU_ID_UTILS_DATA_VIEWER,
                SUBMENU_ID_UTILS_3DLINE,
                SUBMENU_ID_UTILS_SAMPLE
            };
            MENU_IDS_ROOT = new int[3]
            {
                MENU_ID_FILE, MENU_ID_UTILS, MENU_ID_HELP
            };
            MENU_IDS_HELP = new int[1]
            {
                SUBMENU_ID_HELP_ABOUT
            };
        }

        /// <summary>
        /// Description("Returns Whether the given Menu ID has any sub menus")
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public bool HasSubMenus(int menuID)
        {
            switch (menuID)
            {
                case MENU_ID_ROOT: return true;
                case MENU_ID_FILE: return true;
                case MENU_ID_UTILS: return true;
                case MENU_ID_HELP: return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the ID's of sub menu items under a popup menu item; the menu ID of a 'leaf' menu becomes its command ID
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public Array SubMenuItems(int menuID)
        {
            switch (menuID)
            {
                case MENU_ID_ROOT: return MENU_IDS_ROOT;
                case MENU_ID_FILE: return MENU_IDS_FILE;
                case MENU_ID_UTILS: return MENU_IDS_UTILS;
                case MENU_ID_HELP: return MENU_IDS_HELP;
            }

            return null;
        }

        /// <summary>
        /// Returns the display name of a menu item; a menu item with text of a single dash (“-“) is a separator
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public string MenuItemText(int menuID)
        {
            switch (menuID)
            {
                case MENU_ID_ROOT: return "Utilities";
                case MENU_ID_FILE: return "File";
                case MENU_ID_UTILS: return "Utilities";
                case MENU_ID_HELP: return "Help";
                case SUBMENU_ID_DATA_BROWSER: return "Data Browser";
                case SUBMENU_ID_FILE_OPEN: return "Open";
                case SUBMENU_ID_FILE_CLOSE: return "Save & Close";
                case SUBMENU_ID_FILE_EXIT: return "Save All, Exit";
                case SUBMENU_ID_UTILS_CYCLOIDAL_GEAR: return "Cycloidal Gear Generator Open/Close";
                case SUBMENU_ID_UTILS_GEARS: return "Gear Generator";
                case SUBMENU_ID_HELP_ABOUT: return "About";
                case SUBMENU_ID_UTILS_PLANE_FINDER: return "Sketch Plane Finder Open/Close";
                case SUBMENU_ID_UTILS_DATA_VIEWER: return "Property Viewer Open/Close";
                case SUBMENU_ID_UTILS_3DLINE: return "3DLine Open/Close";
                case SUBMENU_ID_UTILS_SAMPLE: return "Sample Open/Close";
            }

            return "";
        }

        /// <summary>
        /// Returns True if input menu item has sub menus // seems odd given name of method
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public bool PopupMenu(int menuID)
        {
            return false;
        }

        /// <summary>
        /// Returns property bits providing information about the state of a menu item
        /// ADDON_MENU_ENABLED = 1,
        /// ADDON_MENU_GRAYED = 2,
        /// ADDON_MENU_CHECKED = 3,
        /// ADDON_MENU_UNCHECKED = 4,
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public ADDONMenuStates MenuItemState(int menuID, string sessionIdentifier)
        {
            var session = alibreRoot.Sessions.Item(sessionIdentifier);

            switch (session)
            {
                case IADDrawingSession:
                    switch (menuID)
                    {
                        case MENU_ID_ROOT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MENU_ID_FILE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MENU_ID_UTILS: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_DATA_BROWSER: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_OPEN: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_CLOSE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_EXIT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_CYCLOIDAL_GEAR: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_GEARS: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_PLANE_FINDER: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_DATA_VIEWER: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_3DLINE: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_SAMPLE: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_HELP_ABOUT: return ADDONMenuStates.ADDON_MENU_GRAYED;
                    }

                    break;

                case IADAssemblySession:
                    switch (menuID)
                    {
                        case MENU_ID_ROOT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MENU_ID_FILE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MENU_ID_UTILS: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_DATA_BROWSER: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_OPEN: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_CLOSE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_EXIT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_CYCLOIDAL_GEAR: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_PLANE_FINDER: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_GEARS: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_DATA_VIEWER: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_3DLINE: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SUBMENU_ID_UTILS_SAMPLE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_HELP_ABOUT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                    }

                    break;
                case IADPartSession:
                    switch (menuID)
                    {
                        case MENU_ID_ROOT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MENU_ID_FILE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MENU_ID_UTILS: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_DATA_BROWSER: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_OPEN: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_CLOSE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_FILE_EXIT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_CYCLOIDAL_GEAR: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_GEARS: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_PLANE_FINDER: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_DATA_VIEWER: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_3DLINE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_UTILS_SAMPLE: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SUBMENU_ID_HELP_ABOUT: return ADDONMenuStates.ADDON_MENU_ENABLED;
                    }

                    break;
            }

            return ADDONMenuStates.ADDON_MENU_ENABLED;
        }

        /// <summary>
        /// Returns a tool tip string if input menu ID is that of a 'leaf' menu item
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public string MenuItemToolTip(int menuID)
        {
            switch (menuID)
            {
                case MENU_ID_ROOT: return "Utilities";
                case MENU_ID_FILE: return "File";
                case MENU_ID_UTILS: return "Utilities";
                case SUBMENU_ID_DATA_BROWSER: return "Opens the custom Data Browser";
                case SUBMENU_ID_FILE_OPEN: return "Opens files from file explorer";
                case SUBMENU_ID_FILE_CLOSE: return "Saves and closes the current file";
                case SUBMENU_ID_FILE_EXIT: return "Saves all open files and quits Alibre";
                case SUBMENU_ID_UTILS_CYCLOIDAL_GEAR: return "Opens/Closes Cycloidal Gear Generator";
                case SUBMENU_ID_UTILS_GEARS: return "Opens Gear Generator Dialog";
                case SUBMENU_ID_UTILS_PLANE_FINDER: return "Finds the Plane on which a selected Sketch is drawn";
                case SUBMENU_ID_UTILS_DATA_VIEWER: return "Opens/Closes Property Viewer";
                case SUBMENU_ID_UTILS_3DLINE: return "Opens/Closes 3DLine Generator";
                case SUBMENU_ID_UTILS_SAMPLE: return "Opens/Closes Sample";
                case SUBMENU_ID_HELP_ABOUT: return "About Utilities for Alibre";
            }

            return "";
        }

        /// <summary>
        /// Returns the icon name (with extension) for a menu item; the icon will be searched under the folder where the add-on's .adc file is present
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public string MenuIcon(int menuID)
        {
           

            return string.Empty;
        }

        /// <summary>
        /// Returns True if AddOn has updated Persistent Data
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public bool HasPersistentDataToSave(string sessionIdentifier)
        {
            return false;
        }

        /// <summary>
        /// Invokes the add-on command identified by menu ID; returning the add-on command interface is optional
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public IAlibreAddOnCommand InvokeCommand(int menuID, string sessionIdentifier)
        {
            var session = alibreRoot.Sessions.Item(sessionIdentifier);

            switch (menuID)
            {
              
                case SUBMENU_ID_FILE_OPEN:
                {
                    return DoFileOpen();
                }
                case SUBMENU_ID_FILE_CLOSE:
                {
                    return DoFileClose(session);
                }
                case SUBMENU_ID_FILE_EXIT:
                {
                    return DoFileExit();
                }
              

                case SUBMENU_ID_UTILS_SAMPLE:
                {
                    return DoSample(session);
                }
             
            }

            return null;
        }

        #endregion

     
        #region Sample

        /// <summary>
        /// A dictionary to keep track of currently open EmptyAddOnCommand object.
        /// </summary>
        private readonly Dictionary<string, SampleAddOnCommand> sampleAddOnCommands = new();

        private IAlibreAddOnCommand DoSample(IADSession session)
        {
            SampleAddOnCommand sampleViewerAddOnCommand;
            if (!sampleAddOnCommands.ContainsKey(session.Identifier))
            {
                sampleViewerAddOnCommand = new SampleAddOnCommand(session);
                sampleViewerAddOnCommand.SampleUserControl.Visible = true;
                sampleViewerAddOnCommand.Terminate += SampleAddOnCommandOnTerminate;
                sampleAddOnCommands.Add(session.Identifier, sampleViewerAddOnCommand);
            }
            else
            {
                if (sampleAddOnCommands.TryGetValue(session.Identifier, out sampleViewerAddOnCommand))
                {
                    sampleViewerAddOnCommand.UserRequestedClose();
                    sampleAddOnCommands.Remove(session.Identifier);
                    return null;
                }
            }

            return sampleViewerAddOnCommand;
        }

        private void SampleAddOnCommandOnTerminate(object sender, SampleAddonCommandTerminateEventArgs e)
        {
            SampleAddOnCommand sampleAddOnCommand;
            if (sampleAddOnCommands.TryGetValue(e.SampleAddOnCommand.session.Identifier, out sampleAddOnCommand))
            {
                sampleAddOnCommands.Remove(e.SampleAddOnCommand.session.Identifier);
            }
        }

        #endregion

      

    

 

   

        #region FileOpen

        /// <summary>
        /// Opens a standard file dialog, opens the selected file or returns null if the user closes the dialog without selecting a file.
        /// </summary>
        /// <returns></returns>
        private static IAlibreAddOnCommand DoFileOpen()
        {
            var filePath = string.Empty;
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Alibre Files (*.AD_*)|*.AD_*|All files (*.*)|*.*";
            openFileDialog.Title = "Open Alibre File";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                OpenWithDefaultProgram(filePath);
                return (IAlibreAddOnCommand) null;
            }

            // fall through if no file selected
            return (IAlibreAddOnCommand) null;
        }

        /// <summary>
        /// Helper method to open files with default program
        /// </summary>
        /// <param name="path"></param>
        public static void OpenWithDefaultProgram(string path)
        {
            using var fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        #endregion

        #region FileClose

        /// <summary>
        /// Closes the current session, saving the file if required
        /// </summary>
        /// <param name="currentSession"></param>
        /// <returns></returns>
        private static IAlibreAddOnCommand DoFileClose(IADSession currentSession)
        {
            currentSession.Close(true);
            return null;
        }

        #endregion

        #region FileExit

        /// <summary>
        /// Saves and closes all open files, terminates the application
        /// </summary>
        /// <returns></returns>
        private IAlibreAddOnCommand DoFileExit()
        {
            foreach (IADSession session in alibreRoot.Sessions)
            {
                session.Close(true);
            }

            alibreRoot.Terminate();
            return null;
        }

        #endregion

        /// <summary>
        /// Loads Data from AddOn
        /// </summary>
        /// <param name="pCustomData"></param>
        /// <param name="sessionIdentifier"></param>
        public void LoadData(IStream pCustomData, string sessionIdentifier)
        {
        }

        /// <summary>
        /// Saves Data to AddOn
        /// </summary>
        /// <param name="pCustomData"></param>
        /// <param name="sessionIdentifier"></param>
        public void SaveData(IStream pCustomData, string sessionIdentifier)
        {
        }

        /// <summary>
        /// Sets the IsLicensed bit for the tightly coupled Add-on
        /// </summary>
        /// <param name="isLicensed"></param>
        public void setIsAddOnLicensed(bool isLicensed)
        {
        }

        /// <summary>
        /// Returns True if the AddOn needs to use a Dedicated Ribbon Tab
        /// </summary>
        /// <returns></returns>
        public bool UseDedicatedRibbonTab()
        {
            return false;
        }
    }
}