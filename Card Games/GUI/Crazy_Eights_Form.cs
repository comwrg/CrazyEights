using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameObjects;
using Games;

namespace GUI {

    public partial class Crazy_Eights_Form : Form
    {

        public Crazy_Eights_Form()
        {
            InitializeComponent();
        }

        private void Crazy_Eights_Form_Load(object sender, EventArgs e)
        {

        }

        private void DisplayHand(Hand hand, TableLayoutPanel panel)
        {
            // remove any previous card images
            panel.Controls.Clear();
            // repeat for each card in the hand
            for (int i = 0; i < hand.GetCount(); i++)
            {
                // add a picture box to the panel with the appropriate image
                PictureBox picCard = new PictureBox();
                picCard.SizeMode = PictureBoxSizeMode.AutoSize;
                picCard.Image = Images.GetCardImage(hand.GetCard(i));
                panel.Controls.Add(picCard, i, 0);
                // add an event handler if it is being added to the user’s panel
                if (panel == tblUserHand)
                {
                    picCard.Click += new System.EventHandler(this.picPlayCard_Click);
                }
            }
        }

        public static Suit eightSuit;

        private void picPlayCard_Click(object sender, EventArgs e) // user play
        {
            // get the picturebox that was clicked
            PictureBox picCard = (PictureBox) sender;
            // determine the position of the picturebox that was clicked
            int columnNum = ((TableLayoutPanel) ((Control) sender).Parent).GetPositionFromControl(picCard).Column;
            // ...you will need to continue this yourself in part C...
            //MessageBox.Show(string.Format("Clicked column {0}", columnNum)); // temporary
            if (!CrazyEights.IsUserTurn)
            {
                UpdateInstructions("Not you turn!");
                return;
            }
            CrazyEights.ActionResult actionResult = CrazyEights.UserPlayCard(columnNum);
            switch (actionResult)
            {
                case CrazyEights.ActionResult.SuitRequired:
                    new Choose_Suit_Form().ShowDialog(this);
                    CrazyEights.UserPlayCard(columnNum, eightSuit);
                    ComputerAction();
                    break;
                case CrazyEights.ActionResult.WinningPlay:
                    UpdateInstructions("You Win!");
                    UpdateUIEnable(false);
                    break;
                case CrazyEights.ActionResult.ValidPlayAndExtraTurn:
                    UpdateInstructions("You get extra turn!");
                    break;
                case CrazyEights.ActionResult.ValidPlay:
                    ComputerAction();
                    UpdateInstructions("You turn!");
                    break;
                case CrazyEights.ActionResult.InvalidPlayAndMustDraw:
                    UpdateInstructions("You have not card can play and must draw!");
                    break;
                case CrazyEights.ActionResult.InvalidPlay:
                    UpdateInstructions("You cannot play this card!");
                    break;

            }
            DisplayHand(CrazyEights.UserHand, tblUserHand);
            DisplayHand(CrazyEights.ComputerHand, tblComputerHand);
            picDiscardpile.Image = Images.GetCardImage(CrazyEights.TopDiscard);
            this.Controls.Owner.Update();

            if (!CrazyEights.IsPlaying)
            {
                UpdateUIEnable(false);
            }
        }

        private void ComputerAction()
        {
            DisplayHand(CrazyEights.UserHand, tblUserHand);
            DisplayHand(CrazyEights.ComputerHand, tblComputerHand);
            picDiscardpile.Image = Images.GetCardImage(CrazyEights.TopDiscard);
            this.Controls.Owner.Update();

            UpdateInstructions("Computer turn!", true);
            CrazyEights.ActionResult actionResult = CrazyEights.ComputerAction();
            Console.WriteLine(actionResult);
            switch (actionResult)
            {
                case CrazyEights.ActionResult.DrewPlayableCard:
                    UpdateInstructions("Drew a card", true);
                    ComputerAction();
                    break;
                case CrazyEights.ActionResult.DrewUnplayableCard:
                    UpdateInstructions("Drew a card", true);
                    ComputerAction();
                    break;
                case CrazyEights.ActionResult.DrewAndNoMovePossible:
                    break;
                case CrazyEights.ActionResult.DrewAndResetPiles:
                    picDrawpile.Image = null;
                    UpdateInstructions("Drew and reset piles", true);
                    picDrawpile.Image = Images.GetBackOfCardImage();
                    break;
                case CrazyEights.ActionResult.FlippedDeck:
                    UpdateInstructions("Flipped deck", true);
                    ComputerAction();
                    break;
                case CrazyEights.ActionResult.WinningPlay:
                    UpdateInstructions("Computer Win!");
                    UpdateUIEnable(false);
                    break;
                case CrazyEights.ActionResult.ValidPlayAndExtraTurn:
                    UpdateInstructions("Get extern turn!");
                    ComputerAction();
                    break;
                case CrazyEights.ActionResult.ValidPlay:
                    break;
            }

            if (CrazyEights.IsDrawPileEmpty)
            {
                picDrawpile.Image = null;
            }
            else
            {
                picDrawpile.Image= Images.GetBackOfCardImage();
            }

            DisplayHand(CrazyEights.UserHand, tblUserHand);
            DisplayHand(CrazyEights.ComputerHand, tblComputerHand);
            picDiscardpile.Image = Images.GetCardImage(CrazyEights.TopDiscard);
            this.Controls.Owner.Update();
        }


        private void UpdateInstructions(string message, bool wait = false)
        {
            lblInstruction.Text = message;
            lblInstruction.Refresh();
            if (wait)
            {
                Thread.Sleep(1000);
            }
        }



        private void btnDeal_Click(object sender, EventArgs e)
        {
            btnDeal.Enabled = false; btnDeal.ForeColor = Color.LemonChiffon;
            btnSorthand.Enabled = true; btnSorthand.ForeColor = Color.Black;
            btnExitgame.Enabled = true; btnExitgame.ForeColor = Color.Black;
            UpdateInstructions("You first!");
            // start game
            CrazyEights.StartGame();
            DisplayHand(CrazyEights.UserHand, tblUserHand);
            DisplayHand(CrazyEights.ComputerHand, tblComputerHand);
            picDrawpile.Image= Images.GetBackOfCardImage();
            picDiscardpile.Image = Images.GetCardImage(CrazyEights.TopDiscard);
        }

        private void btnSorthand_Click(object sender, EventArgs e)
        {
            // sort
            CrazyEights.SortUserHand();
            DisplayHand(CrazyEights.UserHand, tblUserHand);
        }

        private void btnExitgame_Click(object sender, EventArgs e)
        {
            UpdateUIEnable(false);
        }

        private void picDrawpile_Click(object sender, EventArgs e)
        {
            if (!CrazyEights.IsUserTurn)
            {
                UpdateInstructions("Not you turn!");
                return;
            }
            CrazyEights.ActionResult actionResult = CrazyEights.UserDrawCard();
            switch (actionResult)
            {
                case CrazyEights.ActionResult.CannotDraw:
                    UpdateInstructions("You cannot draw!");
                    break;
                case CrazyEights.ActionResult.DrewPlayableCard:
                    break;
                case CrazyEights.ActionResult.DrewUnplayableCard:
                    UpdateInstructions("No card can play, draw go on!");
                    break;
                case CrazyEights.ActionResult.DrewAndNoMovePossible:
                    ComputerAction();
                    break;
                case CrazyEights.ActionResult.DrewAndResetPiles:
                    UpdateInstructions("No body can play, reset piles");
                    break;
                case CrazyEights.ActionResult.FlippedDeck:
                    UpdateInstructions("Flipped Deck");
                    break;
            }

            if (CrazyEights.IsDrawPileEmpty)
            {
                picDrawpile.Image = null;
            }
            else
            {
                picDrawpile.Image= Images.GetBackOfCardImage();
            }

            DisplayHand(CrazyEights.UserHand, tblUserHand);
            DisplayHand(CrazyEights.ComputerHand, tblComputerHand);
            picDiscardpile.Image = Images.GetCardImage(CrazyEights.TopDiscard);
            this.Controls.Owner.Update();
        }

        private void UpdateUIEnable(bool isPlaying)
        {
            btnDeal.Enabled = isPlaying; btnDeal.ForeColor = btnDeal.Enabled ? Color.Black : Color.LemonChiffon;
            btnSorthand.Enabled = !isPlaying; btnSorthand.ForeColor = btnSorthand.Enabled ? Color.Black : Color.LemonChiffon;
            btnExitgame.Enabled = !isPlaying; btnExitgame.ForeColor = btnExitgame.Enabled ? Color.Black : Color.LemonChiffon;
        }
    }

}

