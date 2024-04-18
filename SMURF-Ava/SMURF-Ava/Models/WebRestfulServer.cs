using System.Diagnostics;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class WebRestfulServer : AbstractEventDrivenObject{
    
    
    public WebRestfulServer() {
        
    }

    private int port;

    private void SetUpServerPort(int port) {
        if(port == 0) return;
        this.port = port;

        string restApiExecutablePath = System.Environment.CurrentDirectory + @"/SMURF_Web_API.exe";
        
        // 创建一个新的进程启动配置
        var startInfo = new ProcessStartInfo(restApiExecutablePath)
        {
            Arguments = " " + port, // 传递任何所需的命令行参数
            UseShellExecute = false,   // 是否使用操作系统的shell启动进程
            RedirectStandardOutput = true, // 是否获取程序输出
            //CreateNoWindow = true // 是否在新窗口中启动程序
        };

        //Process.Start(startInfo);
        ExceptionManager.GetInstance().ThrowException("restApi 已经启动了!");

    }


    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(TcpShortServer.PublishServerPort))) {
            int port = (int)o;
            this.SetUpServerPort(port);
            return;
        }
    }
}