using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using Microsoft.Maui.Hosting;
using SGSB.SIG.APP.Pages;
using Microsoft.Maui.Maps;
using SGSB.SIG.APP.Model;
using SGSB.SIG.APP.Service;

namespace SGSB.SIG.APP
{
    public partial class Zona : ContentPage
    {
        Microsoft.Maui.Controls.Maps.Map mauiMap;

        List<Location> polygonPoints = new List<Location>();
        List<Polygon> polygonLines = new List<Polygon>();
        Pin messagePin;

        public Zona()
        {
            InitializeComponent();
            mappy.MapClicked += OnMapClicked;



            CenterMapOnCurrentLocation();


        }

        private async void CarregaMaps()
        {

            var mapas = await ZonaService.GetBuscarListZona();


            MapSpan visibleRegion;
            List<MapSpan> lstVisibleRegion = new List<MapSpan>();

            foreach (var map in mapas)
            {

                var locations = await Geocoding.GetLocationsAsync(map.area);

                if (locations != null && locations.Any())
                {
                    var primeiraLocalizacao = locations.First();
                    var mapSpan = MapSpan.FromCenterAndRadius(new Location(primeiraLocalizacao.Latitude, primeiraLocalizacao.Longitude), Distance.FromMiles(1.0));
                    lstVisibleRegion.Add(mapSpan);
                }


            }


            if (lstVisibleRegion.Count > 0)
            {
                // Inicialize as extremidades da região visível com as coordenadas do primeiro MapSpan
                double minLatitude = lstVisibleRegion.Min(mapSpan => mapSpan.Center.Latitude - mapSpan.LatitudeDegrees / 2);
                double maxLatitude = lstVisibleRegion.Max(mapSpan => mapSpan.Center.Latitude + mapSpan.LatitudeDegrees / 2);
                double minLongitude = lstVisibleRegion.Min(mapSpan => mapSpan.Center.Longitude - mapSpan.LongitudeDegrees / 2);
                double maxLongitude = lstVisibleRegion.Max(mapSpan => mapSpan.Center.Longitude + mapSpan.LongitudeDegrees / 2);

                // Crie uma nova região visível com base nas extremidades calculadas
                MapSpan novaVisibleRegion = MapSpan.FromCenterAndRadius(
                    new Location((minLatitude + maxLatitude) / 2, (minLongitude + maxLongitude) / 2),
                    Distance.FromMiles(Math.Max(maxLatitude - minLatitude, maxLongitude - minLongitude)));

                // Atribua a nova região visível ao mapa
                mappy.MoveToRegion(novaVisibleRegion);
            }



        }
        private async void OnOpenMapClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapModalPage());
        }
        // Constructor and other existing code...

        private void OnNextStepClicked(object sender, EventArgs e)
        {
            // Handle logic for moving to the next step

            // For example, hide the current step
            Step1Frame.IsVisible = false;

            // Show the next step
            Step2Frame.IsVisible = true;
        }
        private void OnNextStep2Clicked(object sender, EventArgs e)
        {
            // Handle logic for moving to the next step

            // For example, hide the current step
            Step1Frame.IsVisible = false;

            // Show the next step
            Step2Frame.IsVisible = false;
            // Show the next step
            Step3Frame.IsVisible = true;
        }
        private void OnPreviousStepClicked(object sender, EventArgs e)
        {
            // Handle logic for moving to the previous step

            // For example, hide the current step
            Step2Frame.IsVisible = false;
            // For example, hide the current step
            Step3Frame.IsVisible = false;

            // Show the previous step
            Step1Frame.IsVisible = true;
        }
        private void OnPreviousStep3Clicked(object sender, EventArgs e)
        {
            // Handle logic for moving to the previous step

            // For example, hide the current step
            Step2Frame.IsVisible = true;

            // Show the previous step
            Step1Frame.IsVisible = false;
        }
        private async void OnFinishClicked(object sender, EventArgs e)
        {
            ZonaModel zonaModel = new ZonaModel();
            zonaModel.cpf = txtCPF.Text;
            zonaModel.endereco = txtEndereco.Text;
            zonaModel.nomeCidadao = txtNomeCidadao.Text;
            zonaModel.numeroPessoas = Int32.Parse(txtNumeroPessoas.Text);
            zonaModel.numeroTelefone = txtNumeroTelefone.Text;
            zonaModel.observacao = txtObservacao.Text;
            zonaModel.renda = txtRenda.Text;
            zonaModel.dataNascimento = dpDataNascimento.Date;
            zonaModel.idZonaNome = idZonaNome.Text;
            zonaModel.usuario = Infra.Constantes.USUARIO_AUTENTICADO.login;
            //       ZonaModel zonaModel = new ZonaModel();
            //zonaModel.cpf = txtCPF.Text;
            //zonaModel.endereco = txtEndereco.Text;
            //zonaModel.nomeCidadao = txtNomeCidadao.Text;
            //zonaModel.numeroPessoas = Int32.Parse(txtNumeroPessoas.Text);
            //zonaModel.numeroTelefone = txtNumeroTelefone.Text;
            //zonaModel.observacao = txtObservacao.Text;
            //zonaModel.renda = txtRenda.Text;
            //zonaModel.dataNascimento = dpDataNascimento.Date;
            //zonaModel.idZonaNome = idZonaNome.Text;
            //zonaModel.usuario = Infra.Constantes.USUARIO_AUTENTICADO.login;





            zonaModel.area = mappy.VisibleRegion.ToString();


            if (await ZonaService.CadastrarZona(zonaModel))
            {
                // Perform your logic with the submitted data
                await DisplayAlert("Mensagem", "Zona cadastrada com sucesso!", "OK");

                await Navigation.PushAsync(new ListarZona());
            }
            else
            {
                // Perform your logic with the submitted data
                await DisplayAlert("Mensagem", "Erro de cadastro!", "Cancel");
            }

            //await DisplayAlert("Mensagem", "Zona cadastrada com sucesso!", "OK").ConfigureAwait(false);
        }
        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            polygonPoints.Add(e.Location);

            if (polygonPoints.Count > 1)
            {
                var lastPoint = polygonPoints[polygonPoints.Count - 2];
                var newPoint = polygonPoints[polygonPoints.Count - 1];

                var polygon = new Polygon
                {
                    FillColor = Color.FromRgb(255, 165, 0), // Laranja em RGB
                    StrokeColor = Color.FromRgb(255, 215, 0), // Adicione StrokeColor se desejar uma borda colorida
                    StrokeWidth = 2
                };

                polygon.Geopath.Add(lastPoint);
                polygon.Geopath.Add(newPoint);

                polygonLines.Add(polygon);

                mappy.MapElements.Add(polygon);
            }

            if (polygonPoints.Count > 2)
            {
                // Close the polygon by adding the first point to the end
                var firstPoint = polygonPoints[0];
                var lastPoint = polygonPoints[polygonPoints.Count - 1];

                var closedPolygon = new Polygon
                {
                    FillColor = Color.FromRgb(255, 165, 0), // Laranja em RGB
                    StrokeColor = Color.FromRgb(255, 215, 0), // Adicione StrokeColor se desejar uma borda colorida
                    StrokeWidth = 2
                };

                foreach (var point in polygonPoints)
                {
                    closedPolygon.Geopath.Add(point);
                }

                closedPolygon.Geopath.Add(firstPoint); // Connect the last point to the first point

                // Remove the previous polygon and add the closed one
                mappy.MapElements.Remove(polygonLines[polygonLines.Count - 1]);
                mappy.MapElements.Add(closedPolygon);

                // Update the list of polygons
                polygonLines[polygonLines.Count - 1] = closedPolygon;
            }

            if (messagePin != null)

                mappy.Pins.Remove(messagePin);

            var message = idZonaNome.Text;
            messagePin = new Pin
            {
                Label = message,
                Location = e.Location,
                Type = PinType.Generic,
                AutomationId = "pinMessage"
            };

            mappy.Pins.Add(messagePin);
        }
        private async void CenterMapOnCurrentLocation()
        {
            try
            {
                // Obter a localização atual
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    // Definir as coordenadas do mapa para a localização atual
                    mappy.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromMiles(1)));
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções ao obter a localização
                // Pode ser necessário lidar com casos onde as permissões não foram concedidas
            }
        }
        private async void OnCloseMapClicked(object sender, EventArgs e)
        {
            // Acessa a coleção de pinos do mapa
            var pinCollection = mappy.Pins;
            var MapElements = mappy.MapElements;
            // Remove todos os pinos do mapa
            pinCollection.Clear();
            MapElements.Clear();
            polygonPoints.Clear();
            polygonLines.Clear();

        }
    }

}

