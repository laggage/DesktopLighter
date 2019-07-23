## DesktopLighter by laggage

os: win10
platform: .net45
language: C#

DesktopLighter is a library aim to easy control monitor brightness.

I build this project in C# Dotnet4.5. Originally, I want to build it in .net core3.0 preview, but I failed to do that. Reason is .Net core3.0 preview haven't support api under namespace of System.Management,which make me failed to invoke some function wmi in .net core 3.0 preview.

when .net core 3.0 have a good support for api under namespace of System.Management, I will rebuilt this project in .net core 3.0.

## How to use

### get brightness of current monitor

```
WmiBrightnessController.GetCurrentBrightness();
```

### set brightness of current monitor

```
IBrightnessController brightnessController = new WmiBrightnessController();
brightnessController.SetMonitorBrightness(30);
```

### get notified when brightness changed
```
IBrightnessController brightnessController = new WmiBrightnessController();
brightnessController.MonitorBrightnessChanged += (s,args) => 
{
    byte newBrightness = args.NewMonitorBrightness;
}
```

## Demo
There is a demo about how to use this project to controller monitor brightness.See
project **DesktopLighter.Wpf** under the same sln.

![](https://img2018.cnblogs.com/blog/1596066/201907/1596066-20190723213253950-345239320.png)
