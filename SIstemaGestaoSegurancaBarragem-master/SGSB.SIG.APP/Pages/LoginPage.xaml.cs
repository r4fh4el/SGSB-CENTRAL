namespace SGSB.SIG.APP.Pages;
using Microsoft.Maui.Controls;
using SGSB.SIG.APP.Infra;
using SGSB.SIG.APP.Model;
using SGSB.SIG.APP.Service;

public partial class LoginPage : ContentPage
{

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        var aAthModel = new AuthModel()
        {
            login = username,
            senha = password,
        };
        if (await AuthService.Logar(aAthModel))
        {
            // Perform your logic with the submitted data
            await DisplayAlert("logon", "Autenticação realizada com sucesso!", "OK");
            // Feche a página de login
            // Navegue para a próxima página após o login bem-sucedido

        

            // Navegue para a próxima página após o login bem-sucedido
            await Navigation.PushAsync(new MainPage());

            // Remova a página de login modal da pilha de navegação
            await Navigation.PopModalAsync();
            // Feche a página de login
            Navigation.RemovePage(this);
            //AppShell MainPage = new AppShell();

            //await Navigation.PushModalAsync(MainPage, false);

        }
        else
        {
            // Perform your logic with the submitted data
            await DisplayAlert("Falha de logon", "Verifique o login  e a senha se estão corretos!", "Cancel");
        }
    }



    private void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        // Adicione a lógica para lidar com a recuperação de senha aqui
        DisplayAlert("Esqueceu a senha", "Recuperação de senha clicada!", "OK");
    }
}

