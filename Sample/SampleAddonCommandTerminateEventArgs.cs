using System;

namespace AlibreExportOpen.Sample
{
    public class SampleAddonCommandTerminateEventArgs : EventArgs
    {
        public SampleAddonCommandTerminateEventArgs(SampleAddOnCommand sampleAddOnCommand)
        {
            SampleAddOnCommand = sampleAddOnCommand;
        }

        public SampleAddOnCommand SampleAddOnCommand { get; }
    }
}