using EventDrivenElements;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using SMURF_Ava.Models.DatabaseMappingItems;
using SMURF_Ava.Models.DatabaseMappingItems.MappingClasses;

namespace SMURF_Ava.Models;

public class DatabaseConnector : AbstractEventDrivenObject {

    private static DatabaseConnector _instance;

    private DatabaseConnector() {
        ConnectionString = "Server=10.6.128.17;Database=studytable;User Id=root;Password=KqCUydwe7M#f";
        InitSessionFactory();
    }

    public static DatabaseConnector GetInstance() {
        if (_instance == null) {
            lock (typeof(DatabaseConnector)) {
                if (_instance == null) {
                    _instance = new DatabaseConnector();
                }
            }
        }

        return _instance;
    }

    private string ConnectionString;

    private ISessionFactory SessionFactory;

    private void InitSessionFactory() {
        if(SessionFactory != null)return;
        SessionFactory = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<App>())
            .BuildSessionFactory();
    }

    public Study_DB_Item GetStudyByUid(string uid) {
        Study_DB_Item 
        using (ISession session = this.SessionFactory.OpenSession()) {
            string studyInstanceUID = uid;
            var cretaria = session.CreateCriteria<Study_Mapper>()
                .Add(Restrictions.Eq("StudyInstanceUID", studyInstanceUID));
            var studyItems = cretaria.List<Study_Mapper>();
        }
        
    }

}