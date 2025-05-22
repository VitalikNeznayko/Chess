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
            panelMain = new Panel();
            btnSettings = new Button();
            panelSettings = new Panel();
            label4 = new Label();
            cBColor = new ComboBox();
            label3 = new Label();
            cBTimer = new ComboBox();
            label2 = new Label();
            buttonBack = new Button();
            panelMain.SuspendLayout();
            panelSettings.SuspendLayout();
            SuspendLayout();
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(24, 106);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(228, 40);
            btnNewGame.TabIndex = 0;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // btnHistory
            // 
            btnHistory.Location = new Point(24, 162);
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
            btnExit.Location = new Point(24, 275);
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
            // panelMain
            // 
            panelMain.Controls.Add(btnSettings);
            panelMain.Controls.Add(label1);
            panelMain.Controls.Add(btnNewGame);
            panelMain.Controls.Add(btnExit);
            panelMain.Controls.Add(btnHistory);
            panelMain.Location = new Point(12, 12);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(272, 329);
            panelMain.TabIndex = 100;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(24, 220);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(228, 40);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // panelSettings
            // 
            panelSettings.Controls.Add(label4);
            panelSettings.Controls.Add(cBColor);
            panelSettings.Controls.Add(label3);
            panelSettings.Controls.Add(cBTimer);
            panelSettings.Controls.Add(label2);
            panelSettings.Controls.Add(buttonBack);
            panelSettings.Location = new Point(12, 12);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(272, 329);
            panelSettings.TabIndex = 19;
            panelSettings.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(24, 168);
            label4.Name = "label4";
            label4.Size = new Size(165, 25);
            label4.TabIndex = 9;
            label4.Text = "Select board color";
            // 
            // cBColor
            // 
            cBColor.Font = new Font("Segoe UI", 10F);
            cBColor.FormattingEnabled = true;
            cBColor.Items.AddRange(new object[] { "Default", "Classic", "Blue", "Green" });
            cBColor.Location = new Point(24, 193);
            cBColor.Name = "cBColor";
            cBColor.Size = new Size(228, 31);
            cBColor.TabIndex = 8;
            cBColor.Text = "Default";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(24, 90);
            label3.Name = "label3";
            label3.Size = new Size(104, 25);
            label3.TabIndex = 7;
            label3.Text = "Select time";
            // 
            // cBTimer
            // 
            cBTimer.Font = new Font("Segoe UI", 10F);
            cBTimer.FormattingEnabled = true;
            cBTimer.Items.AddRange(new object[] { "10 min", "5 min", "1 min" });
            cBTimer.Location = new Point(24, 118);
            cBTimer.Name = "cBTimer";
            cBTimer.Size = new Size(228, 31);
            cBTimer.TabIndex = 6;
            cBTimer.Text = "10 min";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 30F);
            label2.Location = new Point(33, 17);
            label2.Name = "label2";
            label2.Size = new Size(206, 67);
            label2.TabIndex = 5;
            label2.Text = "Settings";
            // 
            // buttonBack
            // 
            buttonBack.Location = new Point(24, 275);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(228, 40);
            buttonBack.TabIndex = 4;
            buttonBack.Text = "Back";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(295, 353);
            Controls.Add(panelSettings);
            Controls.Add(panelMain);
            Controls.Add(checkBox1);
            Name = "MainForm";
            Text = "MainForm";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNewGame;
        private Button btnHistory;
        private CheckBox checkBox1;
        private Button btnExit;
        private Label label1;
        private Panel panelMain;
        private Button btnSettings;
        private Panel panelSettings;
        private ComboBox cBTimer;
        private Label label2;
        private Button buttonBack;
        private Label label3;
        private ComboBox cBColor;
        private Label label4;
    }
}