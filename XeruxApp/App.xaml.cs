using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Xerux.GameBarWidgets;

namespace Xerux
{
    sealed partial class App : Application
    {
        private Dictionary<string, XboxGameBarWidget> GameBarWidgets;

        public App()
        {
            GameBarWidgets = new Dictionary<string, XboxGameBarWidget>();
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (Window.Current.Content != null) return;

            var rootFrame = new Frame();
            rootFrame.NavigationFailed += OnNavigationFailed;
            rootFrame.Navigate(typeof(MainPage), e.Arguments);

            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        protected override void OnActivated(IActivatedEventArgs eventArgs)
        {
            XboxGameBarWidgetActivatedEventArgs widgetArgs = null;

            if (eventArgs.Kind == ActivationKind.Protocol)
            {
                var protocolArgs = (IProtocolActivatedEventArgs)eventArgs;
                if (protocolArgs.Uri.Scheme.Equals("ms-gamebarwidget"))
                {
                    widgetArgs = (XboxGameBarWidgetActivatedEventArgs)eventArgs;
                }
            }

            if (widgetArgs != null && widgetArgs.IsLaunchActivation)
            {
                var widgetRoot = new Frame();
                widgetRoot.NavigationFailed += OnNavigationFailed;
                widgetRoot.Navigate(typeof(XeruxWidget));

                GameBarWidgets.Add(nameof(XeruxWidget), new XboxGameBarWidget(widgetArgs, Window.Current.CoreWindow, widgetRoot));

                Window.Current.Content = widgetRoot;
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            // clean app here...            
            deferral.Complete();
        }
    }
}
