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

        private ObservableCollection<Developer> _Developers;
        public ObservableCollection<Developer> Developers
        {
            get => _Developers ?? (_Developers = new ObservableCollection<Developer>(Global.DB.Developers));
            set
            {
                _Developers = value;
                OnPropertyChanged();
            }
        }

        private Developer _SelectedDeveloper;
        public Developer SelectedDeveloper
        {
            get => _SelectedDeveloper ?? (_SelectedDeveloper = new Developer());
            set
            {
                _SelectedDeveloper = value;
                OnPropertyChanged();
            }
        }
    }
}
