using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameObjects;
using Games;

namespace GUI {

    public partial class Crazy_Eights_Form : Form {

        public Crazy_Eights_Form() {
            InitializeComponent();
            Hand userHand = new Hand(new List<Card> {
            new Card(Suit.Diamonds, FaceValue.Three),
            new Card(Suit.Spades, FaceValue.King)
            });
            Hand computerHand = new Hand(new List<Card> {
            new Card(Suit.Hearts, FaceValue.Ace),
            new Card(Suit.Spades, FaceValue.Two)
            });
            DisplayHand(userHand, tblUserHand);
            DisplayHand(computerHand, tblComputerHand);
            picDrawpile.Image = Images.GetBackOfCardImage();
            picDiscardpile.Image = Images.GetCardImage(new Card(Suit.Hearts, FaceValue.Queen));
        }
        public bool wait = false;

      

        private void Crazy_Eights_Form_Load(object sender, EventArgs e)
        {

        }

        private void DisplayHand(Hand hand, TableLayoutPanel panel)
        {
            // remove any previous card images
            panel.Controls.Clear();
            // repeat for each card in the hand
            for (int i = 0; i < hand.GetCount(); i++) {
                // add a picture box to the panel with the appropriate image
                PictureBox picCard = new PictureBox();
                picCard.SizeMode = PictureBoxSizeMode.AutoSize;
                picCard.Image = Images.GetCardImage(hand.GetCard(i));
                panel.Controls.Add(picCard, i, 0);
                // add an event handler if it is being added to the user’s panel
                if (panel == tblUserHand) {
                    picCard.Click += new System.EventHandler(this.picPlayCard_Click);
                }
            }
        }
        private void picPlayCard_Click(object sender, EventArgs e)
        {
            // get the picturebox that was clicked
            PictureBox picCard = (PictureBox)sender;
            // determine the position of the picturebox that was clicked
            int columnNum = ((TableLayoutPanel)((Control)sender).Parent).GetPositionFromControl(picCard).Column;
            // ...you will need to continue this yourself in part C...
            MessageBox.Show(string.Format("Clicked column {0}", columnNum)); // temporary
        }



        private void UpdateInstructions(string message, bool wait = false)
        {
            lblInstruction.Text = message;
            lblInstruction.Refresh();
            if (wait) {
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            Choose_Suit_Form suitForm = new Choose_Suit_Form();
            suitForm.ShowDialog();
            UpdateInstructions(string.Format("You chose {0}.", suitForm.GetChosenSuit().ToString()), wait=true);
            UpdateInstructions("Click Deal to start the game.");
            
        }


        }
    }

