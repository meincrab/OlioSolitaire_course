using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace OlioSolitaire3000
{
    /// <summary>
    /// Interaction logic for playfield.xaml
    /// </summary>
    public partial class playfield : UserControl, ISwitchable
    {
        // used to track win condition
        bool home1isFull = false;
        bool home2isFull = false;
        bool home3isFull = false;
        bool home4isFull = false;
        // used to determine if timer has been started
        bool timerIsStarted = false;
        // invisible temp deck for moving cards
        Deck movedCards = new Deck(true);
        int fromdecknum = 0;
        int index = -1;
        Card tempCard = new Card(0, "Black", 0, true);
        Deck[] dealCards;
        //create full stack
        Deck pakka = new Deck(false);
        //create turnover stack
        Deck flipdeck = new Deck(true);
        //create home decks
        Deck home1 = new Deck(true);
        Deck home2 = new Deck(true);
        Deck home3 = new Deck(true);
        Deck home4 = new Deck(true);
        //create playfield decks
        Deck plfld1 = new Deck(true);
        Deck plfld2 = new Deck(true);
        Deck plfld3 = new Deck(true);
        Deck plfld4 = new Deck(true);
        Deck plfld5 = new Deck(true);
        Deck plfld6 = new Deck(true);
        Deck plfld7 = new Deck(true);
     

        public playfield()
        {
            InitializeComponent();
            NewGame();
            Time();
        }
        public void NewGame()
        {
            // BIND to UI
            CurrentHand.ItemsSource = movedCards.GetDeck(index, 0);
            turnOver.ItemsSource = flipdeck.GetDeck(index, 0);
            pakkaDeck.ItemsSource = pakka.GetDeck(index, 0);
            homedeck1.ItemsSource = home1.GetDeck(index, 0);
            homedeck2.ItemsSource = home2.GetDeck(index, 0);
            homedeck3.ItemsSource = home3.GetDeck(index, 0);
            homedeck4.ItemsSource = home4.GetDeck(index, 0);
            playfieldDeck1.ItemsSource = plfld1.GetDeck(index, 0);
            playfieldDeck2.ItemsSource = plfld2.GetDeck(index, 0);
            playfieldDeck3.ItemsSource = plfld3.GetDeck(index, 0);
            playfieldDeck4.ItemsSource = plfld4.GetDeck(index, 0);
            playfieldDeck5.ItemsSource = plfld5.GetDeck(index, 0);
            playfieldDeck6.ItemsSource = plfld6.GetDeck(index, 0);
            playfieldDeck7.ItemsSource = plfld7.GetDeck(index, 0);
            // deal cards
            dealCards = new Deck[7] { plfld1, plfld2, plfld3, plfld4, plfld5, plfld6, plfld7 };
            for (int i = 0,j = -1; i <= 6; i++)
            {
                dealCards[i].DealCard(pakka.GetCard(fromdecknum));
                pakka.RemoveLastCard();
                if (i == 6)
                {
                    j++;
                    i = j;
                }
            }
        }
        // used for viewswither
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region playcontrols
        // playcontrols
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new mainmenu());
        }
        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            // check if this causes extra memory usage? or not idk.
            Switcher.Switch(new playfield());
        }
        #endregion
        #region cardhitboxes
        private void hitBoxStack_Click(object sender, RoutedEventArgs e)
        {
            // check if movedcards is not empty
            if (movedCards.IsEmpty() == false)
            {
                // then reverse move if it is not
                ReverseMove(index, fromdecknum);
            }
            else
            {
                // move cards from stack to turnoverstack
                StartMove(index, 0);
                MoveCard(0, 1);
            }
        }
        private void ButtonStack_Click(object sender, RoutedEventArgs e)
        {
            // check if movedcards is not empty
            if (movedCards.IsEmpty() == false)
            {
                // then reverse move if it is not
                ReverseMove(index, fromdecknum);
            }
            else
            {
                // flip the turnover deck back to main deck
                pakka.SetDeck(flipdeck.GetDeck(index, 0), 0);
                flipdeck.ClearDeck();
            }
        }
        private void hitBoxTurnOver_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty())
            {
                StartMove(index, 1);
            }
            else
            {
                MoveCard(fromdecknum, 1);
            }
        }
        private void ButtonTurnOver_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty() == false)
            {
                MoveCard(fromdecknum, 1);
            }
        }
        #region home deck hitboxes
        private void hitBoxHome1_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty())
            {
                StartMove(index, 2);
            }
            else
            {
                MoveCard(fromdecknum, 2);
            }
        }
        private void ButtonHome1_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty() == false)
            {
                MoveCard(fromdecknum, 2);
            }
        }
        private void hitBoxHome2_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty())
            {
                StartMove(index, 3);
            }
            else
            {
                MoveCard(fromdecknum, 3);
            }
        }
        private void ButtonHome2_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty() == false)
            {
                MoveCard(fromdecknum, 3);
            }
        }
        private void hitBoxHome3_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty())
            {
                StartMove(index, 4);
            }
            else
            {
                MoveCard(fromdecknum, 4);
            }
        }
        private void ButtonHome3_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty() == false)
            {
                MoveCard(fromdecknum, 4);
            }
        }
        private void hitBoxHome4_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty())
            {
                StartMove(index, 5);
            }
            else
            {
                MoveCard(fromdecknum, 5);
            }
        }
        private void ButtonHome4_Click(object sender, RoutedEventArgs e)
        {
            if (movedCards.IsEmpty() == false)
            {
                MoveCard(fromdecknum, 5);
            }
        }
        #endregion
        #region playfield hitboxes
        private void hitBoxPlayfield1_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld1.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld1.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 6);
                }
                else
                {
                    MoveCard(fromdecknum, 6);
                }

            }
        }
        private void ButtonPlfld1_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 6);
        }
        private void hitBoxPlayfield2_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld2.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld2.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 7);
                }
                else
                {
                    MoveCard(fromdecknum, 7);
                }

            }
        }
        private void ButtonPlfld2_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 7);
        }
        private void hitBoxPlayfield3_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld3.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld3.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 8);
                }
                else
                {
                    MoveCard(fromdecknum, 8);
                }

            }
        }
        private void ButtonPlfld3_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 8);
        }
        private void hitBoxPlayfield4_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld4.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld4.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 9);
                }
                else
                {
                    MoveCard(fromdecknum, 9);
                }

            }

        }
        private void ButtonPlfld4_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 9);
        }
        private void hitBoxPlayfield5_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld5.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld5.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 10);
                }
                else
                {
                    MoveCard(fromdecknum, 10);
                }

            }
        }
        private void ButtonPlfld5_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 10);
        }
        private void hitBoxPlayfield6_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld6.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld6.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 11);
                }
                else
                {
                    MoveCard(fromdecknum, 11);
                }

            }
        }
        private void ButtonPlfld6_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 11);
        }
        private void hitBoxPlayfield7_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (FrameworkElement)e.OriginalSource;
            Card varCard = ((Card)tmp.DataContext);
            index = plfld7.GetIndex(varCard);
            if (varCard.IsFaceDown == true && movedCards.IsEmpty())
            {
                plfld7.FaceUpLastCard();
            }
            else
            {
                if (movedCards.IsEmpty())
                {
                    StartMove(index, 12);
                }
                else
                {
                    MoveCard(fromdecknum, 12);
                }

            }
        }
        private void ButtonPlfld7_Click(object sender, RoutedEventArgs e)
        {
            MoveCard(fromdecknum, 12);
        }
        #endregion
        #endregion
        /// <summary>
        /// Starting move, copies cards to tempcards
        /// </summary>
        /// <param name="card">Card which is clicked</param>
        /// <param name="fromdeck">int of deck where the cards are from</param>
        public void StartMove(int index, int fromdeck)
        {
            if (timerIsStarted == false)
            {
                RealTime.Start();
                BurningPoints.Start();
                timerIsStarted = true;
            }
            if (movedCards.IsEmpty() == true)
            {
                if (fromdeck == 0)
                {
                    movedCards.SetCard(pakka.GetCard(fromdeck), 14);
                    pakka.RemoveLastCard();
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 1)
                {
                    movedCards.SetCard(flipdeck.GetCard(fromdeck), 14);
                    flipdeck.RemoveLastCard();
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 2)
                {
                    movedCards.SetCard(home1.GetCard(fromdeck), 14);
                    home1.RemoveLastCard();
                    fromdecknum = fromdeck;
                    if (home1isFull == true)
                    {
                        home1isFull = false;
                    }
                }
                else if (fromdeck == 3)
                {
                    movedCards.SetCard(home2.GetCard(fromdeck), 14);
                    home2.RemoveLastCard();
                    fromdecknum = fromdeck;
                    if (home2isFull == true)
                    {
                        home2isFull = false;
                    }
                }
                else if (fromdeck == 4)
                {
                    movedCards.SetCard(home3.GetCard(fromdeck), 14);
                    home3.RemoveLastCard();
                    fromdecknum = fromdeck;
                    if (home3isFull == true)
                    {
                        home3isFull = false;
                    }
                }
                else if (fromdeck == 5)
                {
                    movedCards.SetCard(home4.GetCard(fromdeck), 14);
                    home4.RemoveLastCard();
                    fromdecknum = fromdeck;
                    if (home4isFull == true)
                    {
                        home4isFull = false;
                    }
                }
                // playfield decks
                else if (fromdeck == 6)
                {
                    movedCards.SetDeck(plfld1.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 7)
                {
                    movedCards.SetDeck(plfld2.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 8)
                {
                    movedCards.SetDeck(plfld3.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 9)
                {
                    movedCards.SetDeck(plfld4.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 10)
                {
                    movedCards.SetDeck(plfld5.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 11)
                {
                    movedCards.SetDeck(plfld6.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else if (fromdeck == 12)
                {
                    movedCards.SetDeck(plfld7.GetDeck(index, fromdeck), 14);
                    fromdecknum = fromdeck;
                }
                else
                {
                    fromdecknum = fromdeck;
                    ReverseMove(index, fromdeck);
                }
                fromdecknum = fromdeck;
            }
            else
            {
                fromdecknum = fromdeck;
                ReverseMove(index, fromdeck);
            }
        }
        /// <summary>
        /// Card Moving logic
        /// </summary>
        /// <param name="todeck">Number of deck the Card is moved to; 2-5 upper right homedecks, 6-12 playfield decks</param>
        public void MoveCard(int fromdeck, int todeck)
        {
            if (todeck == 1)
            {
                if (fromdeck == 0)
                {
                    if (flipdeck.SetCard(movedCards.GetCard(14), todeck) == true)
                    {
                        movedCards.ClearDeck();
                    }
                    else
                    {
                        ReverseMove(index, fromdeck);
                    }
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 2)
            {
                if (home1.SetCard(movedCards.GetCard(14), todeck) == true)
                {
                    movedCards.ClearDeck();
                    // win condition setting
                    if (home1.GetCard(2).Nro == 12)
                    {
                        home1isFull = true;
                    }
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 3)
            {
                if (home2.SetCard(movedCards.GetCard(14), todeck) == true)
                {
                    movedCards.ClearDeck();
                    // win condition setting
                    if (home2.GetCard(2).Nro == 12)
                    {
                        home2isFull = true;
                    }
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 4)
            {
                if (home3.SetCard(movedCards.GetCard(14), todeck) == true)
                {
                    movedCards.ClearDeck();
                    // win condition setting
                    if (home3.GetCard(2).Nro == 12)
                    {
                        home3isFull = true;
                    }
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 5)
            {
                if (home4.SetCard(movedCards.GetCard(14), todeck) == true)
                {
                    movedCards.ClearDeck();
                    // win condition setting
                    if (home4.GetCard(2).Nro == 12)
                    {
                        home4isFull = true;
                    }
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 6)
            {
                if (plfld1.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 7)
            {
                if (plfld2.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 8)
            {
                if (plfld3.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 9)
            {
                if (plfld4.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 10)
            {
                if (plfld5.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 11)
            {
                if (plfld6.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else if (todeck == 12)
            {
                if (plfld7.SetDeck(movedCards.GetDeck(index, 14), todeck) == true)
                {
                    movedCards.ClearDeck();
                }
                else
                {
                    ReverseMove(index, fromdeck);
                }
            }
            else
            {
                ReverseMove(index, fromdeck);
            }
        }
        public void ReverseMove(int index, int fromdeck)
        {
            if (fromdeck == 1)
            {
                flipdeck.SetCard(movedCards.GetCard(14), fromdeck);
            }
            else if (fromdeck == 2)
            {
                home1.SetCard(movedCards.GetCard(14), fromdeck);
            }
            else if (fromdeck == 3)
            {
                home2.SetCard(movedCards.GetCard(14), fromdeck);
            }
            else if (fromdeck == 4)
            {
                home3.SetCard(movedCards.GetCard(14), fromdeck);
            }
            else if (fromdeck == 5)
            {
                home4.SetCard(movedCards.GetCard(14), fromdeck);
            }
            else if (fromdeck == 6)
            {
                plfld1.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            else if (fromdeck == 7)
            {
                plfld2.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            else if (fromdeck == 8)
            {
                plfld3.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            else if (fromdeck == 9)
            {
                plfld4.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            else if (fromdeck == 10)
            {
                plfld5.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            else if (fromdeck == 11)
            {
                plfld6.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            else if (fromdeck == 12)
            {
                plfld7.SetDeck(movedCards.GetDeck(index, 14), 14);
            }
            movedCards.ClearDeck();
        }
        /// <summary>
        /// Check if game is won
        /// </summary>
        /// <returns>True if game is won</returns>
        public bool HasWon()
        {
            if (home1isFull == true && home2isFull == true && home3isFull == true && home4isFull == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Triggered when game is won
        /// </summary>
        public void Victory()
        {
            RealTime.Stop();
            BurningPoints.Start();
            newhighscore newscore = new newhighscore(yourScore);
            newscore.ShowDialog();
            Switcher.Switch(new highscores());
        }

        // **********************************************************************************************************************************************************
        // *********************************************************************************************************************************************************************
        //Timer
        DispatcherTimer RealTime = new DispatcherTimer();
        DispatcherTimer BurningPoints = new DispatcherTimer();
        int startingPoints = 0;
        int burningPoints = 1;     
        int yourScore = 32000;

        public void Time()
        {
            RealTime.Interval = new TimeSpan(0, 0, 0, 1, 0);
            RealTime.Tick += new EventHandler(RealTime_Tick);

            BurningPoints.Interval = new TimeSpan(0, 0, 0, 1, 0); 
            BurningPoints.Tick += new EventHandler(BurningPoints_Tick);
        }

        private void BurningPoints_Tick(object sender, EventArgs e)
        {
            // prevents negative score --Okxa
            if (yourScore <= 0)
            {
                yourScore = 0;
            }
            else
            {
                yourScore -= burningPoints;
            }
            ScoreDisplayBlock.Text = yourScore.ToString();
        }

        private void RealTime_Tick(object sender, EventArgs e)
        {
            TimeDisplayBlock.Text = startingPoints++.ToString();
            if (HasWon() == true)
            {
                Victory();
            }
        }
        // debug win button
        private void TimerOff_Click(object sender, RoutedEventArgs e)
        {
            Card pataking = new Card(0, "Black", 12, true);
            Card ristiking = new Card(1, "Black", 12, true);
            Card ruutuking = new Card(2, "Red", 12, true);
            Card herttaking = new Card(3, "Red", 12, true);
            Card pataqueen = new Card(0, "Black", 11, true);
            Card ristiqueen = new Card(1, "Black", 11, true);
            Card ruutuqueen = new Card(2, "Red", 11, true);
            Card herttaqueen = new Card(3, "Red", 11, true);
            home1.SetCard(pataqueen, 14);
            home2.SetCard(ristiqueen, 14);
            home3.SetCard(ruutuqueen, 14);
            home4.SetCard(herttaqueen, 14);
            plfld1.SetCard(pataking, 14);
            plfld2.SetCard(ristiking, 14);
            plfld3.SetCard(ruutuking, 14);
            plfld4.SetCard(herttaking, 14);
        }
    }
}
