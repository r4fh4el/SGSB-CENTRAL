
using OneSignalSDK.DotNet;
using OneSignalSDK.DotNet.Core;
using OneSignalSDK.DotNet.Core.Debug;

namespace SGSB.AppPopulacao
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            Image image = new Image
            {
                Source = ImageSource.FromFile("dotnet_bot.png")
            };
            InitializeComponent();

            // Enable verbose OneSignal logging to debug issues if needed.
            OneSignal.Debug.LogLevel = LogLevel.VERBOSE;

            // OneSignal Initialization
            OneSignal.Initialize(Infra.Constantes.ONESIGNAL_APP_ID);

            // RequestPermissionAsync will show the notification permission prompt.
            // We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 5)
            OneSignal.Notifications.RequestPermissionAsync(true);

           
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}