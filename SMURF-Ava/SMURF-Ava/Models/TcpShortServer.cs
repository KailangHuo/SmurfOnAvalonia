using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Input;
using EventDrivenElements;
using Newtonsoft.Json;

namespace SMURF_Ava.Models;

public class TcpShortServer : AbstractEventDrivenObject{

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

    #region COMMANDS

    public ICommand CleanLogCommand { get; set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void CleanServerLog(object o = null) {
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
                string incomingStr = "";
                do {
                    if (client == null) {
                        client = server.AcceptSocket();
                        //LogServerInfo("Client Connected!");
                    }

                    byte[] bytes = new byte[1024 * 1024];
                    int len = client.Receive(bytes);
                    if (len == 0) {
                        //LogServerInfo("Client Disconnected!");
                        LogServerInfo("Software response received!");
                        TcpReceivedItem tcpReceivedItem = new TcpReceivedItem(DateTime.Now, incomingStr);
                        ResponseReceived(tcpReceivedItem);
                        client.Close();
                        client = null;
                        incomingStr = "";
                        continue;
                    }

                    incomingStr += Encoding.UTF8.GetString(bytes, 0, len);
                } while (true);
            }
            catch (Exception e) {
                ExceptionManager.GetInstance().ThrowException("[TcpServer]: " + e.Message);
            }
        });
        tcpServerThread.Start();
       
        
    }

    private void LogServerInfo(string content) {
        SystemLogger.GetInstance().UpdateLog(content, LogTypeEnum.SYSTEM_NOTIFICATION, content);
    }

    public void ResponseReceived(TcpReceivedItem tcpReceivedItem) {
        PublishEvent(nameof(ResponseReceived), tcpReceivedItem);
    }

}