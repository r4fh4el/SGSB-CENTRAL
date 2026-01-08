using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Net;
using SGSB.AppPopulacao.Data;
using SGSB.AppPopulacao.MVVM.Models;

namespace SGSB.AppPopulacao;

public partial class PontoEncontro : ContentPage
{
    private List<PontoEncontroModel> lstPontoEncontro;
    public PontoEncontro()
    {
        SetInitialPosition();
        //MakeApiRequest();
        InitializeComponent();
    }

    private async Task LoadPontoEncontro()
    {
        lstPontoEncontro = new List<PontoEncontroModel>();
        lstPontoEncontro = await PontoEncontroService.GetPontoEncontro();



        foreach (var item in lstPontoEncontro)
        {
            Location endLocation = new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude));
            mappy.Pins.Add(new Microsoft.Maui.Controls.Maps.Pin
            {
                Label = item.Nome,
                Location = new Location(endLocation),

            });
        };

        //mappy.Pins.Add(new Microsoft.Maui.Controls.Maps.Pin
        //{
        //    Label = "Subscribe to my channel?",
        //    Location = new Location(50.8514, 5.6910),
        //});

    }
    private async void SetInitialPosition()
    {

        var request = new GeolocationRequest(GeolocationAccuracy.Default);
        var location = await Geolocation.GetLocationAsync(request);

        if (location != null)
        {
            // Create a pin with a label
            Pin initialPin = new Pin
            {
                Label = "Aonde estou",
                Location = new Location(location.Latitude, location.Longitude),
                Type = PinType.Place
            };

            // Add the pin to the map
            mappy.Pins.Add(initialPin);

            // Set the map's center to the current location
            mappy.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Location(location.Latitude, location.Longitude),
                Distance.FromMiles(1)));

            // AddRoutePolyline(location.Latitude, location.Longitude, -11.041726, -38.784730);
            // Replace "your_api_url" with the actual URL of the API you want to call
            string apiUrl = "http://api.sgsb.com.br/API/ListarPontoEncontro";

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                    // Make a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the response as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();


                        lstPontoEncontro = await response.Content.ReadFromJsonAsync<List<PontoEncontroModel>>();


                        foreach (var item in lstPontoEncontro)
                        {

                            await DisplayDrivingRoute(location.Latitude, location.Longitude, item.Latitude, item.Longitude);
                        };

                    }
                }


                catch (Exception ex)
                {
                    // Handle exceptions (e.g., location services disabled, permissions not granted)
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }

    private async void MakeApiRequest()
    {
        List<PontoEncontroModel> lstPontoEncontro = new List<PontoEncontroModel>();
        try
        {


            // Replace "your_api_url" with the actual URL of the API you want to call
            string apiUrl = "http://api.sgsb.com.br/API/ListarPontoEncontro";

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                    // Make a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the response as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();


                        lstPontoEncontro = await response.Content.ReadFromJsonAsync<List<PontoEncontroModel>>();


                        foreach (var item in lstPontoEncontro)
                        {
                            Location endLocation = new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude));

                            mappy.Pins.Add(new Microsoft.Maui.Controls.Maps.Pin
                            {
                                Label = item.Nome,
                                Location = new Location(endLocation),
                            });
                        };

                        Console.WriteLine(apiResponse);
                    }
                    else
                    {
                        // Handle the case where the API request was not successful
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }

                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the request
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    private void AddRoutePolyline(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
    {

        // Example: Adding a polyline between two points
        var polyline = new Polygon
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 12,
        };

        polyline.Geopath.Add(new Location(startLatitude, startLongitude));
        polyline.Geopath.Add(new Location(endLatitude, endLongitude));

        mappy.MapElements.Add(polyline);
    }
    public class RoutePoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class Route
    {
        public List<RoutePoint> Points { get; set; }
    }
    public async Task DisplayDrivingRoute(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
    {
        try
        {
            // Fetch the route using Google Maps Directions API
            string apiUrl = $"https://maps.googleapis.com/maps/api/directions/json" +
                            $"?origin={startLatitude},{startLongitude}" +
                            $"&destination={endLatitude},{endLongitude}" +
                            $"&mode=driving" +
                            $"&key=AIzaSyDkIuHwgoaJBesTjO_GYjCm4BiGc3JPdbo";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to get route coordinates
                var route = ParseRoute(responseBody);

                // Add the route to the map
                AddRoutePolyline(route);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void AddRoutePolyline(Route route)
    {
        if (route != null && route.Points?.Count > 0)
        {
            var polyline = new Polyline
            {
                StrokeColor = Colors.Blue,
                StrokeWidth = 12
            };

            foreach (var point in route.Points)
            {
                polyline.Geopath.Add(new Location(point.Latitude, point.Longitude));
            }

            mappy.MapElements.Add(polyline);
        }
    }
    private Route ParseRoute(string responseBody)
    {
        // Parse the JSON response from the routing service
        // Extract relevant information such as route coordinates

        // Example parsing logic (Note: This is a simplified example)
        JObject jsonResponse = JObject.Parse(responseBody);

        var route = new Route
        {
            Points = new List<RoutePoint>()
        };

        // Extract route coordinates
        var steps = jsonResponse.SelectToken("routes[0].legs[0].steps");
        foreach (var step in steps)
        {
            double lat = (double)step.SelectToken("start_location.lat");
            double lng = (double)step.SelectToken("start_location.lng");
            route.Points.Add(new RoutePoint { Latitude = lat, Longitude = lng });
        }

        return route;
    }
    protected async override void OnAppearing()
    {
        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

    }
}