using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
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
        ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(c => c
                .Server("10.6.128.17")
                .Database("studytable")
                .Username("root")
                .Password("KqCUydwe7M#f")))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
            .BuildSessionFactory();

        this.SessionFactory = sessionFactory;
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

    public List<Study_DB_Item> GetRandomStudy(int nums) {
        using (ISession session = this.SessionFactory.OpenSession()) {
            using (ITransaction transaction = session.BeginTransaction()) {
                try {
                    return session.Query<Study_DB_Item>()
                        .Take(nums)
                        .ToList();
                }
                catch (Exception e) {
                    ExceptionManager.GetInstance().ThrowException("GetRandomStudy failed! => " + e.ToString());
                }
            }

        }

        return null;

    }

}