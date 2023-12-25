using EventDrivenElements;

namespace SMURF_Ava.ViewModels;

public class SystemLogManager_ViewModel : AbstractEventDrivenViewModel{

    public SystemLogManager_ViewModel() {
        
    }

    private string logInfo;

    public string LogInfo {
        get {
            return logInfo;
        }
        set {
            if(logInfo == value)return;
            logInfo = value;
            RisePropertyChanged(nameof(logInfo));
        }
    }



}