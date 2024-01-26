using System;
using FluentNHibernate.Mapping;

namespace SMURF_Ava.Models.DatabaseMappingItems;

public class Study_DB_Item {

    public virtual string PatientID { get; set; }

    public virtual string StudyInstanceUID { get; set; }

    public virtual DateTime StudyDate { get; set; }

}