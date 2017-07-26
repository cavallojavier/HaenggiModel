using System;
using System.Globalization;
using System.Threading;
using HaenggiModel.Model;
using HaenggiModel.Model.Extensions;
using HaenggiModel.Presentation.Properties;

namespace HaenggiModel.Presentation.ViewModels
{
    public class BoltonViewModel : ViewModelBase
    {
        private readonly BoltonTotal modelTotal;
        private readonly BoltonPreviousRelation modelPrevious;

        public BoltonViewModel()
        {
            modelTotal = new BoltonTotal();
            modelPrevious = new BoltonPreviousRelation();
        }

        public BoltonViewModel(BoltonTotal modelTotal, BoltonPreviousRelation modelPrevious)
        {
            this.modelTotal = modelTotal;
            this.modelPrevious = modelPrevious;
        }

        public string LblBoltonTotal
        {
            get { return GetBoltonExcessLabel(modelTotal.IsSuperiorExcess); }
            set
            {
                RaisePropertyChanged("LblBoltonTotal");
            }
        }

        public string LblBoltonAnterior
        {
            get { return GetBoltonExcessLabel(modelPrevious.IsSuperiorExcess); }
            set
            {
                RaisePropertyChanged("LblBoltonAnterior");
            }
        }

        public string TxtBoltonTotal
        {
            get { return GetBoltonResult(modelTotal.IsSuperiorExcess, modelTotal.SuperiorExcess, modelTotal.InferiorExcess).ResultToString(); }
            set
            {
                if (modelTotal.IsSuperiorExcess)
                {
                    modelTotal.SuperiorExcess = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                }
                else
                {
                    modelTotal.InferiorExcess = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                }

                RaisePropertyChanged("TxtBoltonTotal");
            }
        }

        public string TxtPreviousBolton
        {
            get { return GetBoltonResult(modelPrevious.IsSuperiorExcess, modelPrevious.SuperiorExcess, modelPrevious.InferiorExcess).ResultToString(); }
            set
            {
                if (modelPrevious.IsSuperiorExcess)
                {
                    modelPrevious.SuperiorExcess = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                }
                else
                {
                    modelPrevious.InferiorExcess = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                }

                RaisePropertyChanged("TxtPreviousBolton");
            }
        }

        private string GetBoltonExcessLabel(bool isSuperior)
        {
            return isSuperior ? Resources.SuperiorExcess : Resources.InferiorExcess;
        }

        private decimal? GetBoltonResult(bool isSuperior, decimal? superior, decimal? inferior)
        {
            return isSuperior ? superior : inferior;
        }
    }
}
