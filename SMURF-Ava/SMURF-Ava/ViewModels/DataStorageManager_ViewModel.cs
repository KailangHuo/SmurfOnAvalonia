using System.IO;
using EventDrivenElements;
using Newtonsoft.Json;

namespace SMURF_Ava.ViewModels;

public class DataStorageManager_ViewModel : AbstractEventDrivenViewModel {

    private static DataStorageManager_ViewModel _instance;

    private DataStorageManager_ViewModel() {
        this._fileName = "MetaDataJSON.json";
    }

    public static DataStorageManager_ViewModel GetInstance() {
        if (_instance == null) {
            lock (typeof(DataStorageManager_ViewModel)) {
                if (_instance == null) {
                    _instance = new DataStorageManager_ViewModel();
                }
            }
        }

        return _instance;
    }

    private string _fileName;

    public void SaveToFile(MetaDataObject metaDataObject) {
        string filePath = System.Environment.CurrentDirectory + "/" + _fileName;
        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }

        string json = JsonConvert.SerializeObject(metaDataObject);
        File.WriteAllText(filePath, json);
    }
    
    public MetaDataObject ReadFromFile() {
        string filePath = System.Environment.CurrentDirectory + "/" + _fileName;
        if(!File.Exists(filePath)) return null;
        string json = File.ReadAllText(filePath);
        MetaDataObject dto = JsonConvert.DeserializeObject<MetaDataObject>(json);
        return dto;
    }


}