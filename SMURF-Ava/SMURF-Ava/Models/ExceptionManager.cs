﻿using EventDrivenElements;

namespace SMURF_Ava.Models;

public class ExceptionManager : AbstractEventDrivenObject {

    private static ExceptionManager _instance;

    private ExceptionManager() {
        
    }

    public static ExceptionManager GetInstance() {
        if (_instance == null) {
            lock (typeof(UihCommandFactory)) {
                if (_instance == null) {
                    _instance = new ExceptionManager();
                }
            }
        }

        return _instance;
    }

    public void ThrowExceptionWithShutDown(string exceptionStr) {
        
    }

}