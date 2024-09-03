using System;
using System.Collections.Generic;

namespace AspNetPatientDoctors.Models;

public partial class Sector
{
    public int SectorId { get; set; }

    public int Number { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
