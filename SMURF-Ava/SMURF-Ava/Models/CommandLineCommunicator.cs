using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EventDrivenElements;
using SMURF_Ava.configuration;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava.Models;

public class CommandLineCommunicator : AbstractEventDrivenObject {

    public CommandLineCommunicator() {
        this.INTEGRATION_EXECUTEABLE = "integration.exe";
        //this.INTEGRATION_EXECUTEABLE = @"E:\GitHub\IntegrationExe\IntegrationSImulator\IntegrationSImulator\bin\Debug\net7.0\IntegrationSImulator.exe";
    }

    private readonly string INTEGRATION_EXECUTEABLE;

    public void SendCommand(UIH_Command uihCommand) {
        uihCommand.CommandHeader = this.INTEGRATION_EXECUTEABLE;
        PublishCommandStart(uihCommand);
        string respond = "";
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = uihCommand.ClientPath + @"\" +this.INTEGRATION_EXECUTEABLE;
        processStartInfo.Arguments = uihCommand.CommandBody;
        processStartInfo.CreateNoWindow = true;
        Process process = Process.Start(processStartInfo);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        uihCommand.CancellationTokenSource = cancellationTokenSource;
        try {
            Task.Run(() => {
                process.WaitForExit();
                int exitCode = process.ExitCode;
                if (cancellationTokenSource.Token.IsCancellationRequested) {
                    exitCode = -111;
                }
                string codeInterpretation = SystemConfiguration.GetInstance().GetCmdlineResultByCodeStr(exitCode + "");
                respond = uihCommand.CommandName + " respond! >> " + exitCode + " : " + codeInterpretation;
                PublishRespondReceived(respond);
                PublishCommandFinished(uihCommand);
            }, cancellationTokenSource.Token);
        }
        catch (Exception e) {
            if (e is OperationCanceledException) {
                respond = uihCommand.CommandName + " Cancelled! >> " + "用户取消了监听命令回执";
                
            }
            else {
                respond = uihCommand.CommandName + " Failed!";
                ExceptionManager.GetInstance().ThrowException(e.ToString());
            }

            process.Kill();
            PublishRespondReceived(respond);
            PublishCommandFinished(uihCommand); 
        }
        
    }

    public void PublishCommandStart(UIH_Command uihCommand) {
        PublishEvent(nameof(PublishCommandStart), uihCommand);
    }

    public void PublishCommandFinished(UIH_Command uihCommand) {
        PublishEvent(nameof(PublishCommandFinished), uihCommand);
    }

    public void PublishRespondReceived(string responds) {
        PublishEvent(nameof(PublishRespondReceived), responds);
    }
    
}