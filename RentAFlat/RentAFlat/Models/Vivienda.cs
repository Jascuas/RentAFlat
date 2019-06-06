using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAFlat.Models
{
    public class Vivienda
    {
        [JsonProperty("Cod_casa")]
        public int Cod_casa { get; set; }
        [JsonProperty("Cod_Provincia")]
        public int Cod_Provincia { get; set; }
        [JsonProperty("IdCliente")]
        public int IdCliente { get; set; }
        [JsonProperty("Ubicacion")]
        public String Ubicacion { get; set; }
        [JsonProperty("Codigo_Posta")]
        public string Codigo_Posta { get; set; }
        [JsonProperty("Ciudad")]
        public string Ciudad { get; set; }
        [JsonProperty("Num_habitaciones")]
        public int Num_habitaciones { get; set; }
        [JsonProperty("Num_banios")]
        public int Num_banios { get; set; }
        [JsonProperty("Tamanio_vivienda")]
        public int Tamanio_vivienda { get; set; }
        [JsonProperty("Descripcion")]
        public String Descripcion { get; set; }
        [JsonProperty("Garaje")]
        public bool Garaje { get; set; }
        [JsonProperty("Cod_TipoVivienda")]
        public int Cod_TipoVivienda { get; set; }
        [JsonProperty("Descripcion_vivienda")]
        public string Descripcion_vivienda { get; set; }
        public int Current { set; get; }
        public int Total { set; get; }
    }
}