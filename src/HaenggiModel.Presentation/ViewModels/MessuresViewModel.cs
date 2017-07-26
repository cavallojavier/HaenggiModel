using System;
using HaenggiModel.Model;
using HaenggiModel.Model.Extensions;

namespace HaenggiModel.Presentation.ViewModels
{
    public class MessuresViewModel : ViewModelBase
    {
        public readonly RoothCalculationEntity roothModel;
        public readonly MouthCalculationEntity mouseModel;
        public readonly PatientInformation patientModel;

        public MessuresViewModel()
        {
            roothModel = new RoothCalculationEntity();
            mouseModel = new MouthCalculationEntity();
            patientModel = new PatientInformation() { DateMessure = DateTime.Now };
        }

        public MessuresViewModel(RoothCalculationEntity roothModel, MouthCalculationEntity mouseModel, PatientInformation patientModel)
        {
            this.roothModel = roothModel;
            this.mouseModel = mouseModel;
            this.patientModel = patientModel;
            this.patientModel.DateMessure = patientModel.DateMessure.Equals(DateTime.MinValue) ? DateTime.Now : patientModel.DateMessure;
        }

        #region Patient

        public string TxtPatient
        {
            get { return patientModel.PatientName; }
            set
            {
                patientModel.PatientName = value;
                RaisePropertyChanged("TxtPatient");
            }
        }

        public string TxtHCNumber
        {
            get { return patientModel.HcNumber; }
            set
            {
                patientModel.HcNumber = value;
                RaisePropertyChanged("TxtHCNumber");
            }
        }

        public string TxtProfesional
        {
            get { return patientModel.UserName; }
            set
            {
                patientModel.UserName = value;
                RaisePropertyChanged("TxtProfesional");
            }
        }

        public DateTime DpDateMessure
        {
            get { return patientModel.DateMessure.Equals(DateTime.MinValue) ? DateTime.Now : patientModel.DateMessure; }
            set
            {
                patientModel.DateMessure = value.Equals(DateTime.MinValue) ? DateTime.Now : value;

                    //string.IsNullOrEmpty(value) ? DateTime.Now : Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                RaisePropertyChanged("DpDateMessure");
            }
        }

        #endregion

        #region Mouth

        public string TxtRightSuperiorPremolar
        {
            get { return mouseModel.RightSuperiorPremolar.ResultToString(); }
            set
            {
                mouseModel.RightSuperiorPremolar = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtRightSuperiorPremolar");
            }
        }

        public string TxtRightSuperiorCanine
        {
            get { return mouseModel.RightSuperiorCanine.ResultToString(); }
            set
            {
                mouseModel.RightSuperiorCanine = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtRightSuperiorCanine");
            }
        }

        public string TxtRightSuperiorIncisive
        {
            get { return mouseModel.RightSuperiorIncisive.ResultToString(); }
            set
            {
                mouseModel.RightSuperiorIncisive = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtRightSuperiorIncisive");
            }
        }

        public string TxtLeftSuperiorIncisive
        {
            get { return mouseModel.LeftSuperiorIncisive.ResultToString(); }
            set
            {
                mouseModel.LeftSuperiorIncisive = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtLeftSuperiorIncisive");
            }
        }

        public string TxtLeftSuperiorCanine
        {
            get { return mouseModel.LeftSuperiorCanine.ResultToString(); }
            set
            {
                mouseModel.LeftSuperiorCanine = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtLeftSuperiorCanine");
            }
        }

        public string TxtLeftSuperiorPremolar
        {
            get { return mouseModel.LeftSuperiorPremolar.ResultToString(); }
            set
            {
                mouseModel.LeftSuperiorPremolar = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtLeftSuperiorPremolar");
            }
        }

        public string TxtRightInferiorPremolar
        {
            get { return mouseModel.RightInferiorPremolar.ResultToString(); }
            set
            {
                mouseModel.RightInferiorPremolar = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtRightInferiorPremolar");
            }
        }

        public string TxtRightInferiorCanine
        {
            get { return mouseModel.RightInferiorCanine.ResultToString(); }
            set
            {
                mouseModel.RightInferiorCanine = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtRightInferiorCanine");
            }
        }

        public string TxtRightInferiorIncisive
        {
            get { return mouseModel.RightInferiorIncisive.ResultToString(); }
            set
            {
                mouseModel.RightInferiorIncisive = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtRightInferiorIncisive");
            }
        }

        public string TxtLeftInferiorIncisive
        {
            get { return mouseModel.LeftInferiorIncisive.ResultToString(); }
            set
            {
                mouseModel.LeftInferiorIncisive = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtLeftInferiorIncisive");
            }
        }

        public string TxtLeftInferiorCanine
        {
            get { return mouseModel.LeftInferiorCanine.ResultToString(); }
            set
            {
                mouseModel.LeftInferiorCanine = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtLeftInferiorCanine");
            }
        }

        public string TxtLeftInferiorPremolar
        {
            get { return mouseModel.LeftInferiorPremolar.ResultToString(); }
            set
            {
                mouseModel.LeftInferiorPremolar = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txtLeftInferiorPremolar");
            }
        }

        #endregion

        #region Rooth

        public string Txt17
        {
            get { return roothModel.Tooth17.ResultToString(); }
            set
            {
                roothModel.Tooth17 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt17");
            }
        }

        public string Txt16
        {
            get { return roothModel.Tooth16.ResultToString(); }
            set
            {
                roothModel.Tooth16 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt16");
            }
        }

        public string Txt15
        {
            get { return roothModel.Tooth15.ResultToString(); }
            set
            {
                roothModel.Tooth15 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt15");
            }
        }

        public string Txt14
        {
            get { return roothModel.Tooth14.ResultToString(); }
            set
            {
                roothModel.Tooth14 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt14");
            }
        }

        public string Txt13
        {
            get { return roothModel.Tooth13.ResultToString(); }
            set
            {
                roothModel.Tooth13 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt13");
            }
        }

        public string Txt12
        {
            get { return roothModel.Tooth12.ResultToString(); }
            set
            {
                roothModel.Tooth12 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt12");
            }
        }

        public string Txt11
        {
            get { return roothModel.Tooth11.ResultToString(); }
            set
            {
                roothModel.Tooth11 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt11");
            }
        }

        public string Txt27
        {
            get { return roothModel.Tooth27.ResultToString(); }
            set
            {
                roothModel.Tooth27 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt27");
            }
        }

        public string Txt26
        {
            get { return roothModel.Tooth26.ResultToString(); }
            set
            {
                roothModel.Tooth26 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt26");
            }
        }

        public string Txt25
        {
            get { return roothModel.Tooth25.ResultToString(); }
            set
            {
                roothModel.Tooth25 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt25");
            }
        }

        public string Txt24
        {
            get { return roothModel.Tooth24.ResultToString(); }
            set
            {
                roothModel.Tooth24 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt24");
            }
        }

        public string Txt23
        {
            get { return roothModel.Tooth23.ResultToString(); }
            set
            {
                roothModel.Tooth23 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt23");
            }
        }

        public string Txt22
        {
            get { return roothModel.Tooth22.ResultToString(); }
            set
            {
                roothModel.Tooth22 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt22");
            }
        }

        public string Txt21
        {
            get { return roothModel.Tooth21.ResultToString(); }
            set
            {
                roothModel.Tooth21 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt21");
            }
        }

        public string Txt47
        {
            get { return roothModel.Tooth47.ResultToString(); }
            set
            {
                roothModel.Tooth47 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt47");
            }
        }

        public string Txt46
        {
            get { return roothModel.Tooth46.ResultToString(); }
            set
            {
                roothModel.Tooth46 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt46");
            }
        }

        public string Txt45
        {
            get { return roothModel.Tooth45.ResultToString(); }
            set
            {
                roothModel.Tooth45 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt45");
            }
        }

        public string Txt44
        {
            get { return roothModel.Tooth44.ResultToString(); }
            set
            {
                roothModel.Tooth44 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt44");
            }
        }

        public string Txt43
        {
            get { return roothModel.Tooth43.ResultToString(); }
            set
            {
                roothModel.Tooth43 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt43");
            }
        }

        public string Txt42
        {
            get { return roothModel.Tooth42.ResultToString(); }
            set
            {
                roothModel.Tooth42 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt42");
            }
        }

        public string Txt41
        {
            get { return roothModel.Tooth41.ResultToString(); }
            set
            {
                roothModel.Tooth41 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt41");
            }
        }

        public string Txt37
        {
            get { return roothModel.Tooth37.ResultToString(); }
            set
            {
                roothModel.Tooth37 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt37");
            }
        }

        public string Txt36
        {
            get { return roothModel.Tooth36.ResultToString(); }
            set
            {
                roothModel.Tooth36 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt36");
            }
        }

        public string Txt35
        {
            get { return roothModel.Tooth35.ResultToString(); }
            set
            {
                roothModel.Tooth35 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt35");
            }
        }

        public string Txt34
        {
            get { return roothModel.Tooth34.ResultToString(); }
            set
            {
                roothModel.Tooth34 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt34");
            }
        }

        public string Txt33
        {
            get { return roothModel.Tooth33.ResultToString(); }
            set
            {
                roothModel.Tooth33 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt33");
            }
        }

        public string Txt32
        {
            get { return roothModel.Tooth32.ResultToString(); }
            set
            {
                roothModel.Tooth32 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt32");
            }
        }

        public string Txt31
        {
            get { return roothModel.Tooth31.ResultToString(); }
            set
            {
                roothModel.Tooth31 = string.IsNullOrEmpty(value) ? null : (decimal?)Convert.ToDecimal(value);
                RaisePropertyChanged("txt31");
            }
        }
        #endregion
    }
}
