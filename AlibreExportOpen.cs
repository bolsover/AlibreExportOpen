﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AlibreAddOn;
using AlibreExportOpen.Sample;
using AlibreX;

namespace AlibreExportOpen
{
    public class AlibreExportOpen : IAlibreAddOn
    {
        private const int MenuIdRoot = 401;
        private const int MenuIdSample = 402;

        private readonly int[] _menuIdsBase = new int[1]
        {
            MenuIdSample
        };

        private IADRoot _alibreRoot;
        private IntPtr _parentWinHandle;

        public AlibreExportOpen(IADRoot alibreRoot, IntPtr parentWinHandle)
        {
            this._alibreRoot = alibreRoot;
            this._parentWinHandle = parentWinHandle;
            
        }

        #region Menus

        /// <summary>
        /// Returns the menu ID of the add-on's root menu item
        /// </summary>
        public int RootMenuItem => MenuIdRoot;


        /// <summary>
        /// Description("Returns Whether the given Menu ID has any sub menus")
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool HasSubMenus(int menuId)
        {
            //   return false;
            return menuId == MenuIdRoot;
        }

        /// <summary>
        /// Returns the ID's of sub menu items under a popup menu item; the menu ID of a 'leaf' menu becomes its command ID
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Array SubMenuItems(int menuId)
        {
            return _menuIdsBase;
        }

        /// <summary>
        /// Returns the display name of a menu item; a menu item with text of a single dash (“-“) is a separator
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string MenuItemText(int menuId)
        {
            return "Export, Open .stl";
        }

        /// <summary>
        /// Returns True if input menu item has sub menus // seems odd given name of method
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool PopupMenu(int menuId)
        {
            return true;
        }

        /// <summary>
        /// Returns property bits providing information about the state of a menu item
        /// ADDON_MENU_ENABLED = 1,
        /// ADDON_MENU_GRAYED = 2,
        /// ADDON_MENU_CHECKED = 3,
        /// ADDON_MENU_UNCHECKED = 4,
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public ADDONMenuStates MenuItemState(int menuId, string sessionIdentifier)
        {
            return ADDONMenuStates.ADDON_MENU_ENABLED;
        }

        /// <summary>
        /// Returns a tool tip string if input menu ID is that of a 'leaf' menu item
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string MenuItemToolTip(int menuId)
        {
            return "Export,Open .stl";
        }

        /// <summary>
        /// Returns the icon name (with extension) for a menu item; the icon will be searched under the folder where the add-on's .adc file is present
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string MenuIcon(int menuId)
        {
            return "3DPrint.ico";
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
        /// <param name="menuId"></param>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public IAlibreAddOnCommand InvokeCommand(int menuId, string sessionIdentifier)
        {
            var session = _alibreRoot.Sessions.Item(sessionIdentifier);
            return DoExport(session);
            //  return DoSample(session);
        }

        #endregion
        
        #region Export

        private IAlibreAddOnCommand DoExport(IADSession currentSession)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Standard Tessellation Language|*.stl";
            saveFileDialog1.Title = "Save a 3D File";
            saveFileDialog1.FileName = currentSession.Name + ".stl";
            DialogResult dr=  saveFileDialog1.ShowDialog();
            if (dr==DialogResult.OK)
            {
                ((IADDesignSession)currentSession).ExportSTL2(saveFileDialog1.FileName);
               
                    System.Diagnostics.Process.Start(Path.GetFullPath(saveFileDialog1.FileName));
            }
            
           
            return null; 
        }
        
        #endregion


        #region Sample

        /// <summary>
        /// A dictionary to keep track of currently open EmptyAddOnCommand object.
        /// </summary>
        private readonly Dictionary<string, SampleAddOnCommand> _emptyAddOnCommands = new();

        private IAlibreAddOnCommand DoSample(IADSession session)
        {
            SampleAddOnCommand sampleViewerAddOnCommand;
            if (!_emptyAddOnCommands.ContainsKey(session.Identifier))
            {
                sampleViewerAddOnCommand = new SampleAddOnCommand(session);
                sampleViewerAddOnCommand.SampleUserControl.Visible = true;
                sampleViewerAddOnCommand.Terminate += SampleAddOnCommandOnTerminate;
                _emptyAddOnCommands.Add(session.Identifier, sampleViewerAddOnCommand);
            }
            else
            {
                if (_emptyAddOnCommands.TryGetValue(session.Identifier, out sampleViewerAddOnCommand))
                {
                    sampleViewerAddOnCommand.UserRequestedClose();
                    _emptyAddOnCommands.Remove(session.Identifier);
                    return null;
                }
            }

            return sampleViewerAddOnCommand;
        }

        private void SampleAddOnCommandOnTerminate(object sender, SampleAddonCommandTerminateEventArgs e)
        {
            SampleAddOnCommand sampleAddOnCommand;
            if (_emptyAddOnCommands.TryGetValue(e.SampleAddOnCommand.Session.Identifier, out sampleAddOnCommand))
            {
                _emptyAddOnCommands.Remove(e.SampleAddOnCommand.Session.Identifier);
            }
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
            return true;
        }
    }
}