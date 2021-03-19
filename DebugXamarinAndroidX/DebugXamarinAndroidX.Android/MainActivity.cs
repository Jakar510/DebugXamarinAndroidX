using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DebugXamarinAndroidX.Droid
{
    [Activity(Label = "DebugXamarinAndroidX", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Application.IActivityLifecycleCallbacks
	{
		internal static MainActivity Instance { get; private set; }
		protected override void OnCreate( Bundle savedInstanceState )
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			LibVLCSharp.Forms.Shared.LibVLCSharpFormsRenderer.Init();
			LibVLCSharp.Shared.Core.Initialize();
			LibVLCSharp.Forms.Platforms.Android.Platform.Init(this);


			Xamarin.Forms.Forms.SetFlags("MediaElement_Experimental");
			Xamarin.Forms.Forms.Init(this, savedInstanceState);
			Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);

			//MediaManager.CrossMediaManager.Current.Init();
			//ScnViewGestures.Plugin.Forms.Droid.Renderers.ViewGesturesRenderer.Init();
			Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
			Plugin.NFC.CrossNFC.Init(this);
			ZXing.Net.Mobile.Forms.Android.Platform.Init();
			Jakar.SettingsView.Droid.SettingsViewInit.Init(this);
			Acr.UserDialogs.UserDialogs.Init(this);
			Rg.Plugins.Popup.Popup.Init(this);

			if ( Build.VERSION.SdkInt >= BuildVersionCodes.Q )
				RegisterActivityLifecycleCallbacks(this);
			
			Plugin.Fingerprint.CrossFingerprint.SetCurrentActivityResolver(() => Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);

			Instance = this;
			LoadApplication(new App());
		}


		public override void OnRequestPermissionsResult( int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults )
		{
			//global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
		protected override void OnNewIntent( Android.Content.Intent intent )
		{
			base.OnNewIntent(intent);

			// Plugin NFC: Tag Discovery Interception
			Plugin.NFC.CrossNFC.OnNewIntent(intent);
		}
		protected override void OnPause()
		{
			base.OnPause();
			Xamarin.Forms.MessagingCenter.Send(nameof(App), nameof(OnPause));
		}
		protected override void OnRestart()
		{
			base.OnRestart();
			Xamarin.Forms.MessagingCenter.Send(nameof(App), nameof(OnRestart));
		}
		protected override void OnDestroy()
		{
			if ( Build.VERSION.SdkInt >= BuildVersionCodes.Q )
				UnregisterActivityLifecycleCallbacks(this);

			base.OnDestroy();
		}
		public override void OnBackPressed() { System.Diagnostics.Debug.WriteLine(Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed) ? "Android back button: There are some pages in the PopupStack" : "Android back button: There are not any pages in the PopupStack"); }
		public void OnActivityCreated( Activity activity, Bundle savedInstanceState ) { Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = activity; }
		public void OnActivityResumed( Activity activity ) { Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = activity; }
		public void OnActivityStarted( Activity activity ) { Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = activity; }
		public void OnActivitySaveInstanceState( Activity activity, Bundle outState ) { }
		public void OnActivityDestroyed( Activity activity ) { }
		public void OnActivityPaused( Activity activity ) { }
		public void OnActivityStopped( Activity activity ) { }
    }
}