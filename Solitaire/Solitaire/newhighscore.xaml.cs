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
using System.Windows.Shapes;

namespace OlioSolitaire3000
{
    /// <summary>
    /// Interaction logic for newhighscore.xaml
    /// </summary>
    public partial class newhighscore : Window
    {
        // create new score
        RealHighScore todaysScore = new RealHighScore("demo", "0");
        // load existing scores from file
        HighScoresHandler tulokset = new HighScoresHandler();
        int pisteet;
        public newhighscore(int score)
        {
            InitializeComponent();
            pisteet = score;
            scoreBox.Text = score.ToString();
        }
        private void btnSaveScore_Click(object sender, RoutedEventArgs e)
        {
            // create current score
            todaysScore.Name = txtbxName.Text;
            todaysScore.Score = pisteet;
            tulokset.SaveScores(todaysScore);
            this.Close();
        }
    }
}
