namespace DesktopLighter.Core.Win32.Helpers
{
    using DesktopLighter.Core.Win32.Interop;
    using System;
    using System.Diagnostics;

    internal class MonitorHelper
    {
        public static PhysicalMonitor[] GetPhysicalMonitors()
        {
            uint monitorCounts = uint.MinValue;
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
            IntPtr hMonitor = MonitorInterop.MonitorFromWindow(hWnd, (uint)NativeConstantsEnum.MonitorDefaultToPrimary);
            bool r = BrightnessInterop.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref monitorCounts);

            PhysicalMonitor[] monitors = new PhysicalMonitor[monitorCounts];
            
            BrightnessInterop.GetPhysicalMonitorsFromHMONITOR(hMonitor, monitorCounts, monitors);
            for (int i = 0; i < monitors.Length; i++)
                monitors[i].hPhysicalMonitor = hMonitor;
            return monitors;
        }

        public static uint GetMonitorMaxBrightness(PhysicalMonitor monitor)
        {
            uint max = 0,c = 0,min = 0;
            uint[] m = null, mi = null, cc = null;
            BrightnessInterop.GetMonitorBrightness(
                monitor.hPhysicalMonitor,  m,  cc,  mi);
            return max;
        }

        public static uint GetMonitorMinBrightness(PhysicalMonitor monitor)
        {
            uint max = 0, c = 0, min = 0;
            uint[] m = null, mi = null, cc = null;
            BrightnessInterop.GetMonitorBrightness(
                monitor.hPhysicalMonitor, m, cc, mi);
            return min;
        }
    }
}
