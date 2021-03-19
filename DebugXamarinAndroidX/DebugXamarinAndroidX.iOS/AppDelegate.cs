using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs.Infrastructure;
using Foundation;
using UIKit;

namespace DebugXamarinAndroidX.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
	        LibVLCSharp.Shared.Core.Initialize();
	        LibVLCSharp.Forms.Shared.LibVLCSharpFormsRenderer.Init();
	        //LibVLCSharp.Forms.Platforms.iOS.VideoViewRenderer(this);

	        Xamarin.Forms.Forms.SetFlags("MediaElement_Experimental");
	        Xamarin.Forms.Forms.Init();
	        Xamarin.Forms.FormsMaterial.Init();
			
	        //MediaManager.CrossMediaManager.Current.Init();
	        //ScnViewGestures.Plugin.Forms.iOS.Renderers.ViewGesturesRenderer.Init();
	        Steema.TeeChart.TChart.Init();
	        Acr.UserDialogs.UserDialogs.Init(() => UIApplication.SharedApplication.GetTopViewController());
	        Jakar.SettingsView.iOS.SettingsViewInit.Init();
	        ZXing.Net.Mobile.Forms.iOS.Platform.Init();
	        Rg.Plugins.Popup.Popup.Init();

	        LoadApplication(new App());

	        return base.FinishedLaunching(app, options);
        }
    }
}
