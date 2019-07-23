namespace DesktopLighter.Core
{
    using System;
    using System.Management;

    public class WmiBrightnessController : IBrightnessController,IDisposable
    {
        private readonly ManagementEventWatcher _eventWatcher;
        private float _monitorBrightness;
        public float MonitorBrightness => _monitorBrightness;

        public WmiBrightnessController()
        {
            GetBrightness();
            ManagementScope scope = new ManagementScope("root\\WMI");
            EventQuery q = new EventQuery("Select * From WmiMonitorBrightnessEvent");

            _eventWatcher = new ManagementEventWatcher(scope, q);
            _eventWatcher.EventArrived += _eventWatcher_EventArrived;
            _eventWatcher.Start();
        }

        private void _eventWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            byte value = (byte)e.NewEvent.Properties["Brightness"].Value;
            _monitorBrightness = value/100f+0.5f;
        }

        private void GetBrightness()
        {
            _monitorBrightness = 0;
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
                            _monitorBrightness = (byte)mObj.Properties["CurrentBrightness"].Value/100f+0.5f;
                            break;
                        }
                    }
                }
            }
            catch(Exception)
            { //ignore
            }
        }


        public bool SetMonitorBrightness(float brightness)
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
                                "WmiSetBrightness", new object[] { uint.MaxValue, (byte)(brightness*100) });
                            break;
                        }
                    }
                }
                return true;
            }
            catch(Exception)
            { return false; }
        }

        public void Dispose()
        {
            _eventWatcher.Dispose();
        }
    }
}
