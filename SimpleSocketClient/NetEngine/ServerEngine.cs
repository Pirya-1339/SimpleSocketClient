using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleSocketClient;

public class ServerEngine
{
    private Socket _serverSocket;
    private IPEndPoint _ipEndPoint;
    private Socket _clientSocket;

    public ServerEngine(string ip, int port)
    {
        _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    private void Log(string msg)
    {
        Console.WriteLine($"LOG: {DateTime.Now} --- {msg}");
    }

    public void StartServer()
    {
        _serverSocket.Bind(_ipEndPoint);
        _serverSocket.Listen(1);
        Log("SERVER STARTED");
    }

    public void AcceptClient()
    {
        _clientSocket = _serverSocket.Accept();

        Log($"CLIENT ACCEPT FROM {_clientSocket.RemoteEndPoint}");
    }

    public void SendMessage(string messageToClient)
    {
        byte[] outputBytes = Encoding.Unicode.GetBytes(messageToClient);
        _clientSocket.Send(outputBytes);

        Log($"MESSAGE TO CLIENT SENT: {messageToClient}");
    }

    public string ReceiveMessage()
    {
        StringBuilder messageBuilder = new StringBuilder();
        do
        {
            byte[] inputBytes = new byte[1024];
            int countBytes = _clientSocket.Receive(inputBytes);
            messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
        } while (_clientSocket.Available > 0);

        string messageFromClient = messageBuilder.ToString();

        Log($"MESSAGE FROM CLIENT RECIEVED: {messageFromClient}");

        return messageFromClient;
    }

    public void CloseClientSocket()
    {
        _clientSocket.Shutdown(SocketShutdown.Both);
        _clientSocket.Close();
        
        Log($"CLIENT FINISHED");
    }

    public void CloseServerSocket()
    {
        _serverSocket.Close();

        Log($"SERVER FINISHED");
    }
}