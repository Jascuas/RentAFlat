using RentAFlat.Base;
using RentAFlat.Models;
using RentAFlat.Repositories;
using RentAFlat.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentAFlat.ViewModels
{
    public class ViviendaViewModel : ViewModelBase
    {
        RepositoryRentAFlat repo;


        //---------------------
        public TiposVivienda TiposVivienda { get; set; }
        public Cliente Clientes { get; set; }

        public Costas Costa { get; set; }


        //----insertar al elegir del combo , el id del tipo de vivienda
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


        //--------insertar el combo de id del cliente
        private ObservableCollection<Cliente> _TipoClientes;

        public ObservableCollection<Cliente> TipoClientes
        {
            get { return this._TipoClientes; }
            set
            {
                this._TipoClientes = value;
                OnPropertyChanged("TipoClientes");
            }
        }

        private async Task CargarTipoClientes()
        {
            List<Cliente> lista = await this.repo.GetClientes();
            this.TipoClientes = new ObservableCollection<Cliente>(lista);
        }

        //--
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
        //-----------------
        public ViviendaViewModel()
        {
            this.repo = new RepositoryRentAFlat();
            if (Vivienda == null)
            {
                Vivienda = new Vivienda();
            }
            Task.Run(async () =>
            {
                await this.CargarTiposVivienda();
            });
            Task.Run(async () =>
            {
                await this.CargarTipoClientes();
            });
            Task.Run(async () =>
            {
                await this.CargarCostas();
            });
        }

        private Vivienda _Vivienda;
        public Vivienda Vivienda
        {
            get { return this._Vivienda; }
            set { this._Vivienda = value;
                OnPropertyChanged("Vivienda"); }
        }

        public Command InsertarVivienda
        {
            get
            {
                
                var session = App.Locator.SessionService;
                return new Command(async () => {
                    this.Vivienda.Cod_TipoVivienda =this.TiposVivienda.Cod_tipo_vivienda;
                    this.Vivienda.IdCliente =this.Clientes.IdCliente;
                    this.Vivienda.Cod_Provincia = this.Costa.Cod_Provincia;

                    await this.repo.InsertarVivienda(this.Vivienda,session.Datos[0].ToString());

                    //cuando haya insertado, le debo decir que debe actualizar la vista
                    MessagingCenter.Send<ViviendasViewModel>(App.Locator.ViviendasViewModel, "INSERTAR");
                });
            }
        }

        public Command EliminarVivienda
        {
            get
            {
                return new Command(async () =>
                {
                   
                    var session = App.Locator.SessionService;
                    await this.repo.EliminarVivienda(this.Vivienda.Cod_casa, session.Datos[0].ToString());
                    //cuando eliminamos un doctor, necesitamos que el listado se actualice sin ese doctor
                    //envio un mensaje a doctoresviewmodel indicando BORRADO
                    MessagingCenter.Send<ViviendasViewModel>(App.Locator.ViviendasViewModel, "BORRADO");

                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    //PopModalAsync-->cierro la pantalla y vuelvo a la anterior

                    //doctoresviewmodel me debo subscribir al mensaje borrado
                });
            }
        }

        public Command Modificarvivienda
        {
            get
            {
                return new Command(async () =>
                {
                    //this.TiposVivienda.Cod_tipo_vivienda = this.Vivienda.Cod_TipoVivienda;
                    var session = App.Locator.SessionService;
                    await this.repo.ModificarVivienda(this.Vivienda,  session.Datos[0].ToString());
                    MessagingCenter.Send<ViviendasViewModel>(App.Locator.ViviendasViewModel, "MODIFICAR");
                });
            }
        }

    }
}
