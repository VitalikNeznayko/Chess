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
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            label3 = new Label();
            panel2 = new Panel();
            label4 = new Label();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 614);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 2;
            label1.Text = "Гравець 1";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(78, 20);
            label2.TabIndex = 3;
            label2.Text = "Гравець 2";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(label3);
            panel1.Location = new Point(465, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(95, 40);
            panel1.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(18, 5);
            label3.Name = "label3";
            label3.Size = new Size(60, 28);
            label3.TabIndex = 0;
            label3.Text = "00:00";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(label4);
            panel2.Location = new Point(465, 605);
            panel2.Name = "panel2";
            panel2.Size = new Size(95, 40);
            panel2.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(18, 5);
            label4.Name = "label4";
            label4.Size = new Size(60, 28);
            label4.TabIndex = 0;
            label4.Text = "00:00";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(687, 655);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label1);
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
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Label label3;
        private Panel panel2;
        private Label label4;
    }
}
