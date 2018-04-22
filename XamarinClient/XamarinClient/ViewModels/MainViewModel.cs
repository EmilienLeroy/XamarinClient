using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinClient.Models;
using XamarinClient.Services;

namespace XamarinClient.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> listDevises;
        private WebService api = new WebService();

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
        }

        private async void ActionGetData()
        {
            var result = await api.getAllDevisesAsync();
            this.ListeDevises = new ObservableCollection<Devise>(result);
        }

        



    }
}
