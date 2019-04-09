using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlioSolitaire3000
{
    /// <summary>
    /// Card Class
    /// </summary>
    public class Card
    {
        public int Suit { get; set; }
        public string Colour { get; set; }
        public int Nro { get; set; }
        public bool IsFaceDown { get; set; }
        public string SuitImageSource { get; set; }
        public string NroImageSource { get; set; }
        public Card(int cardsuit, string color, int number, bool isFacedUp)
        {
            Suit = cardsuit;
            Colour = color;
            Nro = number;
            IsFaceDown = isFacedUp;
            SuitImageSource = string.Format("/Resources/Suits/{0}.png", Suit.ToString());
            NroImageSource = string.Format("/Resources/Numbers/{0}.png", Nro.ToString());
        }
        public override string ToString()
        {
            return Suit.ToString() + "," + Colour + "," + Nro.ToString() + "," + IsFaceDown.ToString();
        }
    }
}
