using System;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class ResponseItem : AbstractEventDrivenObject{

    public ResponseItem(string content) {
        this.TimeStamp = DateTime.Now.ToString();
    }

    public string TimeStamp;

    public string Guid;

    public string StatusType;

    public string StatusParam;



}