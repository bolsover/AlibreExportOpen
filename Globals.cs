using System.Drawing;
using Microsoft.Win32;

namespace AlibreExportOpen
{
    public class Globals
    {
        public static string InstallPath = (string) Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Alibre Design Add-Ons\",
            "{1177de0e-5965-11ed-9b6a-0242ac120002}", null);

        public static Icon Icon = new Icon(InstallPath + "\\3DPrint.ico");

      
    }
}