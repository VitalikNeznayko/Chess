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
            SuspendLayout();
            // 
            // ChessPanel
            // 
            ChessPanel.AutoSize = true;
            ChessPanel.Location = new Point(12, 12);
            ChessPanel.Name = "ChessPanel";
            ChessPanel.Size = new Size(422, 366);
            ChessPanel.TabIndex = 0;
            ChessPanel.Paint += ChessPanel_Paint;
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.Location = new Point(581, 12);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(94, 61);
            btnBackToMenu.TabIndex = 1;
            btnBackToMenu.Text = "Back To Menu ";
            btnBackToMenu.UseVisualStyleBackColor = true;
            btnBackToMenu.Click += btnBackToMenu_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(687, 388);
            Controls.Add(btnBackToMenu);
            Controls.Add(ChessPanel);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel ChessPanel;
        private Button btnBackToMenu;
    }
}
