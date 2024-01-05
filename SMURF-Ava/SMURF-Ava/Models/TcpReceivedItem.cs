using System;
using System.Collections.Generic;

namespace SMURF_Ava.Models;

public class TcpReceivedItem {

    public TcpReceivedItem(DateTime timeStamp, string content) {
        this.TimeStamp = timeStamp;
        this.Content = content;
    }

    public DateTime TimeStamp { get; private set; }

    public string Content{ get; private set; }

}