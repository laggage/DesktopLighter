namespace DesktopLighter.Net45.Core
{
    using DesktopLighter.Net45.Core.Interfaces;
    using System;
    using System.Management;

    public class MonitorBrightnessChangedEventArgs:EventArgs
    {
        public MonitorBrightnessChangedEventArgs(byte newMonitorBrightness)
        {
            NewMonitorBrightness = newMonitorBrightness;
        }
        public byte NewMonitorBrightness { get; }
    }

    public class WmiBrightnessController : IBrightnessController,IDisposable
    {
        private readonly ManagementEventWatcher _eventWatcher;
        public byte MonitorBrightness { get; private set; }
        public event EventHandler<MonitorBrightnessChangedEventArgs> MonitorBrightnessChanged;

        public WmiBrightnessController()
        {
            GetBrightness();
            ManagementScope scope = new ManagementScope("root\\WMI");
            EventQuery q = new EventQuery("Select * From WmiMonitorBrightnessEvent");

            _eventWatcher = new ManagementEventWatcher(scope, q);

            _eventWatcher.EventArrived += (s, arg) =>
            {
                MonitorBrightness = (byte)arg.NewEvent.Properties["Brightness"].Value;
                MonitorBrightnessChanged?.Invoke(this, new MonitorBrightnessChangedEventArgs(MonitorBrightness));
            };
            _eventWatcher.Start();
        }

        private void GetBrightness()
        {
            MonitorBrightness = GetCurrentMonitorBrightness();
        }

        public bool SetMonitorBrightness(byte brightness)
        {
            try
            {
                ManagementScope scope = new ManagementScope("root\\WMI");
                SelectQuery query = new SelectQuery("WmiMonitorBrightnessMethods");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    using (ManagementObjectCollection objectCollection = searcher.Get())
                    {
                        foreach (ManagementObject mObj in objectCollection)
                        {
                            mObj.InvokeMethod(
                                "WmiSetBrightness", new object[] { uint.MaxValue, brightness });
                            break;
                        }
                    }
                }
                return true;
            }
            catch(Exception)
            { return false; }
        }

        public static byte GetCurrentMonitorBrightness()
        {
            byte brightness = 0;
            try
            {
                ManagementScope s = new ManagementScope("root\\WMI");
                SelectQuery q = new SelectQuery("WmiMonitorBrightness");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(s, q))
                {
                    using (ManagementObjectCollection objectCollection = searcher.Get())
                    {
                        foreach (ManagementObject mObj in objectCollection)
                        {
                            brightness = (byte)mObj.Properties["CurrentBrightness"].Value;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            { //ignore
            }

            return brightness;
        }

        public void Dispose()
        {
            _eventWatcher.Stop();
            _eventWatcher.Dispose();
        }
    }
}
