using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using Microsoft.Maui.Hosting;
using SGSB.SIG.APP.Pages;
using SGSB.SIG.APP.Infra;
using OneOf.Types;

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

			if (Constantes.FLAG_AUTENTICADO)
			{
                this.RedirectToLoginPage();
            }
               
 
		}
        public MainPage(bool logado)
        {
            InitializeComponent();


 
        }
        private async void RedirectToLoginPage()
        {
			// Configurando a LoginPage como a nova página
			if (Constantes.USUARIO_AUTENTICADO == null)
			{
				await Navigation.PushModalAsync(new LoginPage());
			}
			else
			{
			
                Constantes.FLAG_AUTENTICADO = false;
                await Navigation.PushAsync(new MainPage());
            }
            
        
        }

        public static implicit operator MainPage(NavigationPage v)
        {
            throw new NotImplementedException();
        }
    }
}
