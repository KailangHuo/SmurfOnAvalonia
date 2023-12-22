using EventDrivenElements;

namespace SMURF_Ava.ViewModels;

public class UserAccountViewModel : AbstractEventDrivenViewModel{

    public UserAccountViewModel() {
        
    }

    private string _userName;

    public string UserName {
        get {
            return _userName;
        }
        set {
            if(_userName == value)return;
            _userName = value;
            RisePropertyChanged(nameof(UserName));
        }
    }
    
    private string _password;

    public string Password {
        get {
            return _password;
        }
        set {
            if(_password == value)return;
            _password = value;
            RisePropertyChanged(nameof(Password));
        }
    }
    
    private string _domainUrl;

    public string DomainUrl {
        get {
            return _domainUrl;
        }
        set {
            if(_domainUrl == value)return;
            _domainUrl = value;
            RisePropertyChanged(nameof(DomainUrl));
        }
    }

}