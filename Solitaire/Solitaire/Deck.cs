using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlioSolitaire3000
{
    /// <summary>
    /// Deck Class
    /// </summary>
    class Deck
    {
        ObservableCollection<Card> kortit;
        ObservableCollection<Card> tempkortit;
        Card tempcard = new Card(0, "Pink", 0, true);
        List<Card> cards;
        // used to create empty decks
        public Deck(bool isempty)
        {
            if (isempty == true)
            {
                kortit = new ObservableCollection<Card>();
                tempkortit = new ObservableCollection<Card>();
                cards = new List<Card>();
            }
            else
            {
                Random rng = new Random();
                // create deck
                // list is used for randomizing
                cards = new List<Card>();
                // observablecollection is for UI binding
                kortit = new ObservableCollection<Card>();
                tempkortit = new ObservableCollection<Card>();
                string vari = "";
                // create cards
                for (int j = 0; j <= 3; j++)
                {
                    for (int i = 0; i <= 12; i++)
                    {
                        if (j <= 1)
                        {
                            vari = "Black";
                        }
                        else if (j >= 2)
                        {
                            vari = "Red";
                        }
                        cards.Add(new Card(j, vari, i, true));
                    }
                }
                // randomize deck
                var randcards = cards.OrderBy(item => rng.Next()).ToList();
                // add cards to collection
                foreach (var item in randcards)
                {
                    kortit.Add(item);
                }
            }
        }
        /// <summary>
        /// Returns the current deck from this
        /// </summary>
        /// <returns>Deck</returns>
        public ObservableCollection<Card> GetDeck(int index, int fromdeck)
        {
            if (fromdeck == 14)
            {
                return kortit;
            }
            else if(index == -1)
            {
                return kortit;
            }
            else if (fromdeck >= 6 && fromdeck <= 12)
            {
                if (kortit.Last().IsFaceDown == true)
                {
                    kortit.Last().IsFaceDown = false;
                    return null;
                }
                else
                {
                    tempkortit.Clear();
                    cards.Clear();
                    bool isValidMove = false;
                    // check if clicked card is faced up
                    if (kortit.ElementAt(index).IsFaceDown == false)
                    {
                        // loop for checks
                        for (int i = index; i <= kortit.IndexOf(kortit.Last()); i++)
                        {
                            // check for if topmost card
                            if (i == kortit.IndexOf(kortit.Last()))
                            {
                                isValidMove = true;
                            }
                            // check for smaller number
                            else if (kortit.ElementAt(i + 1).Nro == kortit.ElementAt(i).Nro + 1)
                            {
                                isValidMove = true;
                            }
                            else
                            {
                                isValidMove = false;
                            }
                            // check for if topmost card
                            if (i == kortit.IndexOf(kortit.Last()))
                            {
                                isValidMove = true;
                            }
                            // check for different colour
                            else if (kortit.ElementAt(i + 1).Colour != kortit.ElementAt(i).Colour)
                            {
                                isValidMove = true;
                            }
                            else
                            {
                                isValidMove = false;
                            }
                        }
                        if (isValidMove == true)
                        {
                            for (int i = index; i <= kortit.IndexOf(kortit.Last()); i++)
                            {
                                tempkortit.Add(kortit.ElementAt(i));
                            }
                            for (int i = kortit.IndexOf(kortit.Last()); i >= index; i--)
                            {
                                kortit.Remove(kortit.ElementAt(i));
                            }
                        }
                        return tempkortit;
                    }
                    else
                    {
                        tempkortit.Clear();
                        return tempkortit;
                    }
                }
            }
            else
            {
                return kortit;
            }
        }
        /// <summary>
        /// Transfer deck to other deck, used for flipping the main deck
        /// </summary>
        /// <param name="deck">Deck to be saved</param>
        /// <param name="todeck">Number of deck to save</param>
        /// <returns>True if move was succesfull</returns>
        public bool SetDeck(ObservableCollection<Card> deck, int todeck)
        {
            cards.Clear();
            foreach (var item in deck)
            {
                cards.Add(item);
            }
            // if todeck is main deck, then reverse
            if (todeck == 0)
            {
                kortit.Clear();
                cards.Reverse();
                foreach (var item in cards)
                {
                    kortit.Add(item);
                }
                return true;
            }
            // used for when moving cards to movedCards or when reversing a move from playfield deck
            else if (todeck == 14)
            {
                foreach (var item in cards)
                {
                    kortit.Add(item);
                }
                return true;
            }
            // check for playfield decks
            else if (todeck >= 6 && todeck <= 12)
            {
                // turn cards over
                foreach (var item in cards)
                {
                    item.IsFaceDown = false;
                }
                // if the deck is empty
                if (kortit.Count() == 0)
                {
                    // check if card is king
                    if (cards.First().Nro == 12)
                    {
                        // add cards
                        foreach (var item in cards)
                        {
                            kortit.Add(item);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                // if not empty but topmost card is faced down
                else if (kortit.Last().IsFaceDown == true)
                {
                    return false;
                }
                // if not empty or face down
                else if (kortit.Last().IsFaceDown == false)
                {
                    // check for one larger number and different colour
                    if (cards.First().Nro + 1 == kortit.Last().Nro && cards.First().Colour != kortit.Last().Colour)
                    {
                        // add cards
                        foreach (var item in cards)
                        {
                            kortit.Add(item);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Clears the current deck
        /// </summary>
        public void ClearDeck()
        {
            kortit.Clear();
        }
        /// <summary>
        /// Checks if the current stack is empty
        /// </summary>
        /// <returns>True if is empty</returns>
        public bool IsEmpty()
        {
            if (kortit.Count() <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the index of the Card in the target deck
        /// </summary>
        /// <param name="card">The Card which index is wanted</param>
        /// <returns>Index of the card OR 0 if deck is empty</returns>
        public int GetIndex(Card card)
        {
            if (kortit.Count() != 0)
            {
                int index = kortit.IndexOf(card);
                return index;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Sets last Card in deck to face up
        /// </summary>
        public void FaceUpLastCard()
        {
            tempcard = kortit.Last();
            kortit.Remove(kortit.Last());
            tempcard.IsFaceDown = false;
            kortit.Add(tempcard);
        }
        /// <summary>
        /// Removes last Card in the deck
        /// </summary>
        public void RemoveLastCard()
        {
            kortit.RemoveAt(kortit.IndexOf(kortit.Last()));
        }
        /// <summary>
        /// Get's a card from a deck
        /// </summary>
        /// <returns>The Card removed from the target deck</returns>
        public Card GetCard(int fromdeck)
        {
            if (fromdeck >= 0 || fromdeck <= 5)
            {
                tempcard = kortit.Last();
                return tempcard;
            }
            if (kortit.Count >= 1)
            {
                tempcard = kortit.First();
                return tempcard;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Places a card in the target deck
        /// </summary>
        /// <param name="card">Card to be placed to the target deck</param>
        /// <param name="todeck">int value of the target deck</param>
        /// <returns>True if card placement was succesfull</returns>
        public bool SetCard(Card card, int todeck)
        {
            // temp deck
            if (todeck == 14)
            {
                kortit.Add(card);
                return true;
            }
            // if todeck is flipdeck, just set the card back
            // to be used when move is invalid
            else if (todeck == 1)
            {
                kortit.Add(card);
                return true;
            }
            // check for home deck placement
            else if (todeck >= 2 && todeck <= 5)
            {
                if (card != null)
                {
                    // check if deck is empty and if the card is not Ace
                    if (kortit.Count() == 0 && card.Nro != 0)
                    {
                        return false;
                    }
                    // check if deck is not empty and if the card is Ace
                    else if (kortit.Count() != 0 && card.Nro == 0)
                    {
                        return false;
                    }
                    // check if deck is empty and if the card is Ace
                    else if (kortit.Count() == 0 && card.Nro == 0)
                    {
                        kortit.Add(card);
                        return true;
                    }
                    // check if card is same colour and on larger number
                    else if (card.Colour == kortit.Last().Colour && card.Nro - 1 == kortit.Last().Nro && card.Suit == kortit.Last().Suit)
                    {
                        kortit.Add(card);
                        return true;
                    }
                    // else do not place card, but return false
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            // else do not place card, but return false
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            string cardstring = "";
            foreach (var item in kortit)
            {
                cardstring = cardstring + item.ToString();
            }
            return cardstring;
        }
        public void DealCard(Card card)
        {
            if (kortit.Count() >= 1)
            {
                kortit.Last().IsFaceDown = true;
            }
            kortit.Add(card);
            kortit.Last().IsFaceDown = false;
        }
    }
}
