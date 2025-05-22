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

            // Створення шахової дошки з переданим фоном
            board = new BoardPanel(60, background);
            ChessPanel.Controls.Add(board);

            // Ініціалізація часу і таймера
            InitializeTimeLeft();
            InitializeTimer();
        }

        // Встановлення стартового часу для обох гравців
        private void InitializeTimeLeft()
        {
            int minutes = GetMinutesFromSelectedTime();
            whiteTimeLeft = TimeSpan.FromMinutes(minutes);
            blackTimeLeft = TimeSpan.FromMinutes(minutes);

            // Оновлення відображення таймерів
            UpdateTimerLabels();
        }


        // Налаштування таймера, який оновлюється щосекунди
        private void InitializeTimer()
        {
            gameTimer = new WinFormsTimer
            {
                Interval = 1000
            };
            // Прив'язка обробника
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }


        // Отримання кількості хвилин згідно з вибраним режимом гри
        private int GetMinutesFromSelectedTime() => selectedTime switch
        {
            "1 min" => 1,
            "5 min" => 5,
            "10 min" => 10,
            _ => 10
        };


        // Обробник події таймера – виконується кожну секунду
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (ChessGame.Instance.GameEnded)
            {
                // Зупинити таймер, якщо гра завершена
                gameTimer.Stop();
                return;
            }

            if (UpdateTimeForCurrentPlayer())
            {
                // Зупинити таймер, якщо хтось програв по часу
                gameTimer.Stop();
                return;
            }

            // Оновлення лейблів із часом
            UpdateTimerLabels();
        }


        // Зменшення часу поточного гравця
        private bool UpdateTimeForCurrentPlayer()
        {
            ref TimeSpan currentTime = ref (ChessGame.Instance.IsWhiteTurn ? ref whiteTimeLeft : ref blackTimeLeft);
            currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));


            if (currentTime < TimeSpan.Zero)
            {
                // Якщо час вийшов — визначення переможця
                string winner = ChessGame.Instance.IsWhiteTurn ? "Black" : "White";
                ChessGame.Instance.EndGame($"{winner} won on time!", board);
                return true;
            }

            return false;
        }


        // Відображення залишку часу на екрані
        private void UpdateTimerLabels()
        {
            whiteTimerLabel.Text = whiteTimeLeft.ToString(@"mm\:ss");
            blackTimerLabel.Text = blackTimeLeft.ToString(@"mm\:ss");
        }


        // Скидання таймерів (використовується при рестарті гри)
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


        // Кнопка повернення в головне меню
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            // Скидання стану гри і зупинка таймера
            ChessGame.Instance.RestartGame();
            gameTimer.Stop();

            // Закриття вікна гри і показ головного меню
            var mainMenu = new MainForm();
            mainMenu.Show();
            Close();
        }
    }
}
