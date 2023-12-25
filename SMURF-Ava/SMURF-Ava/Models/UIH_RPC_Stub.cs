using EventDrivenElements;

namespace SMURF_Ava.Models;

public class UIH_RPC_Stub : AbstractEventDrivenObject {

    private static UIH_RPC_Stub _instance;

    private UIH_RPC_Stub() {
        
    }

    public static UIH_RPC_Stub GetInstance() {
        if (_instance == null) {
            lock (typeof(UIH_RPC_Stub)) {
                if (_instance == null) {
                    _instance = new UIH_RPC_Stub();
                }
            }
        }

        return _instance;
    }
    
    

}