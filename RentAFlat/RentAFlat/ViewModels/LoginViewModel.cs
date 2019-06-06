using RentAFlat.Base;
using RentAFlat.Models;
using RentAFlat.Repositories;
using RentAFlat.Services;
using RentAFlat.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RentAFlat.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        RepositoryRentAFlat repo;
        public LoginViewModel()
        {
            this.repo = new RepositoryRentAFlat();
            if (Usuario == null)
            {
                Usuario = new Usuario();
            }

        }
        private Usuario _Usuario;
        public Usuario Usuario
        {
            get { return this._Usuario; }
            set { this._Usuario = value; OnPropertyChanged("Usuario"); }
        }
        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    EncodingService encoding = new EncodingService();
                    String token = await this.repo.GetToken(this.Usuario.Login, encoding.SHA256(this.Usuario.Password));
                    if (token != null)
                    {
                        SessionService session = App.Locator.SessionService;
                        session.Datos.Add(token);
                        App.Current.MainPage = new PrincipalMasterAdmin();
                    }

                });
            }
        }
        
    }
}
