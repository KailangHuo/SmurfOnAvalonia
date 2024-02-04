using FluentNHibernate.Mapping;
using NHibernate.Mapping;

namespace SMURF_Ava.Models.DatabaseMappingItems.MappingClasses;

public class Study_Mapper : ClassMap<Study_DB_Item> {

    public Study_Mapper() {
        Table("studytable");
        Id( study => study.DataId);
        Map(study => study.StudyInstanceUID).Column("StudyInstanceUID");
        Map(study => study.StudyDate).Column("StudyDate");
        Map(study => study.PatientID).Column("PatientID");
    }

}