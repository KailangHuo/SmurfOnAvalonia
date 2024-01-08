using System;
using System.Collections.Generic;
using EventDrivenElements;
using Newtonsoft.Json;

namespace SMURF_Ava.Models;

public class ResponseItemManager : AbstractEventDrivenObject{

    public ResponseItemManager() {
        this.ResponseItemList = new List<ResponseItem>();
        this.ResponseItemSet = new HashSet<ResponseItem>();
    }

    private List<ResponseItem> ResponseItemList;

    private HashSet<ResponseItem> ResponseItemSet;

    public void AddItem(ResponseItem responseItem) {
        this.ResponseItemList.Add(responseItem);
        PublishEvent(nameof(AddItem), responseItem);
    }

    public void RemoveItem(ResponseItem responseItem) {
        if (this.ResponseItemSet.Contains(responseItem)) {
            this.ResponseItemList.Remove(responseItem);
            this.ResponseItemSet.Remove(responseItem);
        }
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(TcpShortServer.ResponseReceived))) {
            TcpReceivedItem tcpReceivedItem = (TcpReceivedItem)o;
            string receivedContentJsonStr = tcpReceivedItem.Content;
            ResponseItem responseItem = new ResponseItem();
            try {
                responseItem = JsonConvert.DeserializeObject<ResponseItem>(receivedContentJsonStr);
                ResponseItemStatusParamObject statusParamObject = string.IsNullOrEmpty(responseItem.StatusParam)
                    ? null
                    : JsonConvert.DeserializeObject<ResponseItemStatusParamObject>(responseItem.StatusParam);
                responseItem.SetResponseStatusParam(statusParamObject);
            }
            catch (Exception e) {
                if (receivedContentJsonStr != ">>> Hello World!") {
                    responseItem.SetResponseStatusParam(null);
                }
            }
            responseItem.SetRawContent(receivedContentJsonStr); 
            responseItem.SetTimeStamp(tcpReceivedItem.TimeStamp);
            this.AddItem(responseItem); 
            return;
        }
    }
}