using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{

    /// <summary>
    /// 
    /// </summary>
    public class Hand : IEnumerable
    {
        private List<Card> _hand;

        /// <summary>
        /// Construct a new empty Hand 
        /// </summary>
        public Hand()
        {
            _hand = new List<Card>();
        }


        /// <summary>
        /// Construct a new Hand containing the given list of Cards 
        /// </summary>
        /// <param name="Cards"></param> the card in hand
        public Hand(List<Card> Cards)
        {
            _hand = Cards;
        }


        /// <summary>
        /// Return the number of Cards in the Hand 
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return _hand.Count;
        }


        /// <summary>
        /// Return the Card at the given position in the Hand 
        /// </summary>
        /// <param name="cardPos"></param>
        /// <returns></returns>
        public Card GetCard(int cardPos)
        {
            return _hand[cardPos];
        }


        /// <summary>
        /// Add the given Card to the Hand 
        /// </summary>
        /// <param name="Card"></param>
        public void AddCard(Card Card)
        {
            _hand.Add(Card);
        }

        /// <summary>
        /// Return true if the given Card is in the Hand 
        /// </summary>
        /// <param name="Card"></param>
        /// <returns></returns>
        public bool ContainsCard(Card Card)
        {
            return _hand.Contains(Card);
        }


        /// <summary>
        /// Remove the given Card from the Hand, if possible. Return true if successful. 
        /// </summary>
        /// <param name="Card"></param>
        /// <returns></returns>
        public bool RemoveCard(Card Card)
        {
            return _hand.Remove(Card);
        }


        /// <summary>
        /// Remove the Card at the index given by the integer parameter. Return true if successful. 
        /// </summary>
        /// <param name="cardPos"></param>
        /// <returns></returns>
        public bool RemoveCardAt(int cardPos)
        {
            Card card = _hand[cardPos];
            _hand.RemoveAt(cardPos);
            return !_hand.Contains(card); // if it doesnt cotain the Card at the index given by the integer parameter,remove successfuly
        }


        /// <summary>
        /// Sort the Hand first by Suit, and then by FaceValue 
        /// </summary>
        public void SortHand()
        {
            _hand.Sort();//???not correct!!! we dont know how to Sort the Hand first by Suit, and then by FaceValue 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {

            return _hand.GetEnumerator();
        }
    }
}
