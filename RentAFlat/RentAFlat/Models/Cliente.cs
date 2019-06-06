using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAFlat.Models
{
    public class Cliente
    {
        [JsonProperty("IdCliente")]
        public int IdCliente { get; set; }

        [JsonProperty("NombreCliente")]
        public String NombreCliente { get; set; }
        //--
        [JsonProperty("ApellidoCliente")]
        public String ApellidoCliente { get; set; }

        [JsonProperty("EmailCliente")]
        public String EmailCliente { get; set; }

        [JsonProperty("Direccion")]
        public String Direccion { get; set; }

        [JsonProperty("Ciudad")]
        public String Ciudad { get; set; }

        [JsonProperty("Dni")]
        public string Dni { get; set; }

        [JsonProperty("Telefono")]
        public int Telefono { get; set; }
    }
}
