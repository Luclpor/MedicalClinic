using System.Collections;
using AspNetPatientDoctors.Models;

public interface IRepository<T>
{
    object this[int id] {get;}
    object AddElement(T el,HttpResponse response);
    object UpdateElement(T el,HttpResponse response);
    void DeleteElement(int id,HttpResponse response);
    IEnumerable<object> Sort(string sortField, int page, int rows);
}