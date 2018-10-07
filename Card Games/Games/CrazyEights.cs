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
            discardPile = new CardPile();
            drawPile = new CardPile(true);
            drawPile.ShufflePile();
            UserHand = new Hand(drawPile.DealCards(8));
            ComputerHand = new Hand(drawPile.DealCards(8));
            ComputerHand = computerHand;
            UserHand = userHand;
            discardPile.AddCard(drawPile.DealOneCard());

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
                }else if (card.GetFaceValue() == FaceValue.Eight){
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
            ActionResult action = 0;
           
            if (IsPlaying == false){
                throw new System.ArgumentException("This game is not start!");
            }
            if (IsUserTurn == false){
                throw new System.ArgumentException("This is not your turn!");
            }

            Card drewcard = _drawPile.DealOneCard();

            UserHand.AddCard(drewcard);
            
            if (IsHandPlayable(UserHand)){
                action = ActionResult.CannotDraw;
            } else if (IsCardPlayable(drewcard)){
                action = ActionResult.DrewPlayableCard;
            } else if (!IsCardPlayable(drewcard)){
                action = ActionResult.DrewUnplayableCard;
            }
            else if (DrewAndNoMovePossible(drewcard, _userhand))
            {
                IsUserTurn = !IsUserTurn;
                action = ActionResult.DrewAndNoMovePossible;
            }
            else if (DrewAndNoMovePossible(drewcard, _userhand) && !IsHandPlayable(_computerhand))
            {                
                foreach (Card card in _userhand)
                {
                    _drawPile.AddCard(card);
                }
                _drawPile.ShufflePile();
                action = ActionResult.DrewAndResetPiles;
            } 
            else if (_drawPile.GetCount() == 0)
            {
                _drawPile = _discardPile;
                _discardPile.DealCards(_discardPile.GetCount());
                action = ActionResult.FlippedDeck;
            }
            return action;

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

            
            ActionResult whichaction = new ActionResult();
            Card card = _userhand.GetCard(cardNum);
            if (IsPlaying == false)
            {
                throw new System.ArgumentException("This game is not start!");
            }
            if (IsUserTurn == false)
            {
                throw new System.ArgumentException("This is not your turn!");
            }
            /// how to change the suit of a card
            if (chosenSuit != null && card.GetFaceValue() == FaceValue.Eight)
            {
                Card eight = new Card(chosenSuit, FaceValue.Eight);
                _userhand.RemoveCardAt(cardNum);
                _userhand.AddCard(eight);
            }
            if (card.GetFaceValue() == FaceValue.Eight && chosenSuit == null)
            {
                whichaction = ActionResult.SuitRequired;
            }
            /// is this the end of one game?
            if (_userhand.ContainsCard(card) && _userhand.GetCount() == 0)
            {
                


                whichaction = ActionResult.WinningPlay;
            }
            /// feel like it has some problems here 
            if (_computerhand.GetCount() == 13 && IsHandPlayable(_computerhand) == false)
            {

                whichaction = ActionResult.ValidPlayAndExtraTurn;
            }
            if (_userhand.ContainsCard(card) == false)
            {
                _whosturn = !_whosturn;
                whichaction = ActionResult.ValidPlay;
            }
            /// should the computer drew a card?
            if (DrewAndNoMovePossible(card, _userhand))
            {
                whichaction = ActionResult.InvalidPlayAndMustDraw;
            }
            if (IsCardPlayable(card) == false)
            {
                whichaction = ActionResult.InvalidPlay;
            }
            return whichaction;   
        }
        
        
        
        
        /// <summary>
        /// a
        /// </summary>
        /// <returns></returns>
        public static ActionResult ComputerAction(){
            ActionResult whichaction = new ActionResult();
            Card drewcard = _drawPile.DealOneCard();
            ///its a temp card here, need to finish more steps to get the card
            Card card = new Card();
            if (IsPlaying == false)
            {
                throw new System.ArgumentException("This game is not start!");
            }
            if (IsUserTurn == true)
            {
                throw new System.ArgumentException("This is not your turn!");
            }
            _computerhand.AddCard(drewcard);
            /// comeputer moves
            if (IsCardPlayable(drewcard))
            {
                whichaction = ActionResult.DrewPlayableCard;
            }
            else if (!IsCardPlayable(drewcard))
            {
                whichaction = ActionResult.DrewUnplayableCard;
            }
            else if (DrewAndNoMovePossible(drewcard, _computerhand))
            {
                IsUserTurn = !IsUserTurn;
                whichaction = ActionResult.DrewAndNoMovePossible;
            }
            else if (DrewAndNoMovePossible(drewcard, _computerhand) && !IsHandPlayable(_userhand))
            {
                foreach (Card cards in _userhand)
                {
                    _drawPile.AddCard(cards);
                }
                _drawPile.ShufflePile();
                whichaction = ActionResult.DrewAndResetPiles;
            }
            else if (_drawPile.GetCount() == 0)
            {
                _drawPile = _discardPile;
                _discardPile.DealCards(_discardPile.GetCount());
                whichaction = ActionResult.FlippedDeck;
            }
            else if (_computerhand.ContainsCard(card) == false && _computerhand.GetCount() == 0)
            {
                whichaction = ActionResult.WinningPlay;
            }
            else if (_computerhand.ContainsCard(card) == false && IsHandPlayable(_userhand))
            {
                whichaction = ActionResult.ValidPlayAndExtraTurn;
            }
            else if (_computerhand.ContainsCard(card) == false)
            {
                _whosturn = !_whosturn;
            }
            else if ( _computerhand.ContainsCard(card) == false && _computerhand.GetCount() == 0)
            {
                whichaction = ActionResult.WinningPlay;
            }
            /// feel like it has some problems here 
            else if (_computerhand.ContainsCard(card) == false && IsHandPlayable(_computerhand))
            {
                whichaction = ActionResult.ValidPlayAndExtraTurn;
            }
            else if (_computerhand.ContainsCard(card) == false)
            {
                _whosturn = !_whosturn;
                whichaction = ActionResult.ValidPlay;
            }
            return whichaction;
        }
    }
}
