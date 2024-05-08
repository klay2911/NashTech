using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.BusinessLogic.Services;
using MVC.Models.Models;
using MVC.WebApp.Models;
using X.PagedList;

namespace MVC.WebApp.Controllers;

public class PeopleController : BaseController
{
    private readonly IPeopleService _peopleService;
    private readonly IFileService _fileService;

        public PeopleController(IPeopleService peopleService, IFileService fileService)
    {
        _peopleService = peopleService;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<ActionResult> Index(int? page)
    {
        page ??= 1;
        int pageSize = 3;
        int pageNumber = (int)page;
        var people = (await _peopleService.GetAllAsync()).OrderBy(p=>p.FullName).ToPagedList(pageNumber, pageSize);
        return View(people);
    }

    public async Task<IActionResult> GetMalePersons()
    {
        var malePersons = await _peopleService.GetMaleMembersAsync();
        return View(malePersons);
    }

    public async Task<IActionResult> GetOldestPerson()
    {
        var oldestPerson = await _peopleService.GetOldestPersonAsync();
        return View(oldestPerson);
    }

    public async Task<IActionResult> GetFullNames()
    {
        var fullNameMembers = await _peopleService.GetFullNameMembersAsync();
        return View(fullNameMembers);
    }
    
    public async Task<IActionResult> GetMembersBornInYear(int year = 2000)
    {
        var members = await _peopleService.GetMembersBornInYearAsync(year);
        return View(members);
    }
    public async Task<IActionResult> GetMembersBornAfterYear(int year = 2000)
    {
        var members = await _peopleService.GetMembersBornAfterYearAsync(year);
        return View(members);
    }
    public async Task<IActionResult> GetMembersBornBeforeYear(int year = 2000)
    {
        var members = await _peopleService.GetMembersBornBeforeYearAsync(year);
        return View(members);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Member member)
    {
        if (!ModelState.IsValid)
        {
            return View(member);
        }

        await _peopleService.AddAsync(member);
        TempData["Message"] = $"member {member.FullName} was added from the list successfully!";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var member = await _peopleService.GetByIdAsync(id);
        return View(member);
    }

    [HttpPost]
    public async Task<IActionResult> EditSuccess(Guid id, Member updatedmember)
    {
        if (!ModelState.IsValid)
        {
            throw new Exception("Invalid model state!");
        }

        var member = await _peopleService.GetByIdAsync(id);
        if (true)
        {
            member.FirstName = updatedmember.FirstName;
            member.LastName = updatedmember.LastName;
            member.Gender = updatedmember.Gender;
            member.Dob = updatedmember.Dob;
            member.PhoneNumber = updatedmember.PhoneNumber;
            member.BirthPlace = updatedmember.BirthPlace;
            member.IsGraduated = updatedmember.IsGraduated;
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<ActionResult> Details(Guid id)
    {
        var member = await _peopleService.GetByIdAsync(id);
        return View(member);
    }
    
    [HttpGet]
    public async Task<ActionResult> Delete(Guid id) 
    {
        var member = await _peopleService.GetByIdAsync(id);
        return View(member);
    }
    
    [HttpPost]
    public async Task<ActionResult> DeleteConfirm(Guid id) 
    { 
        var member = await _peopleService.GetByIdAsync(id);
        
        await _peopleService.RemoveAsync(id);
        TempData["Message1"] = $"Person {member.FullName} was removed from the list successfully!";

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<FileResult> Export()
    {
        var data = await _peopleService.ExportToExcelAsync();
        var excelData = await _fileService.CreateExcelFileAsync(data);
        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
