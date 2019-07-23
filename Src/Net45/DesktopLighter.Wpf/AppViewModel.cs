namespace DesktopLighter.Wpf
{
    internal class AppViewModel
    {
        public AppViewModel()
        {
            BrightnessViewModel = new BrightnessViewModel();
        }

        public BrightnessViewModel BrightnessViewModel { get; }
    }
}
