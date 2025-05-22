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
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Game Result: {resultMessage}";

                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging error: {ex.Message}");
            }
        }
    }
}