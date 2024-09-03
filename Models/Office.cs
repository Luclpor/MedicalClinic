using System;
using System.Collections.Generic;

namespace AspNetPatientDoctors.Models;

public partial class Office
{
    public int OfficeId { get; set; }

    public int Number { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
