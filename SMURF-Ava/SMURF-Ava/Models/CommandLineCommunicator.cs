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

    public async void SendCommand(Uuu_Command uuuCommand) {
        uuuCommand.CommandHeader = this.INTEGRATION_EXECUTABLE;
        PublishCommandStart(uuuCommand);
        string respond = "";
        Process process = null;
        try {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = uuuCommand.ClientPath + @"\" +this.INTEGRATION_EXECUTABLE;
            processStartInfo.Arguments = uuuCommand.CommandBody;
            processStartInfo.CreateNoWindow = true;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            uuuCommand.CancellationTokenSource = cancellationTokenSource;
            process = Process.Start(processStartInfo);
            await process.WaitForExitAsync(cancellationTokenSource.Token);
            int exitCode = process.ExitCode;
            string codeInterpretation = SystemConfiguration.GetInstance().GetCmdlineResultByCodeStr(exitCode + "");
            respond = uuuCommand.CommandName + " respond! >> " + exitCode + " : " + codeInterpretation;
            PublishRespondReceived(respond);
            PublishCommandFinished(uuuCommand);
        }
        catch (Exception e) {
            if (e is OperationCanceledException) {
                respond = uuuCommand.CommandName + " Cancelled! >> " + "用户取消了监听命令回执";
            }else {
                respond = uuuCommand.CommandName + " Failed!";
                ExceptionManager.GetInstance().ThrowException(e.ToString());
            }

            process?.Kill();
            PublishRespondReceived(respond);
            PublishCommandFinished(uuuCommand); 
        }
    }

    public void PublishCommandStart(Uuu_Command uuuCommand) {
        PublishEvent(nameof(PublishCommandStart), uuuCommand);
    }

    public void PublishCommandFinished(Uuu_Command uuuCommand) {
        PublishEvent(nameof(PublishCommandFinished), uuuCommand);
    }

    public void PublishRespondReceived(string responds) {
        PublishEvent(nameof(PublishRespondReceived), responds);
    }
    
}