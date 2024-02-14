using EventDrivenElements;

namespace SMURF_Ava.Models;

public class ExceptionManager : AbstractEventDrivenObject {

    private static ExceptionManager _instance;

    private ExceptionManager() {
        
    }

    public static ExceptionManager GetInstance() {
        if (_instance == null) {
            lock (typeof(UuuCommandFactory)) {
                if (_instance == null) {
                    _instance = new ExceptionManager();
                }
            }
        }

        return _instance;
    }

    public void ThrowException(string exceptionStr) {
        PublishEvent(nameof(ThrowException), exceptionStr);
    }

}