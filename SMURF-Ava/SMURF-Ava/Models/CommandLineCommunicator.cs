using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EventDrivenElements;
using SMURF_Ava.configuration;
using SMURF_Ava.ViewModels;

namespace SMURF_Ava.Models;

public class CommandLineCommunicator : AbstractEventDrivenObject {

    public CommandLineCommunicator() {
        this.INTEGRATION_EXECUTABLE = "integration.exe";
        //this.INTEGRATION_EXECUTEABLE = @"E:\GitHub\IntegrationExe\IntegrationSImulator\IntegrationSImulator\bin\Debug\net7.0\IntegrationSImulator.exe";
    }

    private readonly string INTEGRATION_EXECUTABLE;

    public async void SendCommand(UIH_Command uihCommand) {
        uihCommand.CommandHeader = this.INTEGRATION_EXECUTABLE;
        PublishCommandStart(uihCommand);
        string respond = "";
        Process process = null;
        try {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = uihCommand.ClientPath + @"\" +this.INTEGRATION_EXECUTABLE;
            processStartInfo.Arguments = uihCommand.CommandBody;
            processStartInfo.CreateNoWindow = true;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            uihCommand.CancellationTokenSource = cancellationTokenSource;
            process = Process.Start(processStartInfo);
            await process.WaitForExitAsync(cancellationTokenSource.Token);
            int exitCode = process.ExitCode;
            string codeInterpretation = SystemConfiguration.GetInstance().GetCmdlineResultByCodeStr(exitCode + "");
            respond = uihCommand.CommandName + " respond! >> " + exitCode + " : " + codeInterpretation;
            PublishRespondReceived(respond);
            PublishCommandFinished(uihCommand);
        }
        catch (Exception e) {
            if (e is OperationCanceledException) {
                respond = uihCommand.CommandName + " Cancelled! >> " + "用户取消了监听命令回执";
            }else {
                respond = uihCommand.CommandName + " Failed!";
                ExceptionManager.GetInstance().ThrowException(e.ToString());
            }

            process?.Kill();
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