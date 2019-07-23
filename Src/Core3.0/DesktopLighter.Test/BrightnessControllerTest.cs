using DesktopLighter.Core;
using System;
using Xunit;

namespace DesktopLighter.Test
{
    public class BrightnessControllerTest
    {
        [Fact]
        public void SetBrightnessTest()
        {
            BrightnessController brightnessController = new BrightnessController();
            Assert.Throws<Exception>(() => { brightnessController.SetBrightness(0.6f); });
            //try
            //{
            //    brightnessController.SetBrightness(0.6f);
            //    Assert.
            //}
            //catch
            //{
            //    Assert.True(true);
            //}
        }
    }
}
