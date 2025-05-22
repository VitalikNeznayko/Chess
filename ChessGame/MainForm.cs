using ChessGame.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            var gameForm = new GameForm(SelectedTimerIndex, SelectedColor);
            gameForm.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            panelMain.Hide();
            panelSettings.Show();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функція історії ще в розробці.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public string SelectedTimerIndex => cBTimer.Text;
        public string SelectedColor => cBColor.Text;
        private void buttonBack_Click(object sender, EventArgs e)
        {
            panelSettings.Hide();
            panelMain.Show();
        }
    }
}
