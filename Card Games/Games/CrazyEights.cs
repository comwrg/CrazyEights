using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameObjects;

namespace Games {
    /// <summary>
    /// 
    /// </summary>
    public static class CrazyEights {
        /// <summary>
        /// 
        /// </summary>
        public enum ActionResult {
         /// <summary>
         /// A card was played that won the game
         /// </summary>
         WinningPlay,
         /// <summary>
         /// A valid card was played
         /// </summary>
         ValidPlay,
         /// <summary>
         /// A suit is required to continue play
         /// </summary>
         SuitRequired,
         /// <summary>
         /// Attempted to play an invalid card
         /// </summary>
         InvalidPlay,
         /// <summary>
         /// Attempted to play an invalid card when no cards can be played
         /// </summary>
         InvalidPlayAndMustDraw,
         /// <summary>
         /// A valid card was played, and the other player cannot play
         /// </summary>
         ValidPlayAndExtraTurn,
         /// <summary>
         /// Drew a playable card
         /// </summary>
         DrewPlayableCard,
         /// <summary>
         /// Drew an unplayable card
         /// </summary>
         DrewUnplayableCard,
         /// <summary>
         /// Drew an unplayable card and filled the hand
         /// </summary>
         DrewAndNoMovePossible,
         /// <summary>
         /// Drew an unplayable card and filled the hand, leaving both
         /// players unable to play, so piles were reset so that the
         /// the other player can continue play (rule 9)
         /// </summary>
         DrewAndResetPiles,
         /// <summary>
         /// Attempted to draw a card while moves were still possible
         /// </summary>
         CannotDraw,
         /// <summary>
         /// Flipped the discard pile to use as the new draw pile (rule 10)
         /// </summary>
         FlippedDeck
        }


        private static CardPile _drawPile;
        private static CardPile _discardPile;
        private static Hand _computerhand;
        private static Hand _userhand;
        private static bool _whosturn;
        private static bool _canplay;

        
        public static Card TopDiscard {
            get{ return _discardPile.GetLastCardInPile();}
        } 
        public static bool IsDrawPileEmpty {
            get{ return _drawPile.GetCount() == 0;}
        }
        public static Hand ComputerHand {
            get{ return _computerhand;}
            private set{ _computerhand = value;}
        }
        public static Hand UserHand {
            get{ return _userhand;}
            private set{ _userhand = value;}
        }
        public static bool IsUserTurn {
            get{ return _whosturn;}
            private set{ _whosturn = value;}
        }
        public static bool IsPlaying {
            get { return _canplay;}
            private set{ _canplay = value;}
        }

        /// <summary>
        /// a
        /// </summary>
        public static void StartGame(){
            _canplay = true;
            IsUserTurn = true;
            _drawPile = new CardPile(true);
            _drawPile.ShufflePile();
            _discardPile = new CardPile();
            UserHand = new Hand(_drawPile.DealCards(8));
            ComputerHand = new Hand(_drawPile.DealCards(8));
            _discardPile.AddCard(_drawPile.DealOneCard());
        }
        /// <summary>
        /// a
        /// </summary>
        /// <param name="userHand"></param>
        /// <param name="computerHand"></param>
        /// <param name="discardPile"></param>
        /// <param name="drawPile"></param>
        public static void StartGame(Hand userHand, Hand computerHand, CardPile discardPile, CardPile drawPile){
            _canplay = true;
            IsUserTurn = true;
            UserHand = userHand;
            ComputerHand = computerHand;
            _discardPile = discardPile;
            _drawPile = drawPile;
        }
        /// <summary>
        /// a
        /// </summary>
        public static void SortUserHand(){            
                _userhand.SortHand();
            
        }

        /// <summary>
        /// a
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static bool IsHandPlayable(Hand hand){
            foreach (Card card in hand){
                if (card.GetFaceValue() == TopDiscard.GetFaceValue()){
                    return true;
                } else if (card.GetSuit() == TopDiscard.GetSuit()){
                    return true;
                }
                else if (card.GetFaceValue() == FaceValue.Eight)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// a
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private static bool IsCardPlayable(Card card){
            if (card.GetFaceValue() == TopDiscard.GetFaceValue()){
                return true;
            } else if (card.GetSuit() == TopDiscard.GetSuit()){
                return true;
            } else if (_canplay && card.GetFaceValue() == FaceValue.Eight){
                return true;
            }
            return false;
        }
        

        private static bool DrewAndNoMovePossible(Card card, Hand hand)
        {
            bool temp = false;
            if (IsCardPlayable(card) == false && IsHandPlayable(hand) == true)
            {
                temp = true;
            }
            return temp;
            
        }
        /// <summary>
        /// a
        /// </summary>
        /// <returns></returns>
        public static  ActionResult UserDrawCard(){
            if (IsPlaying == false){
                throw new System.ArgumentException("This game is not start!");
            }
            if (IsUserTurn == false){
                throw new System.ArgumentException("This is not your turn!");
            }


            if (TopDiscard.GetFaceValue() == FaceValue.Eight)
            {
                return ActionResult.CannotDraw;
            }

            foreach (Card c in UserHand)
            {
                if (c.GetFaceValue() == TopDiscard.GetFaceValue() || c.GetSuit() == TopDiscard.GetSuit())
                {
                    return ActionResult.CannotDraw;
                }
            }

            if (_drawPile.GetCount() == 0)
            {
                _discardPile.Reverse();
                while (_discardPile.GetCount() > 1)
                {
                    _drawPile.AddCard(_discardPile.DealOneCard());
                }
                return ActionResult.FlippedDeck;
            }

            Card drawCard = _drawPile.DealOneCard();
            UserHand.AddCard(drawCard);

            if (drawCard.GetFaceValue() == TopDiscard.GetFaceValue() || drawCard.GetSuit() == TopDiscard.GetSuit())
            {
                return ActionResult.DrewPlayableCard;
            }
            else
            {
                if (UserHand.GetCount() == 13)
                {
                    IsUserTurn = false;
                    if (ComputerHand.GetCount() == 13)
                    {
                        return ActionResult.DrewAndResetPiles;
                    }
                    else
                    {
                        return ActionResult.DrewAndNoMovePossible;
                    }
                }
                else
                {
                    return ActionResult.DrewUnplayableCard;
                }
            }


        }

        private static ActionResult DrawCard(Hand hand){
            if(IsUserTurn == true) {
                hand = _userhand;
            } else {
                hand = _computerhand;
            }
            if (_canplay && TopDiscard.GetFaceValue() == FaceValue.Eight)
            {
                return ActionResult.CannotDraw;
            }
            for (int i = 0; i < hand.GetCount(); i++)
            {
                if (hand.GetCard(i).GetSuit() == TopDiscard.GetSuit())
                {
                    return ActionResult.CannotDraw;
                }
                else if (hand.GetCard(i).GetFaceValue() == TopDiscard.GetFaceValue())
                {
                    return ActionResult.CannotDraw;
                }
                else if (hand.GetCard(i).GetFaceValue() == FaceValue.Eight)
                {
                    return ActionResult.CannotDraw;
                }
            }
            _canplay = false;
            return ActionResult.DrewPlayableCard;
        }
        /// <summary>
        /// a
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="chosenSuit"></param>
        /// <returns></returns>
        public static ActionResult UserPlayCard(int cardNum, Suit? chosenSuit = null){
            if (IsPlaying == false)
            {
                throw new System.ArgumentException("This game is not start!");
            }
            if (IsUserTurn == false)
            {
                throw new System.ArgumentException("This is not your turn!");
            }
            
            Card card = _userhand.GetCard(cardNum);

            if (_discardPile.GetCount() == 1 && TopDiscard.GetFaceValue() == FaceValue.Eight)
            {
                IsUserTurn = false;
                _userhand.RemoveCardAt(cardNum);
                _discardPile.AddCard(card);
                return ActionResult.ValidPlay;
            }

            if (card.GetFaceValue() == FaceValue.Eight)
            {
                if (!chosenSuit.HasValue)
                {
                    return ActionResult.SuitRequired;
                }

                IsUserTurn = false;
                _userhand.RemoveCardAt(cardNum);
                _discardPile.AddCard(new Card(chosenSuit, FaceValue.Eight));
                return ActionResult.ValidPlay;
            }
            
            if (card.GetFaceValue() == TopDiscard.GetFaceValue() || card.GetSuit() == TopDiscard.GetSuit())
            {
                _userhand.RemoveCardAt(cardNum);
                _discardPile.AddCard(card);

                if (UserHand.GetCount() == 0)
                {
                    IsPlaying = false;
                    return ActionResult.WinningPlay;
                }

                if (ComputerHand.GetCount() == 13)
                {
                    foreach (Card c in ComputerHand)
                    {
                        if (c.GetFaceValue() == TopDiscard.GetFaceValue() || c.GetSuit() == TopDiscard.GetSuit())
                        {
                            IsUserTurn = false;
                            return ActionResult.ValidPlay;
                        }
                    }
                    return ActionResult.ValidPlayAndExtraTurn;
                }

                IsUserTurn = false;
                return ActionResult.ValidPlay;
            }

            // Invalid Play

            foreach (Card c in UserHand)
            {
                if (c.GetFaceValue() == TopDiscard.GetFaceValue() || c.GetSuit() == TopDiscard.GetSuit() || c.GetFaceValue() == FaceValue.Eight)
                {
                    return ActionResult.InvalidPlay;
                }
            }

            return ActionResult.InvalidPlayAndMustDraw;
        }
        
        
        
        
        /// <summary>
        /// a
        /// </summary>
        /// <returns></returns>
        public static ActionResult ComputerAction(){
            if (IsPlaying == false)
            {
                throw new System.ArgumentException("This game is not start!");
            }
            if (IsUserTurn == true)
            {
                throw new System.ArgumentException("This is not your turn!");
            }


            if (TopDiscard.GetFaceValue() == FaceValue.Eight)
            {
                IsUserTurn = true;
                _discardPile.AddCard(ComputerHand.GetCard(0));
                ComputerHand.RemoveCardAt(0);
                return ActionResult.ValidPlay;
            }

            for (int j = 0; j < 3; ++j) // j = 0, match face, j = 1, match suit, j = 2, match eight
            {
                for (int i = ComputerHand.GetCount()-1; i >= 0; --i)
                {
                    Card c = ComputerHand.GetCard(i);
                    if ((c.GetFaceValue() == TopDiscard.GetFaceValue() && j == 0) || (c.GetSuit() == TopDiscard.GetSuit() && j == 1) || (c.GetFaceValue() == FaceValue.Eight && j == 2))
                    {
                        IsUserTurn = false;
                        ComputerHand.RemoveCardAt(i);
                        _discardPile.AddCard(c);

                        if (ComputerHand.GetCount() == 0)
                        {
                            IsPlaying = false;
                            return ActionResult.WinningPlay;
                        }

                        if (UserHand.GetCount() == 13)
                        {
                            foreach (Card c1 in UserHand)
                            {
                                if (c1.GetFaceValue() == TopDiscard.GetFaceValue() ||
                                    c1.GetSuit() == TopDiscard.GetSuit())
                                {
                                    IsUserTurn = true;
                                    return ActionResult.ValidPlay;
                                }
                            }

                            IsUserTurn = false;
                            return ActionResult.ValidPlayAndExtraTurn;
                        }

                        IsUserTurn = true;
                        return ActionResult.ValidPlay;
                    }
                }
            }

            if (_drawPile.GetCount() == 0)
            {
                _discardPile.Reverse();
                while (_discardPile.GetCount() > 1)
                {
                    _drawPile.AddCard(_discardPile.DealOneCard());
                }
                return ActionResult.FlippedDeck;
            }

            Card drawCard = _drawPile.DealOneCard();
            ComputerHand.AddCard(drawCard);

            if (drawCard.GetFaceValue() == TopDiscard.GetFaceValue() || drawCard.GetSuit() == TopDiscard.GetSuit())
            {
                return ActionResult.DrewPlayableCard;
            }
            else
            {
                if (ComputerHand.GetCount() == 13)
                {
                    IsUserTurn = true;
                    if (UserHand.GetCount() == 13)
                    {
                        return ActionResult.DrewAndResetPiles;
                    }
                    else
                    {
                        return ActionResult.DrewAndNoMovePossible;
                    }
                }
                else
                {
                    return ActionResult.DrewUnplayableCard;
                }
            }

            /*
            if (card.GetFaceValue() == FaceValue.Eight)
            {
                if (!chosenSuit.HasValue)
                {
                    return ActionResult.SuitRequired;
                }

                IsUserTurn = false;
                _userhand.RemoveCardAt(cardNum);
                _discardPile.AddCard(new Card(chosenSuit, FaceValue.Eight));
                return ActionResult.ValidPlay;
            }
            
            if (card.GetFaceValue() == TopDiscard.GetFaceValue() || card.GetSuit() == TopDiscard.GetSuit())
            {
                _userhand.RemoveCardAt(cardNum);
                _discardPile.AddCard(card);

                if (UserHand.GetCount() == 0)
                {
                    IsPlaying = false;
                    return ActionResult.WinningPlay;
                }

                if (ComputerHand.GetCount() == 13)
                {
                    foreach (Card c in ComputerHand)
                    {
                        if (c.GetFaceValue() == TopDiscard.GetFaceValue() || c.GetSuit() == TopDiscard.GetSuit())
                        {
                            IsUserTurn = false;
                            return ActionResult.ValidPlay;
                        }
                    }
                    return ActionResult.ValidPlayAndExtraTurn;
                }

                IsUserTurn = false;
                return ActionResult.ValidPlay;
            }

            // Invalid Play

            foreach (Card c in UserHand)
            {
                if (c.GetFaceValue() == TopDiscard.GetFaceValue() || c.GetSuit() == TopDiscard.GetSuit())
                {
                    return ActionResult.InvalidPlay;
                }
            }

            return ActionResult.InvalidPlayAndMustDraw;
             */

        }
    }
}
