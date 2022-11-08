using System.Drawing;
using Microsoft.Win32;

namespace AlibreExportOpen
{
    public class Globals
    {
        public static string InstallPath = (string) Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Alibre Design Add-Ons\",
            "{378829C4-F122-4617-92E0-E36ADD4F9AA8}", null);

        public static Icon Icon = new Icon(InstallPath + "\\3DPrint.ico");
     //   public static Icon IconSvg = new Icon(InstallPath + "\\3DPrint.svg");
        public static string AppName = "Export Open Add-On ";

    }
}