using System;
using HaenggiModel.Model;
using HaenggiModel.Model.Extensions;

namespace HaenggiModel.Presentation.ViewModels
{
    public class MoyersViewModel : ViewModelBase
    {
        private readonly Moyers model;

        public MoyersViewModel()
        {
            model = new Moyers();
        }

        public MoyersViewModel(Moyers tanaka)
        {
            model = tanaka;
        }

        public string TxtLeftInferior
        {
            get { return model.LeftInferior.ResultToString(); }
            set
            {
                model.LeftInferior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtLeftInferior");
            }
        }

        public string TxtLeftSuperior
        {
            get { return model.LeftSuperior.ResultToString(); }
            set
            {
                model.LeftSuperior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtLeftSuperior");
            }
        }

        public string TxtRightInferior
        {
            get { return model.RightInferior.ResultToString(); }
            set
            {
                model.RightInferior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtRightInferior");
            }
        }

        public string TxtRightSuperior
        {
            get { return model.RightSuperior.ResultToString(); }
            set
            {
                model.RightSuperior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtRightSuperior");
            }
        }
    }
}
