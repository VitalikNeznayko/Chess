using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        public ChessClientForm()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            try
            {
                await client.ConnectAsync("127.0.0.1", 8888);
                stream = client.GetStream();
                _ = Task.Run(ReceiveMovesAsync); // фоновий прийом ходів
                MessageBox.Show("Підключено до сервера");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення: {ex.Message}");
            }
        }

        private async Task ReceiveMovesAsync()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                int byteCount;
                try
                {
                    byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (byteCount == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    Invoke(new Action(() =>
                    {
                        ProcessOpponentMove(message);
                    }));
                }
                catch
                {
                    break;
                }
            }
        }

        private void ProcessOpponentMove(string move)
        {
            // Наприклад: "e2e4"
            MessageBox.Show($"Отримано хід: {move}");
            // Тут вставляється код для виконання ходу супротивника на дошці
        }

        public async void SendMove(string move)
        {
            if (stream == null || !stream.CanWrite) return;

            byte[] data = Encoding.UTF8.GetBytes(move);
            await stream.WriteAsync(data, 0, data.Length);
        }

        private void ChessClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream?.Close();
            client?.Close();
        }
    }
}
