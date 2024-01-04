using System.Collections.Generic;

namespace SMURF_Ava.Models;

public class ResponseItemContainer {

    public ResponseItemContainer() {
        this.ResponseItemList = new List<ResponseItem>();
        this.ResponseItemSet = new HashSet<ResponseItem>();
    }

    private List<ResponseItem> ResponseItemList;

    private HashSet<ResponseItem> ResponseItemSet;

    public void AddItem(ResponseItem responseItem) {
        this.ResponseItemList.Add(responseItem);
    }

    public void RemoveItem(ResponseItem responseItem) {
        if (this.ResponseItemSet.Contains(responseItem)) {
            this.ResponseItemList.Remove(responseItem);
            this.ResponseItemSet.Remove(responseItem);
        }
    }

}