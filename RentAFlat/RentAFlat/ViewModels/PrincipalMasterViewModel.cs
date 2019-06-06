using RentAFlat.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RentAFlat.ViewModels
{
    public class PrincipalMasterViewModel : ViewModelBase
    {
        private Boolean _selected;

        public Boolean Selected
        {
            get { return this._selected; }
            set { this._selected = value; OnPropertyChanged("BackGroudColor"); }
        }

        public Color BackgroundColor
        {
            get
            {
                if (Selected)
                    return Color.DeepSkyBlue;
                else
                    return Color.DarkBlue;
            }
        }
    }
}
