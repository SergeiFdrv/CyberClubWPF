using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CyberClub.Pages
{
    public abstract class AdminEditorPage : Page
    {
        protected static App Global => Application.Current as App;

        protected bool _IsInEditMode;
        public bool IsInEditMode
        {
            get => _IsInEditMode;
            set => SetEditMode(value);
        }
        /// <summary>
        /// Sets the IsInEditMode property
        /// </summary>
        protected abstract void SetEditMode(bool value);

        public abstract void OpenFromTable(object obj);
    }
}
