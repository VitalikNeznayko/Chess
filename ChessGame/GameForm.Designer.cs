namespace ChessGame
{
    partial class GameForm
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
            ChessPanel = new Panel();
            btnBackToMenu = new Button();
            name1 = new Label();
            name2 = new Label();
            panel1 = new Panel();
            blackTimer = new Label();
            panel2 = new Panel();
            whiteTimer = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // ChessPanel
            // 
            ChessPanel.AutoSize = true;
            ChessPanel.Location = new Point(12, 50);
            ChessPanel.Name = "ChessPanel";
            ChessPanel.Size = new Size(422, 366);
            ChessPanel.TabIndex = 0;
            ChessPanel.Paint += ChessPanel_Paint;
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.Location = new Point(598, 12);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(77, 48);
            btnBackToMenu.TabIndex = 1;
            btnBackToMenu.Text = "Back To Menu ";
            btnBackToMenu.UseVisualStyleBackColor = true;
            btnBackToMenu.Click += btnBackToMenu_Click;
            // 
            // name1
            // 
            name1.AutoSize = true;
            name1.Font = new Font("Segoe UI", 11F);
            name1.Location = new Point(12, 614);
            name1.Name = "name1";
            name1.Size = new Size(98, 25);
            name1.TabIndex = 2;
            name1.Text = "Гравець 1";
            name1.Click += label1_Click;
            // 
            // name2
            // 
            name2.AutoSize = true;
            name2.Font = new Font("Segoe UI", 11F);
            name2.Location = new Point(12, 12);
            name2.Name = "name2";
            name2.Size = new Size(98, 25);
            name2.TabIndex = 3;
            name2.Text = "Гравець 2";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(blackTimer);
            panel1.Location = new Point(465, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(95, 40);
            panel1.TabIndex = 4;
            // 
            // blackTimer
            // 
            blackTimer.AutoSize = true;
            blackTimer.Font = new Font("Segoe UI", 12F);
            blackTimer.Location = new Point(18, 5);
            blackTimer.Name = "blackTimer";
            blackTimer.Size = new Size(60, 28);
            blackTimer.TabIndex = 0;
            blackTimer.Text = "00:00";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(whiteTimer);
            panel2.Location = new Point(465, 605);
            panel2.Name = "panel2";
            panel2.Size = new Size(95, 40);
            panel2.TabIndex = 5;
            // 
            // whiteTimer
            // 
            whiteTimer.AutoSize = true;
            whiteTimer.Font = new Font("Segoe UI", 12F);
            whiteTimer.Location = new Point(18, 5);
            whiteTimer.Name = "whiteTimer";
            whiteTimer.Size = new Size(60, 28);
            whiteTimer.TabIndex = 0;
            whiteTimer.Text = "00:00";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(687, 655);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(name2);
            Controls.Add(name1);
            Controls.Add(btnBackToMenu);
            Controls.Add(ChessPanel);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel ChessPanel;
        private Button btnBackToMenu;
        private Label name1;
        private Label name2;
        private Panel panel1;
        private Label blackTimer;
        private Panel panel2;
        private Label whiteTimer;
    }
}
