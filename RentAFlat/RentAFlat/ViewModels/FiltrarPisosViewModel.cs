using RentAFlat.Base;
using RentAFlat.Models;
using RentAFlat.Repositories;
using RentAFlat.Services;
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
    public class FiltrarPisosViewModel : ViewModelBase
    {
        RepositoryRentAFlat repo;
        public Numero NumeroBaños { get; set; }
        public Numero NumeroHabitaciones { get; set; }
        public Costas Costa { get; set; }
        public  TiposVivienda Vivienda { get; set; }
        public FiltrarPisosViewModel()
        {
            IsBusy = true;
            this.repo = new RepositoryRentAFlat();
            Task.Run(async () =>
            {
                await this.CargarTiposVivienda();
                await this.CargarCostas();
                IsBusy = false;
            });
            List<Numero> lista = new List<Numero>()
            {
                new Numero(){n = 1},
                new Numero(){n = 2},
                new Numero(){n = 3},
                new Numero(){n = 4},
                new Numero(){n = 5}
            };
            this.Habitaciones = new ObservableCollection<Numero>(lista);
            this.Baños = new ObservableCollection<Numero>(lista);
        }

        private async Task CargarTiposVivienda()
        {
            List<TiposVivienda> lista = await this.repo.GetTiposVivienda();
            this.TiposViviendas = new ObservableCollection<TiposVivienda>(lista);
        }

        private ObservableCollection<TiposVivienda> _TiposViviendas;

        public ObservableCollection<TiposVivienda> TiposViviendas
        {
            get { return this._TiposViviendas; }
            set
            {
                this._TiposViviendas = value;
                OnPropertyChanged("TiposViviendas");
            }
        }
        private async Task CargarCostas()
        {
            List<Costas> lista = await this.repo.GetCostas();
            this.Costas = new ObservableCollection<Costas>(lista);
        }

        private ObservableCollection<Costas> _Costas;

        public ObservableCollection<Costas> Costas
        {
            get { return this._Costas; }
            set
            {
                this._Costas = value;
                OnPropertyChanged("Costas");
            }
        }
        private ObservableCollection<Numero> _Baños;

        public ObservableCollection<Numero> Baños
        {
            get { return this._Baños; }
            set
            {
                this._Baños = value;
                OnPropertyChanged("Baños");
            }
        }
        private ObservableCollection<Numero> _Habitaciones;

        public ObservableCollection<Numero> Habitaciones
        {
            get { return this._Habitaciones; }
            set
            {
                this._Habitaciones = value;
                OnPropertyChanged("Habitaciones");
            }
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
        public Command FiltrarPisos
        {
            get
            {
                return new Command(async () => {

                        BusquedaModel busqueda = new BusquedaModel();
                        busqueda.TiposViviendaSelectedValue = Vivienda.Cod_tipo_vivienda;
                        busqueda.CostasSelectedValue = Costa.Cod_Provincia;
                        busqueda.NumeroBaniosSelectedValue = NumeroBaños.n;
                        busqueda.NumeroHabitacionesSelectedValue = NumeroHabitaciones.n;
                        busqueda.Cod_Cliente = 0;
                        busqueda.Cod_Casa = 0;
                        List<Vivienda> viviendas = await this.repo.GetViviendasPorFiltro(busqueda);
                        int total = viviendas.Count();
                        int current = 1;
                        foreach (Vivienda vivienda in viviendas)
                        {
                            vivienda.Total = total;
                            vivienda.Current = current;
                            current++;
                        }
                        ViviendasView view = new ViviendasView();
                        ViviendasViewModel viewmodel = new ViviendasViewModel(false);
                        PrincipalMaster master = new PrincipalMaster();
                        viewmodel.Viviendas = new ObservableCollection<Vivienda>(viviendas);
                        view.BindingContext = viewmodel;
                        master.Detail = new NavigationPage(view)
                        {
                            BarBackgroundColor = Color.DarkBlue
                        };
                        await Application.Current.MainPage.Navigation.PushModalAsync(master);
                });
            }
        }
    }
}
