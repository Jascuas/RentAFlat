using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RentAFlat.Converters
{
    public class ConvertidorGaraje : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool valor = bool.Parse(value.ToString());
                if (valor == true)
                {

                    return "Sí";
                }
                else
                {

                    return "No";
                }
            }
            else
            {
                return "No";
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
