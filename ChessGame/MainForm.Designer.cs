namespace ChessGame
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
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
            btnNewGame = new Button();
            btnHistory = new Button();
            checkBox1 = new CheckBox();
            btnExit = new Button();
            label1 = new Label();
            panel1 = new Panel();
            btnJoinLobby = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(24, 99);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(228, 40);
            btnNewGame.TabIndex = 0;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // btnHistory
            // 
            btnHistory.Location = new Point(24, 214);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(228, 40);
            btnHistory.TabIndex = 1;
            btnHistory.Text = "History Of Games";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(175, 450);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 24);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(24, 276);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(228, 40);
            btnExit.TabIndex = 4;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 35F);
            label1.Location = new Point(41, 9);
            label1.Name = "label1";
            label1.Size = new Size(184, 78);
            label1.TabIndex = 5;
            label1.Text = "Chess";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnJoinLobby);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnNewGame);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(btnHistory);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(272, 329);
            panel1.TabIndex = 6;
            // 
            // btnJoinLobby
            // 
            btnJoinLobby.Location = new Point(24, 157);
            btnJoinLobby.Name = "btnJoinLobby";
            btnJoinLobby.Size = new Size(228, 40);
            btnJoinLobby.TabIndex = 6;
            btnJoinLobby.Text = "Join Lobby";
            btnJoinLobby.UseVisualStyleBackColor = true;
            btnJoinLobby.Click += btnJoinLobby_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(295, 353);
            Controls.Add(panel1);
            Controls.Add(checkBox1);
            Name = "MainForm";
            Text = "MainForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNewGame;
        private Button btnHistory;
        private CheckBox checkBox1;
        private Button btnExit;
        private Label label1;
        private Panel panel1;
        private Button btnJoinLobby;
    }
}