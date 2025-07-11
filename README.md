# C# GameBar Widget
Besides the code implementation some mandatory updates must be done to project's `Package.appxmanifest`. Get detailed steps in the following link https://learn.microsoft.com/en-us/gaming/game-bar/guide/pkg-manifest

Only C# projects targeting Universal Windows Platform (`<TargetPlatformIdentifier>UAP</TargetPlatformIdentifier>` at `.csproj` file) can use [`Microsoft.Gaming.XboxGameBar`](https://www.nuget.org/packages/Microsoft.Gaming.XboxGameBar) NuGet package. Otherwise referencing the package like `using Microsoft.Gaming.XboxGameBar` will cause an error.

Widgets are activated using the custom app `schema` protocol the following sample project has way more details on how to handle that at https://github.com/microsoft/XboxGameBarSamples/blob/master/Samples/WidgetAdvSampleCS/App.xaml.cs#L40
