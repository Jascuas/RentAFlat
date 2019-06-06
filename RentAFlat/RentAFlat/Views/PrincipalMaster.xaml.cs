using RentAFlat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentAFlat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrincipalMaster : MasterDetailPage
    {
        public List<MenuItemPagina> MenuPaginas { get; set; }
        public PrincipalMaster ()
		{
			InitializeComponent ();
            this.MenuPaginas = new List<MenuItemPagina>();
            var page1 = new MenuItemPagina()
            {
                Titulo = "Home",
                TipoPagina = typeof(HomeView)
            };
            this.MenuPaginas.Add(page1);
            var page2 = new MenuItemPagina()
            {
                Titulo = "Acerca de nosotros",
                TipoPagina = typeof(Conocenos)
            };
            this.MenuPaginas.Add(page2);
            var page5 = new MenuItemPagina()
            {
                Titulo = "Filtrar pisos",
                TipoPagina = typeof(FiltrarPisos)
            };
            this.MenuPaginas.Add(page5);
            var page3 = new MenuItemPagina()
            {
                Titulo = "Pisos en alquiler",
                TipoPagina = typeof(ViviendasView)
            };
            this.MenuPaginas.Add(page3);
            var page4 = new MenuItemPagina()
            {
                Titulo = "Area privada",
                TipoPagina = typeof(Login)
            };
            this.MenuPaginas.Add(page4);
           
            this.lsvmenu.ItemsSource = this.MenuPaginas;
            //DEBEMOS INDICAR LA PAGINA DE INICIO AL COMENZAR LA APLICACION
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomeView)))
            {
                BarBackgroundColor = Color.DarkBlue
                
            };
            IsPresented = false;

            //NECESITAMOS EL EVENTO DEL LISTVIEW
            //PARA NAVEGAR ENTRE PAGINAS
            this.lsvmenu.ItemSelected += CambiarPagina;
            
        }


        private void CambiarPagina(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            MenuItemPagina seleccionado =
                (MenuItemPagina)e.SelectedItem;
            
            Detail = new NavigationPage((Page)Activator.CreateInstance(seleccionado.TipoPagina))
            {
                BarBackgroundColor = Color.DarkBlue
            };
            IsPresented = false;
            ((ListView)sender).SelectedItem = null;
        }
    }
}