using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{

    /// <summary>
    /// 
    /// </summary>
    public class CardPile
    {

        private List<Card> _pile = new List<Card>();
        private static Random _numberGenerator = new Random();

       


        /// <summary>
        ///  Default constructor
        /// </summary>
        /// <param name="Default"></param>
        public CardPile(bool Default = false)
        {
            if (Default)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (FaceValue facevalue in Enum.GetValues(typeof(FaceValue)))
                    {
                        _pile.Add(new Card(suit, facevalue));
                    }
                }
            }
        }


        /// <summary>
        /// Adds the given Card to the CardPile 
        /// </summary>
        /// <param name="Card"></param>
        public void AddCard(Card Card)
        {
            _pile.Add(Card);
        }


        /// <summary>
        /// Returns the number of Cards in the CardPile 
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return _pile.Count;
        }



        /// <summary>
        /// Returns the Card at the last position of the CardPile (does not remove it) 
        /// </summary>
        /// <returns></returns>
        public Card GetLastCardInPile()
        {
            return _pile[_pile.Count - 1];
        }



        /// <summary>
        /// reorder the cardpile ramdonly
        /// </summary>
        public void ShufflePile()/// 
        {
            for (int i = 0; i < _pile.Count * 100; ++i)
            {
                int a = _numberGenerator.Next(_pile.Count);
                int b = _numberGenerator.Next(_pile.Count);
                Card temp = _pile[a];
                _pile[a] = _pile[b];
                _pile[b] = temp;

            }
        }



        /// <summary>
        /// Returns the next Card from the CardPile and removes it from the CardPile 
        /// </summary>
        /// <returns></returns>
        public Card DealOneCard()
        {
            Card card = _pile[0];
            _pile.RemoveAt(0); //remove one card
            return card; // a new cardpile(has lost one card)
        }



        /// <summary>
        /// Deals the number of Cards specified by the parameter, removing them and returning them as a List of Cards
        /// </summary>
        /// <param name="NumsOfCard"> number of Cards </param> 
        /// <returns></returns>
        public List<Card> DealCards(int NumsOfCard)
        {
            List<Card> removedPile = new List<Card>();
            Card card;
            for (int i = 0; i < NumsOfCard; i++)
            {
                card = _pile[0];
                _pile.RemoveAt(0); //remove one card from  _pile
                removedPile.Add(card); //add the removeded card to new card pile
            }
            return removedPile;
        }

        public void Reverse()
        {
            _pile.Reverse();
        }
    }
}
