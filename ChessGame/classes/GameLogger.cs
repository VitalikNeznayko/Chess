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

        public void LogGameResult(string resultMessage)
        {
            try
            {
                string logEntry = $"Game result: {resultMessage} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging error: {ex.Message}");
            }
        }

        public List<string> ReadLogEntries()
        {
            try
            {
                if (File.Exists(_logFilePath))
                {
                    return File.ReadAllLines(_logFilePath).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Read error: {ex.Message}");
            }

            return new List<string>(); 
        }

    }
}