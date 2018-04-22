using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using XamarinClient.Models;
using XamarinClient.Services;

namespace XamarinClient.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> listDevises;
        private WebService api = new WebService();
        private string montantEuros;

        public string MontantEuros
        {
            get { return montantEuros; }
            set {
                montantEuros = value;
                RaisePropertyChanged();
            }
        }

        private Devise deviseSelect;

        public Devise DeviseSelect
        {
            get { return deviseSelect; }
            set {
                deviseSelect = value;
                RaisePropertyChanged();
            }
        }


        public ICommand BtnSetConversion { get; private set; }

        public ObservableCollection<Devise> ListeDevises
        {
            get { return listDevises; }
            set
            {
                listDevises = value;
                RaisePropertyChanged();// Pour notifier de la modification de ses données
            }
        }

        public MainViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

        private void ActionSetConversion()
        {
            var result = double.Parse(MontantEuros) * DeviseSelect.Taux;
            MontantEuros = result.ToString();
        }

        private async void ActionGetData()
        {
            var result = await api.getAllDevisesAsync();
            this.ListeDevises = new ObservableCollection<Devise>(result);
        }

        



    }
}
