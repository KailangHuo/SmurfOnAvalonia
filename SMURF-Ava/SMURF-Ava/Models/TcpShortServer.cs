using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Input;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class TcpShortServer : AbstractEventDrivenViewModel{

    private static TcpShortServer _TcpShortServer;
    private TcpShortServer() {
        SetupCommands();
        ip = "127.0.0.1";
        port = 3506;
        Init();
    }

    public static TcpShortServer GetInstance() {
        if (_TcpShortServer == null) {
            lock (typeof(TcpShortServer)) {
                if (_TcpShortServer == null) {
                    _TcpShortServer = new TcpShortServer();
                }
            }
        }

        return _TcpShortServer;
    }

    private string ip;

    private int port;

    private TcpListener server;

    private string _serverLog;

    public string ServerLog {
        get  {
            return _serverLog;
        }
        set {
            _serverLog = value;
            RisePropertyChanged(nameof(ServerLog));
        }
    }

    #region COMMANDS

    public ICommand CleanLogCommand { get; set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void CleanServerLog(object o = null) {
        this.ServerLog = "";
        LogServerInfo("Listening on port: " + port);
    }

    #endregion

    public void SetupCommands() {
        this.CleanLogCommand = new CommonCommand(CleanServerLog);
    }

    public void Init() {
        if(server != null )return;
        Thread tcpServerThread = new Thread(() => {
            try {
                LogServerInfo("Initializing TCP Server...");
                Socket client = null;
                server = new TcpListener(IPAddress.Parse(ip), port);
                server.Start();
                LogServerInfo("Listening on port: " + port);
                byte[] bytes = new byte[1024 * 1024];
                do {
                    if (client == null) {
                        client = server.AcceptSocket();
                        LogServerInfo("Client Connected!");
                    }

                    int len = client.Receive(bytes);
                    if (len == 0) {
                        LogServerInfo("Client Disconnected!");
                        client.Close();
                        client = null;
                        continue;
                    }

                    string incomingStr = Encoding.UTF8.GetString(bytes, 0, len);
                    LogServerInfo("\n>>>\n" + incomingStr + "\n>>>");
                } while (true);
            }
            catch (Exception e) {
                ExceptionManager.GetInstance().ThrowException("[TcpServer]: " + e.Message);
            }
        });
        tcpServerThread.Start();
       
        
    }

    public void LogServerInfo(String s) {
        string timeStamp = DateTime.Now.ToString();
        ServerLog += "[" + timeStamp + "]" + s + "\n";
    }

}