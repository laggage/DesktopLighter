namespace DesktopLighter.Wpf
{
    using DesktopLighter.Net45.Core.Interfaces;
    using DesktopLighter.Net45.Core;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System;

    internal class BrightnessViewModel : INotifyPropertyChanged, IDisposable
    {
        private IBrightnessController _brightnessController;

        private double _monitorBrightness;

        public event PropertyChangedEventHandler PropertyChanged;

        public double MonitorBrighness
        {
            get => _monitorBrightness;
            set
            {
                if (value == _monitorBrightness) return;
                _monitorBrightness = value;
                RaisePropertyChanged();
                //set monitor brightness
                Task.Run(
                    () => _brightnessController.SetMonitorBrightness((byte)_monitorBrightness));
            }
        }

        public BrightnessViewModel()
        {
            _brightnessController = new WmiBrightnessController();

            //Initialize value of monitor brightness, as while, handle system monitor brightness changed event.
            MonitorBrighness = _brightnessController.MonitorBrightness;
            _brightnessController.MonitorBrightnessChanged += (s, args) =>
            {
                if (_monitorBrightness == args.NewMonitorBrightness) return;
                _monitorBrightness = args.NewMonitorBrightness;
                RaisePropertyChanged("MonitorBrighness");
            };
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            if (_brightnessController is IDisposable d) d.Dispose();
        }
    }
}
