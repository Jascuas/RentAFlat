using System;
using System.Collections.Generic;
using System.Text;

namespace RentAFlat.Models
{
    public class BusquedaModel
    {
        public BusquedaModel()
        {
        }


        public int TiposViviendaSelectedValue { get; set; }
        public int CostasSelectedValue { get; set; }
        public int NumeroBaniosSelectedValue { get; set; }
        public int NumeroHabitacionesSelectedValue { get; set; }
        public int Cod_Casa { get; set; }
        public int Cod_Cliente { get; set; }
    }
}
