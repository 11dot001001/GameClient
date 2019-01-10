using System;
using System.Collections.Generic;

public static class Network
{
    private static Client _client;

    public static event Action Connected;
    public static event Action Disconnected;
    public static event Action FailedToConnect;

    public static void IsEmailExists(string email) => _client.TCPCall(_client.IsEmailExists, email);
    public static void IsNicknameExists(string nickname) => _client.TCPCall(_client.IsNicknameExists, nickname);
    public static void GetOtherAccount(int id) => _client.TCPCall(_client.GetOtherAccount, id);
    public static void SignUp(string email, string password, string nickname) => _client.TCPCall(_client.SignUp, email, password, nickname);
    public static void LogIn(string email, string password) => _client.TCPCall(_client.LogIn, email, password);
    public static void FindGame() => _client.TCPCall(_client.FindGame);
    public static void RequestSendViruses(IEnumerable<int> bacteriumsFrom, int bacteriumTo) => _client.TCPCall(_client.RequestSendViruses, bacteriumsFrom, bacteriumTo);

    public static void Initialize(string ipAddress, int port)
    {
        _client = new Client(ipAddress, port);
        _client.Connected += Client_Connected;
        _client.Disconnected += Client_Disconnected;
        _client.FailedToConnect += Client_FailedToConnect;
    }
    public static void Start() => _client.Start();
    public static void Stop() => _client.Stop();

    private static void Client_FailedToConnect() => FailedToConnect?.Invoke();
    private static void Client_Disconnected() => Disconnected?.Invoke();
    private static void Client_Connected() => Connected?.Invoke();
}