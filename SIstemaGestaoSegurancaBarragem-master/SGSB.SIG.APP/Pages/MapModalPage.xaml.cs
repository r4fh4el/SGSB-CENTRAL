using Microsoft.Maui.Controls.Maps;

namespace SGSB.SIG.APP.Pages;

public partial class MapModalPage : ContentPage
{
	Microsoft.Maui.Controls.Maps.Map mauiMap;

	List<Location> polygonPoints = new List<Location>();
	List<Polygon> polygonLines = new List<Polygon>();
	Pin messagePin;
	public MapModalPage()
	{
		InitializeComponent();
		//mauiMap = new Microsoft.Maui.Controls.Maps.Map
		//{
		//	IsShowingUser = true // Ativar localização do usuário
		//};

		mappy.MapClicked += OnMapClicked;

		Content = mappy;

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

		var message = "TEste";
		messagePin = new Pin
		{
			Label = message,
			Location = e.Location,
			Type = PinType.Generic,
			AutomationId = "pinMessage"
		};

		mappy.Pins.Add(messagePin);
	}

	private async void OnCloseMapClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Zona());
	}
}

		