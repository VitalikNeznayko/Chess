using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChessServer
{
    static TcpClient? whiteClient;
    static TcpClient? blackClient;
    static NetworkStream? whiteStream;
    static NetworkStream? blackStream;
    static bool isWhiteTurn = true;

    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 9999);
        listener.Start();
        Console.WriteLine("Chess server started on port 9999...");

        Console.WriteLine("Waiting for white player...");
        whiteClient = listener.AcceptTcpClient();
        whiteStream = whiteClient.GetStream();
        Send(whiteStream, "You are WHITE. Wait for opponent...");

        Console.WriteLine("Waiting for black player...");
        blackClient = listener.AcceptTcpClient();
        blackStream = blackClient.GetStream();
        Send(blackStream, "You are BLACK. Game starts!");

        Send(whiteStream, "Game starts! Your move.");
        Send(blackStream, "Wait for your turn.");

        Thread whiteThread = new Thread(() => HandlePlayer(whiteStream, blackStream, "WHITE"));
        Thread blackThread = new Thread(() => HandlePlayer(blackStream, whiteStream, "BLACK"));
        whiteThread.Start();
        blackThread.Start();
    }

    static void HandlePlayer(NetworkStream myStream, NetworkStream opponentStream, string color)
    {
        byte[] buffer = new byte[256];
        while (true)
        {
            try
            {
                int bytesRead = myStream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Console.WriteLine($"{color} move: {message}");

                bool isMyTurn = (color == "WHITE" && isWhiteTurn) || (color == "BLACK" && !isWhiteTurn);

                if (!isMyTurn)
                {
                    Send(myStream, "Not your turn.");
                    continue;
                }

                isWhiteTurn = !isWhiteTurn;

                Send(opponentStream, message);
                Send(opponentStream, "Your move.");
                Send(myStream, "Wait for your turn.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection lost with {color}. {ex.Message}");
                break;
            }
        }
    }

    static void Send(NetworkStream stream, string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message + "\n");
        stream.Write(buffer, 0, buffer.Length);
    }
}
