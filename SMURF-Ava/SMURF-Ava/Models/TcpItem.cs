using System;
using EventDrivenElements;

namespace SMURF_Ava.Models;

public class TcpItem : AbstractEventDrivenObject{

    public TcpItem(string content) {
        this.TimeStamp = DateTime.Now.ToString();
    }

    public string TimeStamp;

    public string Guid;

    public string StatusType;

    public TcpItemStatusParam StatusParam;



}