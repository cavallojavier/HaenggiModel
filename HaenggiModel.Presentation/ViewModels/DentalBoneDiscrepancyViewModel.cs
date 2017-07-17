using System;
using HaenggiModel.Model;
using HaenggiModel.Model.Extensions;

namespace HaenggiModel.Presentation.ViewModels
{
    public class DentalBoneDiscrepancyViewModel : ViewModelBase
    {
        private readonly DentalBoneDiscrepancy model;

        public DentalBoneDiscrepancyViewModel()
        {
            model = new DentalBoneDiscrepancy();
        }

        public DentalBoneDiscrepancyViewModel(DentalBoneDiscrepancy modelTotal)
        {
            this.model = modelTotal;
        }

        public string TxtDiscSuperior
        {
            get { return model.Superior.ResultToString(); }
            set
            {
                model.Superior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtDiscSuperior");
            }
        }

        public string TxtDiscInferior
        {
            get { return model.Inferior.ResultToString(); }
            set
            {
                model.Inferior = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtDiscInferior");
            }
        }

        public string TxtDiscAnteroSup
        {
            get { return model.SuperiorAntero.ResultToString(); }
            set
            {
                model.SuperiorAntero = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtDiscAnteroSup");
            }
        }

        public string TxtDiscAnteroInf
        {
            get { return model.InferiorAntero.ResultToString(); }
            set
            {
                model.InferiorAntero = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtDiscAnteroInf");
            }
        }

        public string TxtDiscIncisivesInf
        {
            get { return model.InferiorIncisives.ResultToString(); }
            set
            {
                model.InferiorIncisives = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("TxtDiscIncisivesInf");
            }
        }
    }
}
