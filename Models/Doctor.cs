using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetPatientDoctors.Models;

public partial class Doctor
{
    
    public int DoctorId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }
    [Display(Name = "Office number")]
    public int? OfficeId { get; set; }

    public int? SpecializationId { get; set; }
    [Display(Name = "Sector number")]
    public int? SectorId { get; set; }

    public virtual Office? Office { get; set; }

    public virtual Sector? Sector { get; set; }

    public virtual Specialization? Specialization { get; set; }
}
