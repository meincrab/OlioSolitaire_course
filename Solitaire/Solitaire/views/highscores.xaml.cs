using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;

namespace OlioSolitaire3000
{
    /// <summary>
    /// Interaction logic for highscores.xaml
    /// </summary>
    public partial class highscores : UserControl, ISwitchable
    {
        HighScoresHandler tulokset = new HighScoresHandler();
        public highscores()
        {
            InitializeComponent();
            ScoreShow.ItemsSource = tulokset.LoadScores();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new mainmenu());
        }
    }
}
