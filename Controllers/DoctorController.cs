using System.Reflection;
using AspNetPatientDoctors.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Dynamic.Core;
using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Route("api/[controller]")]
public class DoctorController : Controller
{
    private IRepository<Doctor> repository;
    public DoctorController(IRepository<Doctor> repository)
    {
        this.repository = repository;
    }


    [HttpGet("GetSortList")]
    public IActionResult GetSort([FromQuery] string sortField, [FromQuery] int page, [FromQuery] int rows)
    {
        Type type = typeof(Doctor);
        if (type.GetProperty(sortField) != null)
            return Ok(repository.Sort(sortField, page, rows));
        return BadRequest("filed dosnt exist");
    }


    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(repository[id]);
    }


    [HttpPost]
    public IActionResult Post([FromBody] Doctor doctor)
    {
        if (ModelState.IsValid)
        {
            var response = HttpContext.Response;
            var d = repository.AddElement(doctor,response);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(d);
            }
            else if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return new ContentResult
                {
                    Content = $"SectorId: {doctor.SectorId} dont exist",
                    ContentType = "text/plain"
                };
            }
        }
        return NotFound("dont correct");
    }

    [HttpPut]
    public IActionResult Put([FromBody] Doctor doctor)
    {
        var response = HttpContext.Response;
        var p = repository.UpdateElement(doctor, response);
        if (response.StatusCode == StatusCodes.Status200OK)
        {
           return  Ok(p);
        }
        else if (response.StatusCode == StatusCodes.Status404NotFound)
        {
            return new ContentResult
            {
                Content = $"Doctor with Id: {doctor.DoctorId} dont exist",
                ContentType = "text/plain"
            };
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id != null)
        {
            var response = HttpContext.Response;
            repository.DeleteElement(id, response);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                return new ContentResult
                {
                    Content = $"Doctors with Id: {id} deleted",
                    ContentType = "text/plain"
                };
            }
            else if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return new ContentResult
                {
                    Content = $"Doctors with Id: {id} dont exist",
                    ContentType = "text/plain"
                };
            }

        }
        return BadRequest();
    }
}


