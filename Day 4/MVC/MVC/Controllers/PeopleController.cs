using System.Data;
using System.Diagnostics;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using FileResult = Microsoft.AspNetCore.Mvc.FileResult;


namespace MVC.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    public PeopleController(ILogger<PeopleController> logger)
    {
        _logger = logger;
    }
    
    static readonly List<Person> People = new()
   {
        new Person("Nguyen Van", "Nam", GenderType.Male, new DateTime(1999, 06, 02), "0945628812", "Nam Dinh", true),
        new Person("Do Tuan", "Duc", GenderType.Male, new DateTime(2000, 11, 08), "0938428762", "Ha Noi", true),
        new Person("Hoang Thanh", "Huong", GenderType.Female, new DateTime(2002, 4, 20), "0948348712", "VietNam", false)
    };
    
    public IActionResult Index()
    {
        return View(People);
    }
    
    public IActionResult MalePersons()
    {
        var malePersons = People.Where(person => person.Gender == GenderType.Male).ToList();

        return View(malePersons);
    }

    public IActionResult OldestPerson()
    {
        var oldestPerson = People.MinBy(person => person.Dob);

        return View(oldestPerson);
    }

    public IActionResult FullNames()
    {
        var persons = People.ToList();

        return View(persons);
    }
    
    public IActionResult Redirect(int year = 2000)
    {
        switch (year)
        {
            case 2000:
                return RedirectToAction("BirthIs2000");
            case >2000:
                return RedirectToAction("BirthGreaterThan2000");
            case <2000:
                return RedirectToAction("BirthLessThan2000");
        }
    }
    public IActionResult BirthIs2000()
    {
        var persons = People.Where(p => p.Dob.Year == 2000).ToList();
        return View(persons);
    }
    public IActionResult BirthGreaterThan2000()
    {
        var persons = People.Where(p => p.Dob.Year > 2000).ToList();
        return View(persons);
    }
    public IActionResult BirthLessThan2000()
    {
        var persons = People.Where(p => p.Dob.Year < 2000).ToList();
        return View(persons);
    }

    [HttpPost]
    public FileResult Export()
    {
        DataTable dt = new DataTable("Grid");

        var properties = typeof(Person).GetProperties();

        foreach (var prop in properties)
        {
            dt.Columns.Add(new DataColumn(prop.Name));
        }

        foreach (var person in People)
        {
            var row = dt.NewRow();
            foreach (var prop in properties)
            {
                row[prop.Name] = prop.GetValue(person);
            }

            dt.Rows.Add(row);
        }

        using XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt);
        using MemoryStream stream = new MemoryStream();
        wb.SaveAs(stream);
        //converted to Byte Array and exported and downloaded as Excel file using the File function
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    } 
    
    [HttpPost]
    public IActionResult Create(Person person, string action)
    {
        if (action == "Add More")
        {
            People.Add(person);
            TempData["Message"] = $"Person {person.FullName} created successfully";
            return RedirectToAction("Create");
        }
        else if (action == "Create")
        {
            People.Add(person);
            return RedirectToAction("Index");
        }
        return View();
    }

    public ActionResult Edit(Guid id)
    {
        var person = People.FirstOrDefault(s => s.Id == id);
        return View(person);
    }
    [HttpPost]
    public ActionResult EditSuccess(Guid id, Person updatedPerson)
    {
        var person = People.FirstOrDefault(s => s.Id == id);
        if (person != null)
        {
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Gender = updatedPerson.Gender;
            person.Dob = updatedPerson.Dob;
            person.PhoneNumber = updatedPerson.PhoneNumber;
            person.BirthPlace = updatedPerson.BirthPlace;
            person.IsGraduated = updatedPerson.IsGraduated;
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public ActionResult Details(Guid id)
    {
      var person = People.FirstOrDefault(s => s.Id == id);
      return View(person);
    }
    
    [HttpGet]
    public ActionResult Delete(Guid id) 
    {
      var person = People.FirstOrDefault(s => s.Id == id);
      return View(person);
    }
    //Case null cant find id of the person 
    [HttpPost]
    public ActionResult DeleteConfirm(Guid id) 
    { 
        var person = People.FirstOrDefault(s => s.Id == id);
        
        if (person != null) People.Remove(person);
        return RedirectToAction("Index");
    }
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}