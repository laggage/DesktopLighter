namespace DesktopLighter.Net45.Core.Interfaces
{
    using System;

    public interface IBrightnessController
    {
        byte MonitorBrightness { get; }
        bool SetMonitorBrightness(byte brightness);

        event EventHandler<MonitorBrightnessChangedEventArgs> MonitorBrightnessChanged;
    }
}
