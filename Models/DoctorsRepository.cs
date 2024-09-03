using System.Collections;
using System.Data;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AspNetPatientDoctors.Models;
public class DoctorRepository : IRepository<Doctor>
{
    MedicalClinicContext db;
    public DoctorRepository(MedicalClinicContext db)
    {
        this.db = db;
    }
    public object this[int id] => db.Doctors.Where(d => d.DoctorId == id).Select(d => new
    {
        DoctorId = d.DoctorId,
        LastName = d.LastName,
        FirstName = d.FirstName,
        MiddleName = d.MiddleName,
        SectorId = d.SectorId,
        OfficeId = d.OfficeId,
        SpecializationId = d.SpecializationId
    });

    public async Task<object> TestAddElement(Doctor d)
    {
        return await db.SaveChangesAsync();
    }
    public async Task<object> AddElementAsync(Doctor el, HttpResponse response)
    {
        if (el.DoctorId == 0)
        {
            if (!db.Doctors.Any(d_ => d_.SectorId == el.SectorId) || !db.Doctors.Any(d_ => d_.OfficeId == el.OfficeId) || !db.Doctors.Any(d_ => d_.SpecializationId == el.SpecializationId))
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                return el;
            }
            await db.Doctors.AddAsync(el);
            await db.SaveChangesAsync();
            response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                DoctorId = el.DoctorId,
                LastName = el.LastName,
                FirstName = el.FirstName,
                MiddleName = el.MiddleName,
                Office = db.Offices.Where(o => o.OfficeId == el.OfficeId).Select(o => o.Number).First(),
                Sector = db.Sectors.Where(o => o.SectorId == el.SectorId).Select(o => o.Number).First(),
                Specialization = db.Specializations.Where(o => o.SpecializationId == el.SpecializationId).Select(o => o.Title).First()
            };
        }
        else
        {
            if (!db.Doctors.Any(d_ => d_.DoctorId == el.DoctorId))
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                return el;
            }
            db.Doctors.Update(el);
            await db.SaveChangesAsync();
            response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                DoctorId = el.DoctorId,
                LastName = el.LastName,
                FirstName = el.FirstName,
                MiddleName = el.MiddleName,
                SectorId = el.SectorId,
                OfficeId = el.OfficeId,
                SpecializationId = el.SpecializationId
            };
        }
    }

    public async Task DeleteElementAsync(int id, HttpResponse response)
    {
        bool exists = db.Doctors.Any(d => d.DoctorId == id);
        if (exists)
        {
            Doctor doctor = new Doctor { DoctorId = id };
            db.Entry(doctor).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            response.StatusCode = StatusCodes.Status200OK;
            return;
        }
        response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }

    public async Task<object> UpdateElementAsync(Doctor el, HttpResponse response)
    {
        return await AddElementAsync(el,response);
    }

    public IEnumerable<Object> Sort(string sortField, int page, int rows)
    {
        int skip = (page - 1) * rows;
        return db.Doctors.OrderBy(sortField).Skip(skip).Take(rows).Select(d => new
        {
            DoctorId = d.DoctorId,
            LastName = d.LastName,
            FirstName = d.FirstName,
            MiddleName = d.MiddleName,
            Office = d.Office.Number,
            Sector = d.Sector.Number,
            Specialization = d.Specialization.Title
        });
    }
}
