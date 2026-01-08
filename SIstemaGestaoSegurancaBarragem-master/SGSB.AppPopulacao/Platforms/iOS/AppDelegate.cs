using Foundation;
using OneSignalSDK.DotNet;
using OneSignalSDK.DotNet.Core;
using OneSignalSDK.DotNet.Core.Debug;
using UIKit;


namespace SGSB.AppPopulacao
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //... Leave existing code here

            // Enable verbose OneSignal logging to debug issues if needed.
            OneSignal.Debug.LogLevel = LogLevel.VERBOSE;

            // OneSignal Initialization
            OneSignal.Initialize(Infra.Constantes.ONESIGNAL_APP_ID);

            // RequestPermissionAsync will show the notification permission prompt.
            // We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 5)
            OneSignal.Notifications.RequestPermissionAsync(true);

            return base.FinishedLaunching(app, options);
        }
    }
}