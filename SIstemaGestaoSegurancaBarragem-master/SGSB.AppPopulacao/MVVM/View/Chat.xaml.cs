

using Microsoft.AspNetCore.SignalR.Client;


namespace SGSB.AppPopulacao;

public partial class Chat : ContentPage
{
    private  HubConnection _hubConnection;
    public Chat()
	{
		InitializeComponent();

    }

    private async Task OnSendClickedAsync(object sender, EventArgs e)
    {
        string message = MessageEntry.Text;
        _hubConnection = new HubConnectionBuilder()
             .WithUrl("https://sgsb.com.br/myhub")
             .Build();

        //await _hubConnection.StartAsync();
        _hubConnection.On<string, string>("ReceiveMessage", async (user, message) =>
        {
       await SendPushMessage( message);
            Console.WriteLine($"Received message from {user}: {message}");
        });

    
        if (!string.IsNullOrEmpty(message))
        {
            AddMessageToChat("Você", message);
           
            AddMessageToChat("Bot", "Recebido: " + message);
     
            MessageEntry.Text = string.Empty; 
        }
    }

    private void AddMessageToChat(string sender, string message)
    {
        ChatArea.Children.Add(new Label
        {
            Text = $"{sender}: {message}",
            Margin = new Thickness(5)
        });
    }

    public async Task StartHubConnection()
    {
        await _hubConnection.StartAsync();
    }

    public async Task SendPushMessage(string message)
    {
        await _hubConnection.SendAsync("SendPushMessage", message);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        OnSendClickedAsync(sender, e);
    }
}