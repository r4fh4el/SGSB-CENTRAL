using SGSB.AppPopulacao.Data;
using SGSB.AppPopulacao.MVVM.Models;
using SGSB.AppPopulacao.MVVM.ViewModel;

namespace SGSB.AppPopulacao;

public partial class CadastroTicket : ContentPage
{
    public CadastroTicket()
    {
        InitializeComponent();
        BindingContext = new TicketViewModel();
    }

    private async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        var ticketModel = new TicketModel()
        {
            Descricao = tituloEntry.Text,
            Titulo = tituloEntry.Text,
            DataAlteracao = DateTime.Now,
            DataCadastro = DateTime.Now,
            Status = 1,
            IdUsuario = 1,
            TIcket = Guid.NewGuid().GetHashCode().ToString()
        };
        if (await TicketService.CadastrarTicket(ticketModel))
        {
            // Perform your logic with the submitted data
            await DisplayAlert("Enviado com sucesso!", $"Titulo: {ticketModel.Titulo}Ticket: {ticketModel.TIcket}", "OK");
        }
        else
        {
            // Perform your logic with the submitted data
            await DisplayAlert("Houve algum problema no envio da mensagem!", $"Titulo: {ticketModel.Titulo}Ticket: {ticketModel.TIcket}", "Falhou");
        }
    }
}