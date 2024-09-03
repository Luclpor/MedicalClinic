using System;
using System.Collections.Generic;

namespace AspNetPatientDoctors.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Address { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Sex { get; set; } = null!;

    public int? SectorId { get; set; }

    public virtual Sector? Sector { get; set; }
}
