namespace GUI
{
    partial class Choose_Suit_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInstruction = new System.Windows.Forms.Label();
            this.grpChoosesuit = new System.Windows.Forms.GroupBox();
            this.rdoHearts = new System.Windows.Forms.RadioButton();
            this.rdoSpades = new System.Windows.Forms.RadioButton();
            this.rdoDiamonds = new System.Windows.Forms.RadioButton();
            this.rdoClubs = new System.Windows.Forms.RadioButton();
            this.btnPlaycard = new System.Windows.Forms.Button();
            this.grpChoosesuit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstruction.Location = new System.Drawing.Point(87, 30);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(177, 32);
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "     You chose an Eight!\r\nYou get to choose the Suit.";
            // 
            // grpChoosesuit
            // 
            this.grpChoosesuit.Controls.Add(this.rdoHearts);
            this.grpChoosesuit.Controls.Add(this.rdoSpades);
            this.grpChoosesuit.Controls.Add(this.rdoDiamonds);
            this.grpChoosesuit.Controls.Add(this.rdoClubs);
            this.grpChoosesuit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpChoosesuit.Location = new System.Drawing.Point(90, 92);
            this.grpChoosesuit.Name = "grpChoosesuit";
            this.grpChoosesuit.Size = new System.Drawing.Size(165, 143);
            this.grpChoosesuit.TabIndex = 1;
            this.grpChoosesuit.TabStop = false;
            this.grpChoosesuit.Text = "Choose a Suit";
            // 
            // rdoHearts
            // 
            this.rdoHearts.AutoSize = true;
            this.rdoHearts.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoHearts.Location = new System.Drawing.Point(38, 83);
            this.rdoHearts.Name = "rdoHearts";
            this.rdoHearts.Size = new System.Drawing.Size(66, 20);
            this.rdoHearts.TabIndex = 3;
            this.rdoHearts.TabStop = true;
            this.rdoHearts.Text = "Hearts";
            this.rdoHearts.UseVisualStyleBackColor = true;
            this.rdoHearts.CheckedChanged += new System.EventHandler(this.rdoChooseSuit);
            // 
            // rdoSpades
            // 
            this.rdoSpades.AutoSize = true;
            this.rdoSpades.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoSpades.Location = new System.Drawing.Point(38, 109);
            this.rdoSpades.Name = "rdoSpades";
            this.rdoSpades.Size = new System.Drawing.Size(73, 20);
            this.rdoSpades.TabIndex = 4;
            this.rdoSpades.TabStop = true;
            this.rdoSpades.Text = "Spades";
            this.rdoSpades.UseVisualStyleBackColor = true;
            this.rdoSpades.CheckedChanged += new System.EventHandler(this.rdoChooseSuit);
            // 
            // rdoDiamonds
            // 
            this.rdoDiamonds.AutoSize = true;
            this.rdoDiamonds.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoDiamonds.Location = new System.Drawing.Point(38, 57);
            this.rdoDiamonds.Name = "rdoDiamonds";
            this.rdoDiamonds.Size = new System.Drawing.Size(89, 20);
            this.rdoDiamonds.TabIndex = 1;
            this.rdoDiamonds.TabStop = true;
            this.rdoDiamonds.Text = "Diamonds";
            this.rdoDiamonds.UseVisualStyleBackColor = true;
            this.rdoDiamonds.CheckedChanged += new System.EventHandler(this.rdoChooseSuit);
            // 
            // rdoClubs
            // 
            this.rdoClubs.AutoSize = true;
            this.rdoClubs.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoClubs.Location = new System.Drawing.Point(38, 31);
            this.rdoClubs.Name = "rdoClubs";
            this.rdoClubs.Size = new System.Drawing.Size(61, 20);
            this.rdoClubs.TabIndex = 0;
            this.rdoClubs.TabStop = true;
            this.rdoClubs.Text = "Clubs";
            this.rdoClubs.UseVisualStyleBackColor = true;
            this.rdoClubs.CheckedChanged += new System.EventHandler(this.rdoChooseSuit);
            // 
            // btnPlaycard
            // 
            this.btnPlaycard.Enabled = false;
            this.btnPlaycard.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlaycard.Location = new System.Drawing.Point(87, 279);
            this.btnPlaycard.Name = "btnPlaycard";
            this.btnPlaycard.Size = new System.Drawing.Size(177, 36);
            this.btnPlaycard.TabIndex = 2;
            this.btnPlaycard.Text = "Play Card";
            this.btnPlaycard.UseVisualStyleBackColor = true;
            this.btnPlaycard.Click += new System.EventHandler(this.btnPlaycard_Click);
            // 
            // Choose_Suit_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 364);
            this.ControlBox = false;
            this.Controls.Add(this.btnPlaycard);
            this.Controls.Add(this.grpChoosesuit);
            this.Controls.Add(this.lblInstruction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Choose_Suit_Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose a Suit";
            this.TopMost = true;
            this.grpChoosesuit.ResumeLayout(false);
            this.grpChoosesuit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.GroupBox grpChoosesuit;
        private System.Windows.Forms.RadioButton rdoHearts;
        private System.Windows.Forms.RadioButton rdoSpades;
        private System.Windows.Forms.RadioButton rdoDiamonds;
        private System.Windows.Forms.RadioButton rdoClubs;
        private System.Windows.Forms.Button btnPlaycard;
    }
}