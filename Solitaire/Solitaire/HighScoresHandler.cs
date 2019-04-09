using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OlioSolitaire3000
{
    /// <summary>
    /// High Socre Class, stores single high score
    /// </summary>
    class RealHighScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public RealHighScore(string name, string score)
        {
            Name = name;
            Score = int.Parse(score);
        }
        public override string ToString()
        {
            return Name + "," + Score.ToString() + ",";
        }
    }
    /// <summary>
    /// Handler for loading and saving scores
    /// </summary>
    class HighScoresHandler
    {
        // ObservableCollection which is bound to UI
        public ObservableCollection<RealHighScore> scoret;
        // list to manipulate scores during saving
        private List<RealHighScore> tempscoret;
        public HighScoresHandler()
        {
            scoret = new ObservableCollection<RealHighScore>();
            tempscoret = new List<RealHighScore>();
        }
        /// <summary>
        /// Method for loading scores from file to the ObservableCollection
        /// </summary>
        /// <returns>ObservableCollection of the saved scores</returns>
        public ObservableCollection<RealHighScore> LoadScores()
        {
            try
            {
                // create file if not already exists
                if (!(File.Exists("highscores.txt")))
                {
                    File.Create("highscores.txt").Dispose();
                }
                string line;

                System.IO.StreamReader file =
                    new System.IO.StreamReader("highscores.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    scoret.Add(new RealHighScore(words[0], words[1]));
                }
                file.Close();
                return scoret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return scoret;
            }
        }
        /// <summary>
        /// Method for saving a new scores
        /// </summary>
        /// <param name="highscore">Score that needs to be saved</param>
        public void SaveScores(RealHighScore highscore)
        {
            // first load existing scores
            LoadScores();
            tempscoret.Clear();
            // if file empty just add score
            if (scoret.Count == 0)
            {
                tempscoret.Add(highscore);
            }
            // if not empty
            else if (scoret.Count > 0)
            {
                // copy scoret ObservableCollection to List tempscoret
                tempscoret = scoret.ToList();
                try
                {
                    // boolean to determine if the score is in top 10
                    bool CanAdd = false;
                    // loop through all scores
                    for (int i = 0; i < tempscoret.Count(); i++)
                    {
                        // if the new score is same or larger than some score in list
                        if (highscore.Score >= scoret.ElementAt(i).Score)
                        {
                            // then allow adding it
                            CanAdd = true;
                        }
                        // if the new score is NOT same or larger than some score in list BUT scores list is not empty
                        else if (!(highscore.Score >= scoret.ElementAt(i).Score) && scoret.Count() >= 0)
                        {
                            // then allow adding it
                            CanAdd = true;
                        }
                    }
                    // if allowed to add, then just add the score
                    if (CanAdd == true)
                    {
                        tempscoret.Add(highscore);
                    }
                    // sort the scores
                    List<RealHighScore> SortedList = tempscoret.OrderByDescending(o => o.Score).ToList();
                    // and add them to a list
                    tempscoret.Clear();
                    foreach (var item in SortedList)
                    {
                        tempscoret.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            // add the cards back to the collection
            scoret.Clear();
            foreach (var item in tempscoret)
            {
                scoret.Add(item);
            }
            // max 10 scores saved!!
            if (scoret.Count >= 10)
            {
                // removes extra items (only top 10 scores saved)
                tempscoret.RemoveRange(10, tempscoret.Count - 10);
                // add the cards back to the collection
                scoret.Clear();
                foreach (var item in tempscoret)
                {
                    scoret.Add(item);
                }
            }
            // saving to file
            try
            {
                // create file if not already exists
                if (!(File.Exists("highscores.txt")))
                {
                    File.Create("highscores.txt").Dispose();
                }
                System.IO.StreamWriter file =
                        new System.IO.StreamWriter("highscores.txt", false); // first filename, then set to overwite instead of appending to file
                foreach (var item in scoret)
                {
                    try
                    {
                        file.WriteLine(item.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
