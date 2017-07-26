using System;
using HaenggiModel.Model;
using HaenggiModel.Model.Extensions;

namespace HaenggiModel.Presentation.ViewModels
{
    public class PontViewModel : ViewModelBase
    {
        private readonly Pont model;

        public PontViewModel()
        {
            model = new Pont();
        }

        public PontViewModel(Pont tanaka)
        {
            model = tanaka;
        }

        public string TxtPont14to16
        {
            get { return ((decimal?)model.Pont14To24).ResultToString(); }
            set
            {
                model.Pont14To24 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtPont14To24");
            }
        }

        public string TxtPont16To26
        {
            get { return ((decimal?)model.Pont16To26).ResultToString(); }
            set
            {
                model.Pont16To26 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtPont16To26");
            }
        }

        public string TxtArchLong
        {
            get { return ((decimal?)model.ArchLong).ResultToString(); }
            set
            {
                model.ArchLong = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtPontArchLong");
            }
        }
    }
}
