namespace uno
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            gbP1 = new GroupBox();
            pbCardsP1 = new PictureBox();
            cbP1PlayableCard = new ComboBox();
            btnP1UNO = new Button();
            btnP1Play = new Button();
            lbP1 = new Label();
            gbP2 = new GroupBox();
            pbCardsP2 = new PictureBox();
            cbP2PlayableCard = new ComboBox();
            btnP2UNO = new Button();
            btnP2Play = new Button();
            gbP4 = new GroupBox();
            pbCardsP4 = new PictureBox();
            cbP4PlayableCard = new ComboBox();
            btnP4UNO = new Button();
            btnP4Play = new Button();
            gbP3 = new GroupBox();
            pbCardsP3 = new PictureBox();
            cbP3PlayableCard = new ComboBox();
            btnP3UNO = new Button();
            btnP3Play = new Button();
            gbPileAndPlaceDown = new GroupBox();
            pbCardsDeck = new PictureBox();
            pbCardsPlayed = new PictureBox();
            btnPickup = new Button();
            btnReset = new Button();
            lbP2 = new Label();
            lbP3 = new Label();
            lbP4 = new Label();
            gbP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCardsP1).BeginInit();
            gbP2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCardsP2).BeginInit();
            gbP4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCardsP4).BeginInit();
            gbP3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCardsP3).BeginInit();
            gbPileAndPlaceDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCardsDeck).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCardsPlayed).BeginInit();
            SuspendLayout();
            // 
            // gbP1
            // 
            gbP1.Controls.Add(pbCardsP1);
            gbP1.Controls.Add(cbP1PlayableCard);
            gbP1.Controls.Add(btnP1UNO);
            gbP1.Controls.Add(btnP1Play);
            gbP1.ForeColor = Color.Black;
            gbP1.Location = new Point(793, 12);
            gbP1.Name = "gbP1";
            gbP1.Size = new Size(232, 292);
            gbP1.TabIndex = 0;
            gbP1.TabStop = false;
            gbP1.Text = "Player 1";
            gbP1.Paint += gbP1_Paint;
            // 
            // pbCardsP1
            // 
            pbCardsP1.Location = new Point(57, 60);
            pbCardsP1.Name = "pbCardsP1";
            pbCardsP1.Size = new Size(127, 176);
            pbCardsP1.TabIndex = 6;
            pbCardsP1.TabStop = false;
            // 
            // cbP1PlayableCard
            // 
            cbP1PlayableCard.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbP1PlayableCard.FormattingEnabled = true;
            cbP1PlayableCard.Location = new Point(6, 26);
            cbP1PlayableCard.Name = "cbP1PlayableCard";
            cbP1PlayableCard.Size = new Size(220, 27);
            cbP1PlayableCard.TabIndex = 2;
            cbP1PlayableCard.SelectedIndexChanged += cbP1PlayableCard_SelectedIndexChanged;
            // 
            // btnP1UNO
            // 
            btnP1UNO.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP1UNO.Location = new Point(6, 257);
            btnP1UNO.Name = "btnP1UNO";
            btnP1UNO.Size = new Size(94, 29);
            btnP1UNO.TabIndex = 1;
            btnP1UNO.Text = "UNO";
            btnP1UNO.UseVisualStyleBackColor = true;
            btnP1UNO.Click += btnP1UNO_Click;
            // 
            // btnP1Play
            // 
            btnP1Play.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP1Play.Location = new Point(121, 257);
            btnP1Play.Name = "btnP1Play";
            btnP1Play.Size = new Size(105, 29);
            btnP1Play.TabIndex = 0;
            btnP1Play.Text = "Play";
            btnP1Play.UseVisualStyleBackColor = true;
            btnP1Play.Click += btnP1Play_Click;
            // 
            // lbP1
            // 
            lbP1.AutoSize = true;
            lbP1.BackColor = Color.Transparent;
            lbP1.Font = new Font("Snap ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbP1.Location = new Point(666, 36);
            lbP1.Name = "lbP1";
            lbP1.Size = new Size(121, 30);
            lbP1.TabIndex = 4;
            lbP1.Text = "Player 1";
            // 
            // gbP2
            // 
            gbP2.Controls.Add(pbCardsP2);
            gbP2.Controls.Add(cbP2PlayableCard);
            gbP2.Controls.Add(btnP2UNO);
            gbP2.Controls.Add(btnP2Play);
            gbP2.Location = new Point(12, 333);
            gbP2.Name = "gbP2";
            gbP2.Size = new Size(232, 292);
            gbP2.TabIndex = 1;
            gbP2.TabStop = false;
            gbP2.Text = "Player 2";
            gbP2.Paint += gbP2_Paint;
            // 
            // pbCardsP2
            // 
            pbCardsP2.Location = new Point(54, 60);
            pbCardsP2.Name = "pbCardsP2";
            pbCardsP2.Size = new Size(127, 176);
            pbCardsP2.TabIndex = 5;
            pbCardsP2.TabStop = false;
            // 
            // cbP2PlayableCard
            // 
            cbP2PlayableCard.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbP2PlayableCard.FormattingEnabled = true;
            cbP2PlayableCard.Location = new Point(6, 26);
            cbP2PlayableCard.Name = "cbP2PlayableCard";
            cbP2PlayableCard.Size = new Size(220, 27);
            cbP2PlayableCard.TabIndex = 2;
            cbP2PlayableCard.SelectedIndexChanged += cbP2PlayableCard_SelectedIndexChanged;
            // 
            // btnP2UNO
            // 
            btnP2UNO.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP2UNO.Location = new Point(6, 257);
            btnP2UNO.Name = "btnP2UNO";
            btnP2UNO.Size = new Size(94, 29);
            btnP2UNO.TabIndex = 1;
            btnP2UNO.Text = "UNO";
            btnP2UNO.UseVisualStyleBackColor = true;
            btnP2UNO.Click += btnP2UNO_Click;
            // 
            // btnP2Play
            // 
            btnP2Play.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP2Play.Location = new Point(132, 257);
            btnP2Play.Name = "btnP2Play";
            btnP2Play.Size = new Size(94, 29);
            btnP2Play.TabIndex = 0;
            btnP2Play.Text = "Play";
            btnP2Play.UseVisualStyleBackColor = true;
            btnP2Play.Click += btnP2Play_Click;
            // 
            // gbP4
            // 
            gbP4.Controls.Add(pbCardsP4);
            gbP4.Controls.Add(cbP4PlayableCard);
            gbP4.Controls.Add(btnP4UNO);
            gbP4.Controls.Add(btnP4Play);
            gbP4.Location = new Point(1667, 333);
            gbP4.Name = "gbP4";
            gbP4.Size = new Size(232, 292);
            gbP4.TabIndex = 1;
            gbP4.TabStop = false;
            gbP4.Text = "Player 4";
            gbP4.Paint += gbP4_Paint;
            // 
            // pbCardsP4
            // 
            pbCardsP4.Location = new Point(55, 60);
            pbCardsP4.Name = "pbCardsP4";
            pbCardsP4.Size = new Size(127, 176);
            pbCardsP4.TabIndex = 4;
            pbCardsP4.TabStop = false;
            // 
            // cbP4PlayableCard
            // 
            cbP4PlayableCard.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbP4PlayableCard.FormattingEnabled = true;
            cbP4PlayableCard.Location = new Point(4, 26);
            cbP4PlayableCard.Name = "cbP4PlayableCard";
            cbP4PlayableCard.Size = new Size(226, 27);
            cbP4PlayableCard.TabIndex = 2;
            cbP4PlayableCard.SelectedIndexChanged += cbP4PlayableCard_SelectedIndexChanged;
            // 
            // btnP4UNO
            // 
            btnP4UNO.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP4UNO.Location = new Point(6, 257);
            btnP4UNO.Name = "btnP4UNO";
            btnP4UNO.Size = new Size(94, 29);
            btnP4UNO.TabIndex = 1;
            btnP4UNO.Text = "UNO";
            btnP4UNO.UseVisualStyleBackColor = true;
            btnP4UNO.Click += btnP4UNO_Click;
            // 
            // btnP4Play
            // 
            btnP4Play.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP4Play.Location = new Point(121, 257);
            btnP4Play.Name = "btnP4Play";
            btnP4Play.Size = new Size(105, 29);
            btnP4Play.TabIndex = 0;
            btnP4Play.Text = "Play";
            btnP4Play.UseVisualStyleBackColor = true;
            btnP4Play.Click += btnP4Play_Click;
            // 
            // gbP3
            // 
            gbP3.Controls.Add(pbCardsP3);
            gbP3.Controls.Add(cbP3PlayableCard);
            gbP3.Controls.Add(btnP3UNO);
            gbP3.Controls.Add(btnP3Play);
            gbP3.Location = new Point(793, 676);
            gbP3.Name = "gbP3";
            gbP3.Size = new Size(232, 292);
            gbP3.TabIndex = 1;
            gbP3.TabStop = false;
            gbP3.Text = "Player 3";
            gbP3.Paint += gbP3_Paint;
            // 
            // pbCardsP3
            // 
            pbCardsP3.Location = new Point(57, 60);
            pbCardsP3.Name = "pbCardsP3";
            pbCardsP3.Size = new Size(127, 176);
            pbCardsP3.TabIndex = 3;
            pbCardsP3.TabStop = false;
            // 
            // cbP3PlayableCard
            // 
            cbP3PlayableCard.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbP3PlayableCard.FormattingEnabled = true;
            cbP3PlayableCard.Location = new Point(6, 26);
            cbP3PlayableCard.Name = "cbP3PlayableCard";
            cbP3PlayableCard.Size = new Size(220, 27);
            cbP3PlayableCard.TabIndex = 2;
            cbP3PlayableCard.SelectedIndexChanged += cbP3PlayableCard_SelectedIndexChanged_1;
            // 
            // btnP3UNO
            // 
            btnP3UNO.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP3UNO.Location = new Point(6, 257);
            btnP3UNO.Name = "btnP3UNO";
            btnP3UNO.Size = new Size(94, 29);
            btnP3UNO.TabIndex = 1;
            btnP3UNO.Text = "UNO";
            btnP3UNO.UseVisualStyleBackColor = true;
            btnP3UNO.Click += btnP3UNO_Click;
            // 
            // btnP3Play
            // 
            btnP3Play.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnP3Play.Location = new Point(132, 257);
            btnP3Play.Name = "btnP3Play";
            btnP3Play.Size = new Size(94, 29);
            btnP3Play.TabIndex = 0;
            btnP3Play.Text = "Play";
            btnP3Play.UseVisualStyleBackColor = true;
            btnP3Play.Click += btnP3Play_Click;
            // 
            // gbPileAndPlaceDown
            // 
            gbPileAndPlaceDown.Controls.Add(pbCardsDeck);
            gbPileAndPlaceDown.Controls.Add(pbCardsPlayed);
            gbPileAndPlaceDown.Controls.Add(btnPickup);
            gbPileAndPlaceDown.Location = new Point(603, 310);
            gbPileAndPlaceDown.Name = "gbPileAndPlaceDown";
            gbPileAndPlaceDown.Size = new Size(600, 360);
            gbPileAndPlaceDown.TabIndex = 2;
            gbPileAndPlaceDown.TabStop = false;
            gbPileAndPlaceDown.Text = "Deck";
            gbPileAndPlaceDown.Paint += gbPileAndPlaceDown_Paint;
            // 
            // pbCardsDeck
            // 
            pbCardsDeck.Image = (Image)resources.GetObject("pbCardsDeck.Image");
            pbCardsDeck.Location = new Point(311, 64);
            pbCardsDeck.Name = "pbCardsDeck";
            pbCardsDeck.Size = new Size(127, 176);
            pbCardsDeck.TabIndex = 5;
            pbCardsDeck.TabStop = false;
            // 
            // pbCardsPlayed
            // 
            pbCardsPlayed.Location = new Point(163, 64);
            pbCardsPlayed.Name = "pbCardsPlayed";
            pbCardsPlayed.Size = new Size(127, 176);
            pbCardsPlayed.TabIndex = 4;
            pbCardsPlayed.TabStop = false;
            // 
            // btnPickup
            // 
            btnPickup.Font = new Font("Snap ITC", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPickup.Location = new Point(190, 275);
            btnPickup.Name = "btnPickup";
            btnPickup.Size = new Size(232, 29);
            btnPickup.TabIndex = 0;
            btnPickup.Text = "Pick up";
            btnPickup.UseVisualStyleBackColor = true;
            btnPickup.Click += btnPickup_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.Red;
            btnReset.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(8, 924);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(211, 55);
            btnReset.TabIndex = 3;
            btnReset.Text = "RESET GAME";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // lbP2
            // 
            lbP2.AutoSize = true;
            lbP2.BackColor = Color.Transparent;
            lbP2.Font = new Font("Snap ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbP2.Location = new Point(250, 357);
            lbP2.Name = "lbP2";
            lbP2.Size = new Size(127, 30);
            lbP2.TabIndex = 5;
            lbP2.Text = "Player 2";
            // 
            // lbP3
            // 
            lbP3.AutoSize = true;
            lbP3.BackColor = Color.Transparent;
            lbP3.Font = new Font("Snap ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbP3.Location = new Point(664, 702);
            lbP3.Name = "lbP3";
            lbP3.Size = new Size(129, 30);
            lbP3.TabIndex = 6;
            lbP3.Text = "Player 3";
            // 
            // lbP4
            // 
            lbP4.AutoSize = true;
            lbP4.BackColor = Color.Transparent;
            lbP4.Font = new Font("Snap ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbP4.Location = new Point(1532, 359);
            lbP4.Name = "lbP4";
            lbP4.Size = new Size(130, 30);
            lbP4.TabIndex = 7;
            lbP4.Text = "Player 4";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 985);
            Controls.Add(lbP4);
            Controls.Add(lbP3);
            Controls.Add(lbP2);
            Controls.Add(lbP1);
            Controls.Add(btnReset);
            Controls.Add(gbPileAndPlaceDown);
            Controls.Add(gbP3);
            Controls.Add(gbP4);
            Controls.Add(gbP2);
            Controls.Add(gbP1);
            Name = "Form1";
            Text = "Uno (No Mercy)";
            Paint += Form1_Paint;
            gbP1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCardsP1).EndInit();
            gbP2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCardsP2).EndInit();
            gbP4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCardsP4).EndInit();
            gbP3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCardsP3).EndInit();
            gbPileAndPlaceDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCardsDeck).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCardsPlayed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbP1;
        private GroupBox gbP2;
        private GroupBox gbP4;
        private GroupBox gbP3;
        private GroupBox gbPileAndPlaceDown;
        private Button btnP2UNO;
        private Button btnP2Play;
        private Button btnP3Play;
        private Button btnP1UNO;
        private Button btnP1Play;
        private Button btnP4UNO;
        private Button btnP4Play;
        private Button btnP3UNO;
        private Button btnPickup;
        private ComboBox cbP1PlayableCard;
        private ComboBox cbP2PlayableCard;
        private ComboBox cbP4PlayableCard;
        private PictureBox pbCardsP1;
        private PictureBox pbCardsP2;
        private PictureBox pbCardsP4;
        private PictureBox pbCardsP3;
        private ComboBox cbP3PlayableCard;
        private PictureBox pbCardsDeck;
        private PictureBox pbCardsPlayed;
        private Button btnReset;
        private Label lbP1;
        private Label lbP2;
        private Label lbP3;
        private Label lbP4;
    }
}
