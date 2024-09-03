using System.Reflection;
using AspNetPatientDoctors.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Dynamic.Core;
using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Route("api/[controller]")]
public class PatientController : Controller
{
    private IRepository<Patient> repository;
    public PatientController(IRepository<Patient> repository)
    {
        this.repository = repository;
    }


    [HttpGet("GetSortList")]
    public IActionResult GetSort([FromQuery] string sortField, [FromQuery] int page, [FromQuery] int rows)
    {
        Type type = typeof(Patient);
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
    public IActionResult Post([FromBody] Patient patient)
    {
        if (ModelState.IsValid)
        {
            var response = HttpContext.Response;
            var p = repository.AddElement(patient, response);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(p);
            }
            else if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return new ContentResult
                {
                    Content = $"SectorId: {patient.SectorId} dont exist",
                    ContentType = "text/plain"
                };
            }
        }
        return NotFound("dont correct");

    }

    [HttpPut]
    public IActionResult Put([FromBody] Patient patient)
    {
        var response = HttpContext.Response;
        var p = repository.UpdateElement(patient, response);
        if (response.StatusCode == StatusCodes.Status200OK)
        {
           return  Ok(p);
        }
        else if (response.StatusCode == StatusCodes.Status404NotFound)
        {
            return new ContentResult
            {
                Content = $"Patient with Id: {patient.PatientId} dont exist",
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
                    Content = $"Patient with Id: {id} deleted",
                    ContentType = "text/plain"
                };
            }
            else if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return new ContentResult
                {
                    Content = $"Patient with Id: {id} dont exist",
                    ContentType = "text/plain"
                };
            }

        }
        return BadRequest();
    }
}


