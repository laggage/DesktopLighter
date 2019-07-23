using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DesktopLighter.Core.Win32.Interop;

namespace DesktopLighter.Core
{
    public class Gdi32BrightnessController : IBrightnessController
    {
        private IntPtr _hdc;
        private RAMP _ramp;

        public Gdi32BrightnessController()
        {
            //_hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
            _ramp = new RAMP();
        }

        public float MonitorBrightness => throw new NotImplementedException();

        public bool SetMonitorBrightness(float brightness)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gamma range from 3 - 44
        /// </summary>
        /// <param name="gamma"></param>
        private void SetGamma(int gamma)
        {
            _ramp.Red = new ushort[256];
            _ramp.Green = new ushort[256];
            _ramp.Blue = new ushort[256];

            for (int i = 1; i < 256; i++)
            {
                _ramp.Red[i] = _ramp.Green[i] = _ramp.Blue[i]
                    = (ushort) Math.Min(
                        65535,
                        Math.Max(
                            0,(Math.Pow((i + 1) / 256.0, gamma * 0.1)*65535+0.5)));
            }

            Gdi32Interop.SetDeviceGammaRamp(_hdc, ref _ramp);
        }
    }
}
