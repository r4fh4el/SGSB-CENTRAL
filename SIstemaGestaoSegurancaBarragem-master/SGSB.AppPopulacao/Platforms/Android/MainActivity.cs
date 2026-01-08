using Android.App;
using Android.Content.PM;
using Android.OS;
using OneSignalSDK.DotNet;
using OneSignalSDK.DotNet.Core;
using OneSignalSDK.DotNet.Core.Debug;


namespace SGSB.AppPopulacao
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //... Leave the existing code here

            // Enable verbose OneSignal logging to debug issues if needed.
            OneSignal.Debug.LogLevel = LogLevel.VERBOSE;

            // OneSignal Initialization
            OneSignal.Initialize(Infra.Constantes.ONESIGNAL_APP_ID);

            // RequestPermissionAsync will show the notification permission prompt.
            // We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 5)
            OneSignal.Notifications.RequestPermissionAsync(true);
        }
    }

  
}