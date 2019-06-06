using Autofac;
using RentAFlat.Services;
using RentAFlat.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAFlat.Configuration
{
    public class IoCConfiguration
    {
        private IContainer container;

        public IoCConfiguration()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //REGISTRAMOS TODOS LOS TIPOS DE INYECCION DE DEPENDENCIAS
            //O CLASES QUE NECESITAMOS QUE NOS DEVUELVA EL CONTENEDOR
            //CLASES COMUNICADAS ENTRE OTRAS
            builder.RegisterType<SessionService>().SingleInstance();
            builder.RegisterType<FiltrarPisosViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<ViviendasViewModel>();
            builder.RegisterType<ViviendaViewModel>();
            this.container = builder.Build();
        }

        //CREAMOS METODOS PARA QUE EL CONTENEDOR SEA CAPAZ
        //DE DEVOLVER LAS CLASES QUE LLAMEMOS DE FORMA EXPLICITA
        public SessionService SessionService
        {
            get { return this.container.Resolve<SessionService>(); }
        }
        public FiltrarPisosViewModel FiltrarPisosViewModel
        {
            get
            {
                return this.container.Resolve<FiltrarPisosViewModel>();
            }
        }
        public LoginViewModel LoginViewModel
        {
            get
            {
                return this.container.Resolve<LoginViewModel>();
            }
        }
        
        public ViviendaViewModel ViviendaViewModel
        {
            get
            {
                return this.container.Resolve<ViviendaViewModel>();
            }
        }
        public ViviendasViewModel ViviendasViewModel
        {
            get
            {
                return this.container.Resolve<ViviendasViewModel>();
            }
        }
    }
}
