using Microsoft.JSInterop;
using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace SGSB.Web.Pages.Maps
{
    public class GoogleMap
    {
        private readonly string apiKey;
        private IJSRuntime JSRuntime { get; set; }
        public GoogleMap(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task CalculateRoute(string origin, string destination, string travelMode)
        {

            // Use JavaScript interop to call the Google Maps API
            await JSRuntime.InvokeVoidAsync("calculateRoute", apiKey, origin, destination, travelMode);
        }
    }
}
