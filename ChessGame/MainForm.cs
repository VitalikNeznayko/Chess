using Chess;
using Chess.Classes;
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

        // Обробник події кнопки "Нова гра"
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            // Створюємо нову форму гри з вибраними параметрами
            var gameForm = new GameForm(SelectedTimerIndex, SelectedColor);
            gameForm.Show();
            this.Hide();
        }

        // Обробник події кнопки "Налаштування"
        private void btnSettings_Click(object sender, EventArgs e)
        {
            SwitchPanel(panelSettings, panelMain);

        }

        // Обробник події кнопки "Історія"
        private void btnHistory_Click(object sender, EventArgs e)
        {
            SwitchPanel(panelHistory, panelMain);

        }

        // Обробник події кнопки "Вихід"
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Властивість для отримання вибраного часу таймера
        public string SelectedTimerIndex => cBTimer.Text;


        // Властивість для отримання вибраного кольору гравця
        public string SelectedColor => cBColor.Text;


        // Обробник кнопки "Назад" з панелі налаштувань
        private void buttonBack_Click(object sender, EventArgs e)
        {
            SwitchPanel(panelMain, panelSettings);

        }

        // Завантаження журналу ігор у список
        private void LoadLogToListBox()
        {
            var logger = new GameLogger();

            // Зчитування записів
            List<string> entries = logger.ReadLogEntries();

            // Очищення списку
            listBoxLog.Items.Clear();
            foreach (var entry in entries)
            {
                // Додавання кожного запису
                listBoxLog.Items.Add(entry);
            }
        }

        // Обробник завантаження головної форми
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Завантаження логів при запуску форми
            LoadLogToListBox();
        }

        // Обробка вибору елемента у списку логів
        private void listBoxLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLog.SelectedItem != null)
            {
                string selectedLog = listBoxLog.SelectedItem.ToString();
                // Показ повідомлення з деталями лог-файлу
                MessageBox.Show(selectedLog, "Log Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Метод перемикання панелей
        private void SwitchPanel(Panel toShow, Panel toHide)
        {
            // Сховати попередню панель
            toHide.Hide();
            // Показати нову панель
            toShow.Show();
        }

        // Обробник кнопки "Назад" з панелі історії
        private void btnBack_Click(object sender, EventArgs e)
        {
            SwitchPanel(panelMain, panelHistory);

        }

    }
}
