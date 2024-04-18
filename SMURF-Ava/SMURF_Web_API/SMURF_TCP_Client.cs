using System.Net.Sockets;
using System.Text;

namespace SMURF_Web_API;

public class SMURF_TCP_Client {

    private string ipAdress;

    private int portNumber;

    public SMURF_TCP_Client(string ip, int port) {
        this.ipAdress = ip;
        this.portNumber = port;
    }

    public void Send(string content) {
        try {
            TcpClient tcpClient = new TcpClient();
            Console.WriteLine(">>> Initiated Client...");

            tcpClient.Connect(ipAdress, portNumber);
            Console.WriteLine(">>> Connected!");
            Console.WriteLine(">>> Input your msg tobe transmitted: ");
            String input = content;
            byte[] outputBytes = Encoding.UTF8.GetBytes(input);
            Console.WriteLine(">>> Sending...");
            tcpClient.Client.Send(outputBytes);
            tcpClient.Close();
        }
        catch (IOException e) {
            Console.WriteLine(e.Message);
        }
    }
}