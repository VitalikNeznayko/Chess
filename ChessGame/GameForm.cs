using ChessGame.Classes;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace ChessGame
{
    public partial class GameForm : Form
    {
        private WinFormsTimer gameTimer;
        private TimeSpan whiteTimeLeft;
        private TimeSpan blackTimeLeft;
        private string selectedTime; 
        private BoardPanel board;

        public GameForm(string selectedTime)
        {
            InitializeComponent();
            board = new BoardPanel();
            ChessPanel.Controls.Add(board);

            this.selectedTime = selectedTime; 

            gameTimer = new WinFormsTimer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            int minutes = GetMinutesFromSelectedTime();

            whiteTimeLeft = TimeSpan.FromMinutes(minutes);
            blackTimeLeft = TimeSpan.FromMinutes(minutes);

            UpdateTimerLabels();
            gameTimer.Start();
        }
        private int GetMinutesFromSelectedTime()
        {
            return selectedTime switch
            {
                "1 хв" => 1,
                "5 хв" => 5,
                "10 хв" => 10, 
                _ => 10
            };
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (ChessGame.Instance.GameEnded)
            {
                gameTimer.Stop();
                return;
            }
            if (ChessGame.Instance.IsWhiteTurn)
            {
                whiteTimeLeft = whiteTimeLeft.Subtract(TimeSpan.FromSeconds(1));
                if (whiteTimeLeft < TimeSpan.Zero)
                {
                    ChessGame.Instance.EndGame("Чорні перемогли за часом!", board);
                    return;
                }
            }
            else
            {
                blackTimeLeft = blackTimeLeft.Subtract(TimeSpan.FromSeconds(1));
                if (blackTimeLeft < TimeSpan.Zero)
                {
                    ChessGame.Instance.EndGame("Білі перемогли за часом!", board);
                    return;
                }
            }
            UpdateTimerLabels();

        }

        private void UpdateTimerLabels()
        {
            whiteTimerLabel.Text = whiteTimeLeft.ToString(@"mm\:ss");
            blackTimerLabel.Text = blackTimeLeft.ToString(@"mm\:ss");
        }
        public void ResetTimers()
        {
            int minutes = GetMinutesFromSelectedTime();

            whiteTimeLeft = TimeSpan.FromMinutes(minutes);
            blackTimeLeft = TimeSpan.FromMinutes(minutes);
            UpdateTimerLabels();
            gameTimer.Start();
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
            this.Close();
        }
    }
}