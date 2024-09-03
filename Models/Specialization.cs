using System;
using System.Collections.Generic;

namespace AspNetPatientDoctors.Models;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
