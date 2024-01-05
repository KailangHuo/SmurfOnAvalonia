using System.Collections.Generic;

namespace SMURF_Ava.Models;

public class ResponseItemStatusParamObject {

    public List<string> ImageList;

    public List<string> StudyUIDList;

    public string SourceApp;

    public string Application;

    public string WindowState;

    public override string ToString() {
        string s = "{ \n";
        
        if(!string.IsNullOrEmpty(SourceApp))s += "SourceApp: " + SourceApp + "\n";
        if(ImageList.Count > 0)s += "ImageList (count): " + ImageList.Count + "\n";
        if (StudyUIDList.Count > 0) {
            s += "StudyUIDList: ";
            for (int i = 0; i < StudyUIDList.Count; i++) {
                if (i > 0) s += ", ";
                s += StudyUIDList[i];
            }

            s += "\n";
        }

        if (!string.IsNullOrEmpty(Application)) s += "Application: " + Application + "\n";
        if (!string.IsNullOrEmpty(WindowState)) s += "WindowState: " + WindowState + "\n";
        s += "}";
        return s;
    }
}