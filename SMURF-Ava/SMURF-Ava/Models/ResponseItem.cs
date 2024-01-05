using System;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class ResponseItem : AbstractEventDrivenObject{

    public ResponseItem() {
        
    }

    public DateTime TimeStamp { get; private set; }

    public string RawContent { get; private set; }

    public string Guid;

    public string StatusType;

    public string StatusParam;

    public ResponseItemStatusParamObject ResponseItemStatusParamObject { get;private set; }

    public void SetResponseStatusParam(ResponseItemStatusParamObject o) {
        this.ResponseItemStatusParamObject = o;
    }

    public void SetTimeStamp(DateTime dateTime) {
        this.TimeStamp = dateTime;
    }

    public void SetRawContent(string jsonString) {
        this.RawContent = jsonString;
    }

}