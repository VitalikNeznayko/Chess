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
            panelHistory = new Panel();
            label5 = new Label();
            listBoxLog = new ListBox();
            label7 = new Label();
            btnBack = new Button();
            panelMain.SuspendLayout();
            panelSettings.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();

            // 
            // btnNewGame
            // 

            btnNewGame.Location = new Point(26, 105);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(228, 40);
            btnNewGame.TabIndex = 0;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;

            // 
            // btnHistory
            // 

            btnHistory.Location = new Point(26, 162);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(228, 40);
            btnHistory.TabIndex = 1;
            btnHistory.Text = "History Of Games";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;

            // 
            // btnExit
            // 

            btnExit.Location = new Point(26, 275);
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
            label1.Location = new Point(43, 9);
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
            panelMain.Location = new Point(9, 7);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(272, 329);
            panelMain.TabIndex = 100;

            // 
            // btnSettings
            // 

            btnSettings.Location = new Point(26, 218);
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
            panelSettings.Location = new Point(9, 7);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(272, 329);
            panelSettings.TabIndex = 19;
            panelSettings.Visible = false;

            // 
            // label4
            // 

            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(18, 168);
            label4.Name = "label4";
            label4.Size = new Size(165, 25);
            label4.TabIndex = 9;
            label4.Text = "Select board color";

            // 
            // cBColor
            // 

            cBColor.Font = new Font("Segoe UI", 10F);
            cBColor.FormattingEnabled = true;
            cBColor.Items.AddRange(new object[] { "Default", "Classic", "Green" });
            cBColor.Location = new Point(18, 196);
            cBColor.Name = "cBColor";
            cBColor.Size = new Size(228, 31);
            cBColor.TabIndex = 8;
            cBColor.Text = "Default";

            // 
            // label3
            // 

            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(18, 90);
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
            cBTimer.Location = new Point(18, 118);
            cBTimer.Name = "cBTimer";
            cBTimer.Size = new Size(228, 31);
            cBTimer.TabIndex = 6;
            cBTimer.Text = "10 min";

            // 
            // label2
            // 

            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 30F);
            label2.Location = new Point(24, 17);
            label2.Name = "label2";
            label2.Size = new Size(206, 67);
            label2.TabIndex = 5;
            label2.Text = "Settings";

            // 
            // buttonBack
            // 

            buttonBack.Location = new Point(18, 275);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(228, 40);
            buttonBack.TabIndex = 4;
            buttonBack.Text = "Back";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;

            // 
            // panelHistory
            //
            
            panelHistory.Controls.Add(label5);
            panelHistory.Controls.Add(listBoxLog);
            panelHistory.Controls.Add(label7);
            panelHistory.Controls.Add(btnBack);
            panelHistory.Location = new Point(9, 7);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(272, 329);
            panelHistory.TabIndex = 20;
            panelHistory.Visible = false;

            // 
            // label5
            // 

            label5.AutoSize = true;
            label5.Location = new Point(24, 83);
            label5.Name = "label5";
            label5.Size = new Size(111, 20);
            label5.TabIndex = 7;
            label5.Text = "Click for details";

            // 
            // listBoxLog
            // 

            listBoxLog.FormattingEnabled = true;
            listBoxLog.Location = new Point(24, 105);
            listBoxLog.Name = "listBoxLog";
            listBoxLog.Size = new Size(219, 164);
            listBoxLog.TabIndex = 6;
            listBoxLog.SelectedIndexChanged += listBoxLog_SelectedIndexChanged;
            
            // 
            // label7
            // 

            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 30F);
            label7.Location = new Point(40, 9);
            label7.Name = "label7";
            label7.Size = new Size(185, 67);
            label7.TabIndex = 5;
            label7.Text = "History";
            
            // 
            // btnBack
            // 

            btnBack.Location = new Point(24, 275);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(219, 40);
            btnBack.TabIndex = 4;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            
            // 
            // MainForm
            // 

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 343);
            Controls.Add(panelSettings);
            Controls.Add(panelHistory);
            Controls.Add(panelMain);
            MaximumSize = new Size(306, 390);
            MinimumSize = new Size(306, 390);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
            panelHistory.ResumeLayout(false);
            panelHistory.PerformLayout();
            ResumeLayout(false);
        }


        #endregion

        private Button btnNewGame;
        private Button btnHistory;
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
        private Panel panelHistory;
        private ListBox listBoxLog;
        private Label label7;
        private Button btnBack;
        private Label label5;
    }
}