using System.Collections;
using AspNetPatientDoctors.Models;

public interface IRepository<T>
{
    object this[int id] {get;}
    Task<object> AddElementAsync(T el,HttpResponse response);
    Task<object> UpdateElementAsync(T el,HttpResponse response);
    Task DeleteElementAsync(int id,HttpResponse response);
    IEnumerable<object> Sort(string sortField, int page, int rows);
}