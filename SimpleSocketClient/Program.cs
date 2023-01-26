using System.Text.Json;
using SimpleSocketServer;
using SimpleSocketServer.NetEngine;
using SimpleSocketServer.NetModel;
using SimpleSocketClient;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer(); // запуск
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage(); //получение сообщения

Requset requset = JsonSerializer.Deserialize<Requset>(messageFromClient);
Response response;

if (requset.Command == Commands.AddAge)
{
    Cat cat = JsonSerializer.Deserialize<Cat>(requset.JsonData);
    cat.Age += 2;

    response = new Response()
    {
        Status = Statuses.Ok,
        JsonData = JsonSerializer.Serialize(cat)
    };
}
else
{
    response = new Response()
    {
        Status = Statuses.UnknownCommand
    };
}

string messageToClient = JsonSerializer.Serialize(response);

serverEngine.SendMessage(messageToClient);

serverEngine.CloseClientSocket();
serverEngine.CloseServerSocket();

