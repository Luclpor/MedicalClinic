using System.Collections;
using System.Data;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AspNetPatientDoctors.Models;
public class PatientsRepository : IRepository<Patient>
{
    private Dictionary<int, Doctor> items;
    MedicalClinicContext db;
    public PatientsRepository(MedicalClinicContext db)
    {
        this.db = db;
    }
    public object this[int id] => db.Patients.Where(p => p.PatientId == id).Select(p => new
    {
        PatientId = p.PatientId,
        LastName = p.LastName,
        FirstName = p.FirstName,
        MiddleName = p.MiddleName,
        Address = p.Address,
        DateOfBirth = p.DateOfBirth,
        Sex = p.Sex,
        SectorId = p.SectorId,
    });


    public object AddElement(Patient p, HttpResponse response)
    {
        if (p.PatientId == 0)
        {
            if(!db.Patients.Any(p_ => p_.SectorId == p.SectorId))
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                return p;
            }
            db.Patients.Add(p);
            db.SaveChanges();
            response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                PatientId = p.PatientId,
                LastName = p.LastName,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                Address = p.Address,
                DateOfBirth = p.DateOfBirth,
                Sex = p.Sex,
                Sector = db.Sectors.Where(o => o.SectorId == p.SectorId).Select(o => o.Number).First(),
            };
        }
        else
        {
            if(!db.Patients.Any(p_ => p_.PatientId == p.PatientId))
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                return p;
            }
            db.Patients.Update(p);
            db.SaveChanges();
            response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                DoctorId = p.PatientId,
                LastName = p.LastName,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                Address = p.Address,
                DateOfBirth = p.DateOfBirth,
                Sex = p.Sex,
                SectorId = p.SectorId
            };
        }
    }

    public void DeleteElement(int id, HttpResponse response)
    {
        bool exists = db.Patients.Any(p => p.PatientId == id);
        if (exists)
        {
            Patient patient = new Patient { PatientId = id };
            db.Entry(patient).State = EntityState.Deleted;
            db.SaveChanges();
            response.StatusCode = StatusCodes.Status200OK;
            return;
        }
        response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }

    public Object UpdateElement(Patient p,HttpResponse response)
    {
        return AddElement(p, response);
    }

    public IEnumerable<Object> Sort(string sortField, int page, int rows)
    {
        int skip = (page - 1) * rows;
        return db.Patients.OrderBy(sortField).Skip(skip).Take(rows).Select(p => new
        {
            PatientId = p.PatientId,
            LastName = p.LastName,
            FirstName = p.FirstName,
            MiddleName = p.MiddleName,
            Address = p.Address,
            DateOfBirth = p.DateOfBirth,
            Sex = p.Sex,
            Sector = db.Sectors.Where(o => o.SectorId == p.SectorId).Select(o => o.Number).First()
        });
    }

}
