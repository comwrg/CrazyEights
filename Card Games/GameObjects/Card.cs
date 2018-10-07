using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Junlin Yin n9905391
/// Huachen Liu n10073591
/// </summary>
namespace GameObjects
{
    public enum Suit { Clubs, Diamonds, Hearts, Spades }
    public enum FaceValue
    {
        Ace,
        Two, Three, Four, Five, Six, Seven, Eight, Nine,
        Ten, Jack, Queen, King
    }


    /// <summary>
    /// 
    /// </summary>
    public class Card : IEquatable<Card>, IComparable<Card>
    {

        private FaceValue _faceValue;
        private Suit _suit;

        /// <summary>
        /// Constructs a Card with the given Suit and FaceValue 
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="faceValue"></param>
        public Card(Suit? suit, FaceValue faceValue)
        {
            _faceValue = faceValue;
            _suit = suit.Value;
        }

        public Card() {
        }


        /// <summary>
        /// Returns the FaceValue of the Card
        /// </summary>
        /// <returns></returns>
        public FaceValue GetFaceValue()
        {
            return _faceValue;
        }


        /// <summary>
        /// Returns the Suit of the Card 
        /// </summary>
        /// <returns></returns>
        public Suit GetSuit()
        {
            return _suit;
        }


        /// <summary>
        /// Returns true if this Card (from which Equals was called) is equivalent to the given Card (the parameter) 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Card other)
        {
            return (other._faceValue == _faceValue &&
                other._suit == _suit);
            // true && true => same card
            // true && false => different card
            // false && true => different card
            // false && false => different card
        }


        /// <summary>
        /// Returns a number  0 if this Card should be sorted before the given Card; 
        /// returns 0 if this Card occurs in the same position as the given Card; 
        /// returns a number 0 
        ///  if this Card should be sorted after the given Card. https
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Card other)
        {
            if (_suit > other._suit)
            {
                return 1;   // the face value of this card is greater than other card.

            }
            else if (_suit == other._suit)
            {  // the face value of this card is same as the other card. 

                if (_faceValue > other._faceValue)
                {
                    return 1;

                }
                else if (_faceValue == other._faceValue) // these two cards are the same.

                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1; // the face value of this card is less than the other card.
            }
        }


        /// <summary>
        /// Used to obtain a string value representing the card.
        /// String values start with the number or first letter of the FaceValue, 
        /// followed by the first letter of the Suit
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string initialSuit = "";
            string itialfaceValue = "";

            if (_suit == Suit.Clubs)
            {
                initialSuit = "C";
            }
            else if (_suit == Suit.Diamonds)
            {
                initialSuit = "D";
            }
            else if (_suit == Suit.Hearts)
            {
                initialSuit = "H";
            }
            else if (_suit == Suit.Spades)
            {
                initialSuit = "S";
            }
            if (_faceValue == FaceValue.Ace)
            {
                itialfaceValue = "A";
            }
            else if (_faceValue == FaceValue.Two)
            {
                itialfaceValue = "2";
            }
            else if (_faceValue == FaceValue.Three)
            {
                itialfaceValue = "3";
            }
            else if (_faceValue == FaceValue.Four)
            {
                itialfaceValue = "4";
            }
            else if (_faceValue == FaceValue.Five)
            {
                itialfaceValue = "5";
            }
            else if (_faceValue == FaceValue.Six)
            {
                itialfaceValue = "6";
            }
            else if (_faceValue == FaceValue.Seven)
            {
                itialfaceValue = "7";
            }
            else if (_faceValue == FaceValue.Eight)
            {
                itialfaceValue = "8";
            }
            else if (_faceValue == FaceValue.Nine)
            {
                itialfaceValue = "9";
            }
            else if (_faceValue == FaceValue.Ten)
            {
                itialfaceValue = "10";
            }
            else if (_faceValue == FaceValue.Jack)
            {
                itialfaceValue = "J";
            }
            else if (_faceValue == FaceValue.Queen)
            {
                itialfaceValue = "Q";
            }
            else if (_faceValue == FaceValue.King)
            {
                itialfaceValue = "K";
            }

            return itialfaceValue + initialSuit;
            }
        }
}
