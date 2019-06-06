using RentAFlat.Models;
using RentAFlat.Services;
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
	public partial class PrincipalMasterAdmin : MasterDetailPage
    {
        public List<MenuItemPagina> MenuPaginas { get; set; }
        public PrincipalMasterAdmin()
        {
            InitializeComponent();
            this.MenuPaginas = new List<MenuItemPagina>();
            var page1 = new MenuItemPagina()
            {
                Titulo = "Admin",
                TipoPagina = typeof(Admin)
            };
            this.MenuPaginas.Add(page1);
            var page2 = new MenuItemPagina()
            {
                Titulo = "Ver Total Viviendas",
                TipoPagina = typeof(ViviendasView)
            };
            this.MenuPaginas.Add(page2);


            var page3 = new MenuItemPagina()
            {
                Titulo = "Filtro viviendas",
                TipoPagina = typeof(FiltrarPisos)
            };
            this.MenuPaginas.Add(page3);


            var page4 = new MenuItemPagina()
            {
                Titulo = "Viviendas",
                TipoPagina = typeof(BackViviendasView)
            };
            this.MenuPaginas.Add(page4);

            var page5 = new MenuItemPagina()
            {
                Titulo = "Logout",
                TipoPagina = typeof(HomeView)         
            };
            this.MenuPaginas.Add(page5);
            this.lsvmenu.ItemsSource = this.MenuPaginas;
            //DEBEMOS INDICAR LA PAGINA DE INICIO AL COMENZAR LA APLICACION
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Admin)))
            {
                BarBackgroundColor = Color.Black
            };
            IsPresented = false;
            //NECESITAMOS EL EVENTO DEL LISTVIEW
            //PARA NAVEGAR ENTRE PAGINAS
            BackgroundColor = Color.Black;
            this.lsvmenu.ItemSelected += CambiarPagina;
        }
        private void CambiarPagina(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            MenuItemPagina seleccionado =
                (MenuItemPagina)e.SelectedItem;
            Detail = new NavigationPage((Page)Activator.CreateInstance(seleccionado.TipoPagina))
            {
                BarBackgroundColor = Color.Black
            };
            IsPresented = false;
            if(seleccionado.Titulo == "Logout")
            {
                SessionService session = App.Locator.SessionService;
                session.Datos.Add("");
                App.Current.MainPage = new PrincipalMaster();
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}