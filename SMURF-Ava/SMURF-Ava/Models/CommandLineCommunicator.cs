using System;
using System.Diagnostics;
using System.Threading;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class CommandLineCommunicator : AbstractEventDrivenObject {

    public CommandLineCommunicator() {
        this.INTEGRATION_EXECUTEABLE = @"\integration.exe";
        //this.INTEGRATION_EXECUTEABLE = @"\IntegrationSImulator.exe";
    }

    private readonly string INTEGRATION_EXECUTEABLE;

    public void SendCommand(UIH_Command uihCommand) {
        PublishCommandStart(uihCommand);
        string respond = "";
        Thread thread = new Thread(() => {
            try { 
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = uihCommand.ClientPath + this.INTEGRATION_EXECUTEABLE;
                processStartInfo.Arguments = uihCommand.GetIntegrationArgs();
                processStartInfo.CreateNoWindow = true;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
                int exitCode = process.ExitCode;
                respond = uihCommand.CommandName + " respond! ==> " + exitCode;
                
            }
            catch (Exception e) {
                ExceptionManager.GetInstance().ThrowException(e.ToString());
                respond = uihCommand.CommandName + " Failed!";
            }
            
            PublishRespondReceived(uihCommand, respond);
            PublishCommandFinished(uihCommand);
        });
        thread.Start();
    }

    public void PublishCommandStart(UIH_Command uihCommand) {
        PublishEvent(nameof(PublishCommandStart), uihCommand);
    }

    public void PublishCommandFinished(UIH_Command uihCommand) {
        PublishEvent(nameof(PublishCommandFinished), uihCommand);
    }

    public void PublishRespondReceived(UIH_Command uihCommand, string responds) {
        PublishEvent(nameof(PublishRespondReceived), responds);
    }
}