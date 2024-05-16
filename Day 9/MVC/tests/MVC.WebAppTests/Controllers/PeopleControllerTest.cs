using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using MVC.BusinessLogic.Services;
using MVC.Models.Enum;
using MVC.Models.Models;
using MVC.WebApp.Controllers;
using X.PagedList;

namespace MVC.WebAppTests.Controllers;

public class PeopleControllerTest
{
    private PeopleController _controller = null!;
    private Mock<IPeopleService> _mockService = null!;
    private Mock<IFileService> _mockFileService = null!;
    
    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
    }
    
    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPeopleService>();
        _mockFileService = new Mock<IFileService>();
        _controller = new PeopleController(_mockService.Object, _mockFileService.Object);
    }
    [Test]
    public async Task Index_CallsGetAllAsyncOnService_ReturnsCorrectResult()
    {
        // Arrange
        var expectedPeople = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedPeople);

        // Act
        var result = await _controller.Index(1);

        // Assert
        _mockService.Verify(service => service.GetAllAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedPeople, viewResult?.Model);
    }
    [Test]
    public async Task Index_WithNullPageNumber_CallsGetAllAsyncOnService_ReturnsCorrectResult()
    {
        // Arrange
        var expectedPeople = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedPeople);

        // Act
        var result = await _controller.Index(null);

        // Assert
        _mockService.Verify(service => service.GetAllAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedPeople, viewResult?.Model);
    }

    [Test]
    public async Task Index_WithPageNumber1_CallsGetAllAsyncOnService_ReturnsCorrectResult()
    {
        // Arrange
        var expectedPeople = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedPeople);

        // Act
        var result = await _controller.Index(1);

        // Assert
        _mockService.Verify(service => service.GetAllAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedPeople, viewResult?.Model);
    }

    [Test]
    public async Task Index_CallsGetAllAsyncOnPageNumber2_ReturnsCorrectResult()
    {
        // Arrange
        var expectedPeople = new List<Member>
        {
            new() { FirstName = "Test", LastName = "User" },
            new() { FirstName = "Test1", LastName = "User1" },
            new() { FirstName = "Test2", LastName = "User2" },
            new() { FirstName = "Test3", LastName = "User3" }
        };
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedPeople);

        // Act
        var result = await _controller.Index(2);

        // Assert
        _mockService.Verify(service => service.GetAllAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        var model = viewResult?.Model as IPagedList<Member>;
        Assert.IsNotNull(model);
        Assert.AreEqual(1, model.Count); 
        Assert.AreEqual(expectedPeople[3].FirstName, model.First().FirstName); 
        Assert.AreEqual(expectedPeople[3].LastName, model.First().LastName);
    }

    [Test]
    public async Task Index_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<Member>());

        // Act
        var result = await _controller.Index(1);

        // Assert
        _mockService.Verify(service => service.GetAllAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        var model = viewResult?.Model as IPagedList<Member>;
        Assert.IsNotNull(model);
        Assert.AreEqual(0, model.Count);
    }

    [Test]
    public void Index_WhenGetAllAsyncThrowsException_HandlesException()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllAsync()).ThrowsAsync(new Exception());

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _controller.Index(1));
    }

    [Test]
    public async Task GetMalePersons_CallsGetMaleMembersAsyncOnService_ReturnsCorrectResult()
    {
        // Arrange
        var expectedPeople = new List<Member> { new() { FirstName = "Test", LastName = "User", Gender = GenderType.Male } };
        _mockService.Setup(service => service.GetMaleMembersAsync()).ReturnsAsync(expectedPeople);

        // Act
        var result = await _controller.GetMalePersons();

        // Assert
        _mockService.Verify(service => service.GetMaleMembersAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedPeople, viewResult.Model);
    }

    [Test]
    public async Task GetOldestPerson_CallsGetOldestPersonAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMember = new Member { FirstName = "Test", LastName = "User" };
        _mockService.Setup(service => service.GetOldestPersonAsync()).ReturnsAsync(expectedMember);

        // Act
        var result = await _controller.GetOldestPerson();

        // Assert
        _mockService.Verify(service => service.GetOldestPersonAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedMember, viewResult?.Model);
    }

    [Test]
    public async Task GetFullNames_CallsGetFullNameMembersAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetFullNameMembersAsync()).ReturnsAsync(expectedMembers);

        // Act
        var result = await _controller.GetFullNames();

        // Assert
        _mockService.Verify(service => service.GetFullNameMembersAsync(), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedMembers, viewResult?.Model);
    }

    [Test]
    public async Task GetMembersBornInYear_CallsGetMembersBornInYearAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetMembersBornInYearAsync(It.IsAny<int>())).ReturnsAsync(expectedMembers);

        // Act
        var result = await _controller.GetMembersBornInYear(2000);

        // Assert
        _mockService.Verify(service => service.GetMembersBornInYearAsync(2000), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedMembers, viewResult?.Model);
    }

    [Test]
    public async Task GetMembersBornAfterYear_CallsGetMembersBornAfterYearAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetMembersBornAfterYearAsync(It.IsAny<int>())).ReturnsAsync(expectedMembers);

        // Act
        var result = await _controller.GetMembersBornAfterYear(2000);

        // Assert
        _mockService.Verify(service => service.GetMembersBornAfterYearAsync(2000), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedMembers, viewResult?.Model);
    }

    [Test]
    public async Task GetMembersBornBeforeYear_CallsGetMembersBornBeforeYearAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockService.Setup(service => service.GetMembersBornBeforeYearAsync(It.IsAny<int>())).ReturnsAsync(expectedMembers);

        // Act
        var result = await _controller.GetMembersBornBeforeYear(2000);

        // Assert
        _mockService.Verify(service => service.GetMembersBornBeforeYearAsync(2000), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedMembers, viewResult?.Model);
    }

    [Test]
    public void Create_ReturnsView()
    {
        // Act
        var result = _controller.Create();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }
    
    [Test]
    public async Task Create_InvalidModelState_ReturnsViewWithMember()
    {
        // Arrange
        var newMember = new Member { FirstName = "Test", LastName = "User" };
        _controller.ModelState.AddModelError("Error", "Invalid model state");

        // Act
        var result = await _controller.Create(newMember);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(newMember, viewResult?.Model);
    }

    [Test]
    public async Task Create_ValidMember_ReturnsRedirectToActionResultAndSetsTempData()
    {
        // Arrange
        var newMember = new Member { FirstName = "Test", LastName = "User" };
        _mockService.Setup(service => service.AddAsync(newMember)).Returns(Task.CompletedTask);

        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext,
        };
        _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

        // Act
        var result = await _controller.Create(newMember);

        // Assert
        _mockService.Verify(service => service.AddAsync(newMember), Times.Once);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual($"member {newMember.FullName} was added from the list successfully!", _controller.TempData["Message"]);
    }
    
    [Test]
    public async Task Edit_ValidId_ReturnsViewResultWithMember()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedMember = new Member { FirstName = "Test", LastName = "User" };
        _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(expectedMember);

        // Act
        var result = await _controller.Edit(id);

        // Assert
        _mockService.Verify(service => service.GetByIdAsync(id), Times.Once);
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(expectedMember, viewResult?.Model);
    }

    [Test]
    public Task Edit_InvalidId_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(service => service.GetByIdAsync(id)).ThrowsAsync(new Exception());

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _controller.Edit(id));
        return Task.CompletedTask;
    }

    [Test]
    public void EditSuccess_InvalidModelState_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updatedMember = new Member { FirstName = "Test", LastName = "User" };
        _controller.ModelState.AddModelError("Error", "Invalid model state");

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _controller.EditSuccess(id, updatedMember));
    }

    [Test]
    public void EditSuccess_NonexistentMember_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updatedMember = new Member { FirstName = "Test", LastName = "User" };
        _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync((Member)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(async () => await _controller.EditSuccess(id, updatedMember));
        Assert.AreEqual("Member not found", ex.Message);
    }
    
    [Test]
    public async Task EditSuccess_ValidModelStateAndExistingMember_RedirectsToIndex()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updatedMember = new Member { Id = id, FirstName = "Test", LastName = "User" }; // Set the Id of updatedMember
        _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(updatedMember);
        _mockService.Setup(service => service.UpdateAsync(id, updatedMember)).Returns(Task.CompletedTask);
    
        // Act
        var result = await _controller.EditSuccess(id, updatedMember);
    
        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Index", redirectResult?.ActionName);
    }
    
    
    [Test]
    public async Task Details_ValidId_ReturnsViewResultWithMember()
    {
    // Arrange
    var id = Guid.NewGuid();
    var expectedMember = new Member { FirstName = "Test", LastName = "User" };
    _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(expectedMember);

    // Act
    var result = await _controller.Details(id);

    // Assert
    _mockService.Verify(service => service.GetByIdAsync(id), Times.Once);
    Assert.IsInstanceOf<ViewResult>(result);
    var viewResult = result as ViewResult;
    Assert.AreEqual(expectedMember, viewResult?.Model);
    }

    [Test]
    public async Task Delete_ValidId_ReturnsViewResultWithMember()
    {
    // Arrange
    var id = Guid.NewGuid();
    var expectedMember = new Member { FirstName = "Test", LastName = "User" };
    _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(expectedMember);

    // Act
    var result = await _controller.Delete(id);

    // Assert
    _mockService.Verify(service => service.GetByIdAsync(id), Times.Once);
    Assert.IsInstanceOf<ViewResult>(result);
    var viewResult = result as ViewResult;
    Assert.AreEqual(expectedMember, viewResult?.Model);
    }

    [Test]
    public async Task DeleteConfirm_ValidId_RedirectsToIndexAndSetsTempData()
    {
    // Arrange
    var id = Guid.NewGuid();
    var member = new Member { FirstName = "Test", LastName = "User" };
    _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(member);
    _mockService.Setup(service => service.RemoveAsync(id)).Returns(Task.FromResult(true));
    
    var httpContext = new DefaultHttpContext();
    _controller.ControllerContext = new ControllerContext()
    {
    HttpContext = httpContext,
    };
    _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

    // Act
    var result = await _controller.DeleteConfirm(id);

    // Assert
    _mockService.Verify(service => service.RemoveAsync(id), Times.Once);
    Assert.IsInstanceOf<RedirectToActionResult>(result);
    Assert.AreEqual($"Person {member.FullName} was removed from the list successfully!", _controller.TempData["Message1"]);
    }
    
    [Test]
    public async Task Export_CallsExportToExcelAsyncOnService_ReturnsFileResult()
    {
        // Arrange
        var dataTable = new DataTable();
        var fileBytes = Array.Empty<byte>();
        _mockService.Setup(service => service.ExportToExcelAsync()).ReturnsAsync(dataTable);
        _mockFileService.Setup(service => service.CreateExcelFileAsync(dataTable)).ReturnsAsync(fileBytes);

        // Act
        var result = await _controller.Export();

        // Assert
        _mockService.Verify(service => service.ExportToExcelAsync(), Times.Once);
        _mockFileService.Verify(service => service.CreateExcelFileAsync(dataTable), Times.Once);
        Assert.IsInstanceOf<FileResult>(result);
    }

}