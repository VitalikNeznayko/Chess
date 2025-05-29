
using ClosedXML.Excel;

namespace Chess
{
    public class GameLogger
    {
        private readonly string _excelFilePath;

        public GameLogger(string excelFilePath = "logs/chess_game_log.xlsx")
        {
            _excelFilePath = excelFilePath;

            string? dir = Path.GetDirectoryName(_excelFilePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(_excelFilePath))
            {
                CreateExcelFileWithHeader();
            }
        }

        private void CreateExcelFileWithHeader()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("GameLog");

            worksheet.Cell(1, 1).Value = "Date & Time";
            worksheet.Cell(1, 2).Value = "Result";

            // Стилі для заголовка
            var headerRange = worksheet.Range("A1:B1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(_excelFilePath);
        }

        public void LogGameResult(string resultMessage)
        {
            try
            {
                using var workbook = new XLWorkbook(_excelFilePath);
                var worksheet = workbook.Worksheet("GameLog");

                int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;
                int newRow = lastRow + 1;

                worksheet.Cell(newRow, 1).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cell(newRow, 2).Value = resultMessage;

                // Стилізація нового рядка
                var rowRange = worksheet.Range($"A{newRow}:B{newRow}");
                rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Альтернативне забарвлення рядків (зебра)
                if (newRow % 2 == 0)
                    rowRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                worksheet.Columns().AdjustToContents();
                workbook.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excel logging error: {ex.Message}");
            }
        }

        public List<string> ReadLogEntries()
        {
            var entries = new List<string>();
            try
            {
                using var workbook = new XLWorkbook(_excelFilePath);
                var worksheet = workbook.Worksheet("GameLog");

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    string timestamp = row.Cell(1).GetValue<string>();
                    string result = row.Cell(2).GetValue<string>();
                    entries.Add($"[{timestamp}] {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excel read error: {ex.Message}");
            }

            return entries;
        }
    }
}
