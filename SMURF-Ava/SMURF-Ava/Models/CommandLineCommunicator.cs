using System;
using System.Diagnostics;
using System.Threading;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class CommandLineCommunicator : AbstractEventDrivenObject {

    public CommandLineCommunicator() {
        this.INTEGRATION_EXECUTEABLE = @"\integration.exe";
    }

    private readonly string INTEGRATION_EXECUTEABLE;

    public void SendCommand(UIH_Command uihCommand) {
        PublishCommandStart(uihCommand);
        int exitCode = 0;
        Thread thread = new Thread(() => {
            try { 
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = uihCommand.ClientPath + this.INTEGRATION_EXECUTEABLE;
                processStartInfo.Arguments = uihCommand.GetIntegrationArgs();
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
                exitCode = process.ExitCode;
                
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            
            PublishRespondReceived(uihCommand, exitCode);
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

    public void PublishRespondReceived(UIH_Command uihCommand, int respondNumber) {
        string respond = "";
        respond += uihCommand.CommandName + " => result:" + respondNumber;
        PublishEvent(nameof(PublishRespondReceived), respond);
    }
}