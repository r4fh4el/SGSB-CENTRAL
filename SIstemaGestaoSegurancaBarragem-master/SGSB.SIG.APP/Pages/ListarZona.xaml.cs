
using SGSB.SIG.APP.Model;
using SGSB.SIG.APP.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SGSB.SIG.APP.Pages;

public partial class ListarZona : ContentPage
{
    public ICommand DeleteCommand { get; }
    private ObservableCollection<ZonaModel> zones = new ObservableCollection<ZonaModel>();
    public ListarZona()
    {
        InitializeComponent();
        // Inicialize o comando no construtor
        DeleteCommand = new Command<object>(OnDeleteClicked);
        CarregaZona();
    }

    private void OnDeleteClicked(object parameter)
    {
        throw new NotImplementedException();
    }

    private async void CarregaZona()
    {
        try
        {
            zones = await ZonaService.GetBuscarListZona();




            ListView lstZona = new ListView();
            lstZona.SetBinding(ItemsView.ItemsSourceProperty, "zones");
            zoneListView.ItemsSource = zones;

        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"InvalidCastException: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            // Add more details as needed
            throw; // Rethrow the exception to ensure it's not silently ignored
        }

    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Zona());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        int idZona = (int)((Button)sender).CommandParameter;
     



      await Service.ZonaService.ExcluirZona(idZona);

        CarregaZona();
        //((Button)sender).CommandParameter
    }


}