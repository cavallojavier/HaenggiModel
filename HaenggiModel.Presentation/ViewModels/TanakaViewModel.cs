using System;
using HaenggiModel.Model;
using HaenggiModel.Model.Extensions;

namespace HaenggiModel.Presentation.ViewModels
{
    public class TanakaViewModel : ViewModelBase
    {
        private readonly TanakaJohnston model;

        public TanakaViewModel()
        {
            model = new TanakaJohnston();
        }

        public TanakaViewModel(TanakaJohnston tanaka)
        {
            model = tanaka;
        }

        public string TxtInferior
        {
            get { return model.Inferior.ResultToString(); }
            set
            {
                model.Inferior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtInferior");
            }
        }

        public string TxtSuperior
        {
            get { return model.Superior.ResultToString(); }
            set
            {
                model.Superior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtSuperior");
            }
        }
    }
}
