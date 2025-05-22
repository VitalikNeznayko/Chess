using ChessGame.Classes;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace ChessGame
{
    public partial class GameForm : Form
    {
        private WinFormsTimer gameTimer;

        private TimeSpan whiteTimeLeft;
        private TimeSpan blackTimeLeft;

        private readonly string selectedTime;

        private BoardPanel board;

        public GameForm(string selectedTime, string background)
        {
            InitializeComponent();
            this.selectedTime = selectedTime;

            // ��������� ������ ����� � ��������� �����
            board = new BoardPanel(60, background);
            ChessPanel.Controls.Add(board);

            // ����������� ���� � �������
            InitializeTimeLeft();
            InitializeTimer();
        }

        // ������������ ���������� ���� ��� ���� �������
        private void InitializeTimeLeft()
        {
            int minutes = GetMinutesFromSelectedTime();
            whiteTimeLeft = TimeSpan.FromMinutes(minutes);
            blackTimeLeft = TimeSpan.FromMinutes(minutes);

            // ��������� ����������� �������
            UpdateTimerLabels();
        }


        // ������������ �������, ���� ����������� ���������
        private void InitializeTimer()
        {
            gameTimer = new WinFormsTimer
            {
                Interval = 1000
            };
            // ����'���� ���������
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }


        // ��������� ������� ������ ����� � �������� ������� ���
        private int GetMinutesFromSelectedTime() => selectedTime switch
        {
            "1 min" => 1,
            "5 min" => 5,
            "10 min" => 10,
            _ => 10
        };


        // �������� ��䳿 ������� � ���������� ����� �������
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (ChessGame.Instance.GameEnded)
            {
                // �������� ������, ���� ��� ���������
                gameTimer.Stop();
                return;
            }

            if (UpdateTimeForCurrentPlayer())
            {
                // �������� ������, ���� ����� ������� �� ����
                gameTimer.Stop();
                return;
            }

            // ��������� ������ �� �����
            UpdateTimerLabels();
        }


        // ��������� ���� ��������� ������
        private bool UpdateTimeForCurrentPlayer()
        {
            ref TimeSpan currentTime = ref (ChessGame.Instance.IsWhiteTurn ? ref whiteTimeLeft : ref blackTimeLeft);
            currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));


            if (currentTime < TimeSpan.Zero)
            {
                // ���� ��� ������ � ���������� ���������
                string winner = ChessGame.Instance.IsWhiteTurn ? "Black" : "White";
                ChessGame.Instance.EndGame($"{winner} won on time!", board);
                return true;
            }

            return false;
        }


        // ³���������� ������� ���� �� �����
        private void UpdateTimerLabels()
        {
            whiteTimerLabel.Text = whiteTimeLeft.ToString(@"mm\:ss");
            blackTimerLabel.Text = blackTimeLeft.ToString(@"mm\:ss");
        }


        // �������� ������� (��������������� ��� ������� ���)
        public void ResetTimers()
        {
            InitializeTimeLeft();
            gameTimer.Start();
        }


        private void MainForm_Load(object sender, EventArgs e) 
        {

        }


        private void ChessPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        // ������ ���������� � ������� ����
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            // �������� ����� ��� � ������� �������
            ChessGame.Instance.RestartGame();
            gameTimer.Stop();

            // �������� ���� ��� � ����� ��������� ����
            var mainMenu = new MainForm();
            mainMenu.Show();
            Close();
        }
    }
}
