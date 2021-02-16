using CyberClub.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberClub.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private ObservableCollection<Game> _Games;
        public ObservableCollection<Game> Games
        {
            get => _Games ?? (_Games = new ObservableCollection<Game>(Global.DB.Games));
            set
            {
                _Games = value;
                OnPropertyChanged();
            }
        }

        private Game _SelectedGame;
        public Game SelectedGame
        {
            get => _SelectedGame ?? (_SelectedGame = new Game());
            set
            {
                _SelectedGame = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Dev> _Devs;
        public ObservableCollection<Dev> Devs
        {
            get => _Devs ?? (_Devs = new ObservableCollection<Dev>(Global.DB.Devs));
            set
            {
                _Devs = value;
                OnPropertyChanged();
            }
        }

        private Dev _SelectedDev;
        public Dev SelectedDev
        {
            get => _SelectedDev ?? (_SelectedDev = new Dev());
            set
            {
                _SelectedDev = value;
                OnPropertyChanged();
            }
        }
    }
}
