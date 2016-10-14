namespace AsystentZakupówWPF.ModelWidoku
{
    using Model;
    using System.ComponentModel;
    using System.Windows.Input;

    public class ModelWidoku : INotifyPropertyChanged
    {
        private SumowanieKwot model = new SumowanieKwot(1000);

        public string Suma
        {
            get
            {
                return model.Suma.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nazwaWłasności)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWłasności));
        }

        public bool CzyŁańcuchKwotyJestPoprawny(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            decimal kwota;
            if (!decimal.TryParse(s, out kwota)) return false;
            else return model.CzyKwotaJestPoprawna(kwota);
        }

        private ICommand dodajKwotęCommand;

        public ICommand DodajKwotę
        {
            get
            {
                if (dodajKwotęCommand == null)
                    dodajKwotęCommand = new RelayCommand(
                        (object argument) =>
                        {
                            decimal kwota = decimal.Parse((string)argument);
                            model.Dodaj(kwota);
                            OnPropertyChanged("Suma");
                        },
                        (object argument) =>
                        {
                            return CzyŁańcuchKwotyJestPoprawny((string)argument);
                        }
                     );
                return dodajKwotęCommand;
            }
        }
    }
}
