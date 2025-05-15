using ChessGame.Classes;

namespace ChessGame
{
    public partial class GameForm : Form
    {
        public GameForm()
        {

            InitializeComponent();
            BoardPanel board = new BoardPanel();
            ChessPanel.Controls.Add(board);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void ChessPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            var mainMenu = new MainForm();
            mainMenu.Show();
            this.Hide();
        }
    }
}
