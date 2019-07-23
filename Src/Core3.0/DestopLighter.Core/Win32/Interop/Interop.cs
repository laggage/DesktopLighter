namespace DesktopLighter.Core.Win32.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PhysicalMonitor
    {
        public IntPtr hPhysicalMonitor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szPhysicalMonitorDescription;
    }

    internal enum McDisplayTechnologyType
    {
        McShadowMaskCathodeRayTube,
        McApertureGrillCathodeRayTube,
        McThinFilmTransistor,
        McLiquidCrystalOnSilicon,
        McPlasma,
        McOrganicLightEmittingDiode,
        McElectroluminescent,
        McMicroelectromechanical,
        McFieldEmissionDevice,
    }

    internal enum NativeConstantsEnum
    {
        MonitorDefaultToNull,
        MonitorDefaultToPrimary,
        MonitorDefaultToNearest
    }

    internal static class BrightnessInterop
    {
        [DllImport("dxva2.dll")]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, ref uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll")]
        public static extern bool GetPhysicalMonitorsFromHMONITOR([In] IntPtr hMonitor,
                  uint dwPhysicalMonitorArraySize, [Out] PhysicalMonitor[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll")]
        public static extern bool DestroyPhysicalMonitors(uint dwPhysicalMonitorArraySize,
                       [Out] PhysicalMonitor[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll")]
        public static extern bool GetMonitorTechnologyType(IntPtr hMonitor,
                        ref McDisplayTechnologyType pdtyDisplayTechnologyType);

        [DllImport("dxva2.dll")]
        public static extern bool GetMonitorCapabilities(IntPtr hMonitor, ref uint pdwMonitorCapabilities,
                        ref uint pdwSupportedColorTemperatures);

        [DllImport("dxva2.dll")]
        public static extern bool SetMonitorBrightness(IntPtr hMonitor, short brightness);

        [DllImport("dxva2.dll")]
        public static extern bool SetMonitorContrast(IntPtr hMonitor, short contrast);

        [DllImport("dxva2.dll")]
        public static extern bool GetMonitorBrightness([In] IntPtr hMonitor, [Out] uint[] pdwMinimumBrightness,
                        [Out] uint[] pdwCurrentBrightness, [Out] uint[] pdwMaximumBrightness);

        [DllImport("dxva2.dll")]
        public static extern bool GetMonitorContrast(IntPtr hMonitor, out short pwdMinimumContrast,
            out short pwdCurrentContrast, out short pwdMaximumContrast);
    }

    internal static class User32Interop
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);
    }

    internal struct RAMP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Blue;
    }

    internal static class Gdi32Interop
    {
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [DllImport("gdi32.dll")]
        public static extern int SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);
    }

    internal static class MonitorInterop
    {
        /// <summary>
        /// get handle of monitor which displaying the current app's mainwindow
        /// </summary>
        /// <param name="hwnd">handle of a window</param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow([In] IntPtr hwnd, uint dwFlags);
    }
}
