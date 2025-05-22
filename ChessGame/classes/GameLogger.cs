using System;
using System.IO;

namespace ChessGame
{
    public class GameLogger
    {
        private readonly string _logFilePath; 

        public GameLogger(string logFilePath = "chess_game_log.txt")
        {
            _logFilePath = logFilePath;
        }

        // Метод для запису результату гри у файл логу
        public void LogGameResult(string resultMessage)
        {
            try
            {
                // Формування рядка з результатом гри та поточною датою/часом
                string logEntry = $"Game result: {resultMessage} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                
                // Додавання запису у файл логу
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging error: {ex.Message}");
            }
        }

        // Метод для зчитування всіх записів з журналу
        public List<string> ReadLogEntries()
        {
            try
            {
                // Перевірка, чи існує файл журналу
                if (File.Exists(_logFilePath))
                {
                    // Зчитування усіх рядків з файлу і перетворення у список
                    return File.ReadAllLines(_logFilePath).ToList();
                }
            }
            catch (Exception ex)
            {
                // Виведення повідомлення про помилку у консоль
                Console.WriteLine($"Read error: {ex.Message}");
            }

            // Повернення порожнього списку у разі помилки або відсутності файлу
            return new List<string>(); 
        }

    }
}