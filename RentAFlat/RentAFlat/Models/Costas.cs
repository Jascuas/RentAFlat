using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAFlat.Models
{
    public class Costas
    {
        [JsonProperty("Cod_Provincia")]
        public int Cod_Provincia { get; set; }

        [JsonProperty("NombreProvincia")]
        public String NombreProvincia { get; set; }

        [JsonProperty("Foto")]
        public byte[] Foto { get; set; }
    }
}
