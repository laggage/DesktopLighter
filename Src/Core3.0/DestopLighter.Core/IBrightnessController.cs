namespace DesktopLighter.Core
{
    public interface IBrightnessController
    {
        float MonitorBrightness { get; }
        bool SetMonitorBrightness(float brightness);
    }
}
