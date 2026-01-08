using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using Microsoft.Maui.Hosting;
using System.Drawing;

namespace SGSB.SIG.APP
{
    public partial class MainPage : ContentPage
    {
        Microsoft.Maui.Controls.Maps.Map mauiMap;

        List<Location> polygonPoints = new List<Location>();
        List<Polygon> polygonLines = new List<Polygon>();
        Pin messagePin;

        public MainPage()
        {
            InitializeComponent();


            mauiMap = new Microsoft.Maui.Controls.Maps.Map
         {
             IsShowingUser = true // Ativar localização do usuário
         };

            mauiMap.MapClicked += OnMapClicked;

            Content = mauiMap;

        }

        [Obsolete]
        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            polygonPoints.Add(e.Location);

            if (polygonPoints.Count > 1)
            {
                var lastPoint = polygonPoints[polygonPoints.Count - 2];
                var newPoint = polygonPoints[polygonPoints.Count - 1];

                var objPolygon = new Polygon
                {
                    StrokeColor = Microsoft.Maui.Graphics.Color.FromHex("#FF9900") ,
                    StrokeWidth = 8,
                    FillColor = Microsoft.Maui.Graphics.Color.FromHex("#88FF9900")
                };

                objPolygon.Geopath.Add(lastPoint);
                objPolygon.Geopath.Add(newPoint);

                polygonLines.Add(objPolygon);

                mauiMap.MapElements.Add(objPolygon);
            }

            if (messagePin != null)
                mauiMap.Pins.Remove(messagePin);

            var message = "Seu Texto Aqui!";
            messagePin = new Pin
            {
                Label = message,
                Location = e.Location,
                Type = PinType.Generic,
                AutomationId = "pinMessage"
            };

            mauiMap.Pins.Add(messagePin);
        }
    }
}
