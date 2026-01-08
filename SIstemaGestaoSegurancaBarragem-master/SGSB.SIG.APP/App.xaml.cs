using SGSB.SIG.APP.Pages;

namespace SGSB.SIG.APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new AppShell());

            ///teste
        }
    }
}