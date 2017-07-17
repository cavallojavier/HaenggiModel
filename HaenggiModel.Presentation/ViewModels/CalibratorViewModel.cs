namespace HaenggiModel.Presentation.ViewModels
{
    public class CalibrataorViewModel : ViewModelBase
    {
        private string txtSample1;
        private string txtSample2;

        public CalibrataorViewModel()
        {
        }

        public string TxtSample1
        {
            get { return txtSample1; }
            set
            {
                txtSample1 = value;
                RaisePropertyChanged("TxtSample1");
            }
        }

        public string TxtSample2
        {
            get { return txtSample2; }
            set
            {
                txtSample2 = value;
                RaisePropertyChanged("TxtSample2");
            }
        }
    }
}
