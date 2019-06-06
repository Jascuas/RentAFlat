using System;
using System.Collections.Generic;
using System.Text;

namespace RentAFlat.Services
{
    public class SessionService
    {
        public List<String> Datos { get; set; }
        public SessionService()
        {
            this.Datos = new List<string>();
        }
    }
}
