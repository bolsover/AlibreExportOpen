; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "AlibreExportOpen"
#define MyAppVersion "1.0"
#define MyAppPublisher "David Bolsover"
#define MyAppURL "https://github.com/bolsover/AlibreExportOpen"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A9D330B0-C04E-41DA-8666-E543E16665C7}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\AlibreExportOpen
DefaultGroupName={#MyAppName}
;LicenseFile=D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\Copyright and License.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Setup
OutputBaseFilename=AlibreExportOpen
SetupIconFile=D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\nexus.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Debug\3DPrint.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Debug\AlibreExportOpen.adc"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Debug\AlibreExportOpen.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Debug\AlibreExportOpen.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Debug\AlibreExportOpen.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Repository\Jetbrains\Bolsover\AlibreExportOpen\bin\Debug\nexus.ico"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files


[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[Registry]
Root: HKLM; Subkey: "SOFTWARE\Alibre Design Add-Ons"; Flags: uninsdeletekeyifempty; Check: IsAdminInstallMode
Root: HKLM; Subkey: "SOFTWARE\Alibre Design Add-Ons"; ValueType: string; ValueName: "{{378829C4-F122-4617-92E0-E36ADD4F9AA8}"; ValueData: "{autopf}\{#MyAppName}"; Check: IsAdminInstallMode