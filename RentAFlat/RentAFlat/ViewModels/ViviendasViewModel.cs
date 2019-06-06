using RentAFlat.Base;
using RentAFlat.Models;
using RentAFlat.Repositories;
using RentAFlat.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentAFlat.ViewModels
{
    public class ViviendasViewModel : ViewModelBase
    {
        RepositoryRentAFlat repo;
        
        public ViviendasViewModel()
        {
            this.repo = new RepositoryRentAFlat();
            IsBusy = true;
            Task.Run(async () =>
            {
                await this.CargarViviendas();
                IsBusy = false;
            });
        }
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        public ViviendasViewModel(bool cargar)
        {
            this.repo = new RepositoryRentAFlat();
        }

        private async Task CargarViviendas()
        {
            List<Vivienda> lista = await this.repo.GetViviendas();
            this.Viviendas = new ObservableCollection<Vivienda>(lista);
            int total = lista.Count();
            int current = 1;
            foreach (Vivienda vivienda in lista)
            {
                vivienda.Total = total;
                vivienda.Current = current;
                current++;
            }
        }

        private ObservableCollection<Vivienda> _Viviendas;

        public ObservableCollection<Vivienda> Viviendas
        {
            get { return this._Viviendas; }
            set
            {
                this._Viviendas = value;
                OnPropertyChanged("Viviendas");
               
            }
        }

        public Command MostrarDetallesVivienda
        {
            get
            {
                return new Command(async (vivienda) =>
                {
                    DetallesVivienda view = new DetallesVivienda();
                    ViviendaViewModel viewmodel = new ViviendaViewModel();
                    PrincipalMaster master = new PrincipalMaster();
                    viewmodel.Vivienda = vivienda as Vivienda;
                    view.BindingContext = viewmodel;
                    master.Detail = new NavigationPage(view)
                    {
                        BarBackgroundColor = Color.DarkBlue
                    }; 
                    await Application.Current.MainPage.Navigation.PushModalAsync(master);
                    
                });
            }
        }

        public Command NuevaVivienda
        {
            get
            {
                return new Command(async () =>
                {
                    InsertarVivienda view = new InsertarVivienda();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);

                    //debemos indicar qué vista queremos que se actualice

                    MessagingCenter.Subscribe<ViviendasViewModel>(this, "INSERTAR", async (sender) =>
                    {
                        //si recibimos una respuesta de "insertar", hacemos cosas...
                        await this.CargarViviendas();//para que actualice las vistas necesarias
                    });
                });
            }
        }

        public Command Eliminarvivienda
        {
            get
            {
                return new Command(async (doctor) =>
                {
                    EliminarVivienda view = new EliminarVivienda();
                    ViviendaViewModel viewmodel = new ViviendaViewModel();
                    viewmodel.Vivienda = doctor as Vivienda;
                    view.BindingContext = viewmodel;

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);

                    MessagingCenter.Subscribe<ViviendasViewModel>(this, "BORRADO", async (sender) =>
                    {
                        //si recibimos una respuesta de "insertar", realizamos lo que necesitemos
                        await this.CargarViviendas();
                    });
                });
            }
        }

        public Command Modificarvivienda
        {
            get
            {
                return new Command(async (doctor) =>
                {
                    ModificarVivienda view = new ModificarVivienda();
                    ViviendaViewModel viewmodel = new ViviendaViewModel();
                    viewmodel.Vivienda = doctor as Vivienda;
                    view.BindingContext = viewmodel;

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);

                    MessagingCenter.Subscribe<ViviendasViewModel>(this, "MODIFICAR", async (sender) =>
                    {
                        //si recibimos una respuesta de "insertar", realizamos lo que necesitemos
                        await this.CargarViviendas();
                    });
                });
            }
        }


    }
}
