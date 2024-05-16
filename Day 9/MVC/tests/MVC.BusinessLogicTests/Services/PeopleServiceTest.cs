using System.Data;
using Moq;
using MVC.BusinessLogic.Services;
using MVC.Models.Enum;
using MVC.Models.Models;
using MVC.Repositories.Repositories;

namespace MVC.BusinessLogicTests.Services;

public class PeopleServiceTest
{
    private PeopleService _service = null!;
    private Mock<IPeopleRepository> _mockRepository = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IPeopleRepository>();
        _service = new PeopleService(_mockRepository.Object);
    }

    [Test]
    public void PeopleService_Constructor_ShouldCreateInstance()
    {
        // Assert
        Assert.IsNotNull(_service);
    }

    [Test]
    public async Task GetAllAsync_CallsGetAllAsyncOnRepository_ReturnsMemberList()
    {
        // Arrange
        var expectedMembers = new List<Member> { new() { FirstName = "Test", LastName = "User" } };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedMembers);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        Assert.AreEqual(expectedMembers, result);
    }
    
    [Test]
    public void GetAllAsync_NoMembers_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<Member>)null);

        Assert.ThrowsAsync<Exception>(async () => await _service.GetAllAsync());
    }
    
    [Test]
    public async Task GetByIdAsync_ValidId_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMember = new Member { FirstName = "Test", LastName = "User" };
        var id = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedMember);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        _mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        Assert.AreEqual(expectedMember, result);
    }
    
    [Test]
    public void GetByIdAsync_NoMember_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Member?)null);
        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _service.GetByIdAsync(id));
    }

    [Test]
    public async Task GetMaleMembersAsync_CallsGetMaleMembersAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new Member { FirstName = "Test", LastName = "User", Gender = GenderType.Male } };
        _mockRepository.Setup(repo => repo.GetMaleMembersAsync()).ReturnsAsync(expectedMembers);

        // Act
        var result = await _service.GetMaleMembersAsync();

        // Assert
        _mockRepository.Verify(repo => repo.GetMaleMembersAsync(), Times.Once);
        Assert.AreEqual(expectedMembers, result);
    }
    
    [Test]
    public void GetMaleMembersAsync_NoMembers_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetMaleMembersAsync()).ReturnsAsync((List<Member>)null);

        Assert.ThrowsAsync<Exception>(async () => await _service.GetMaleMembersAsync());
    }
    
    [Test]
    public async Task GetOldestPersonAsync_CallsGetOldestPersonAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMember = new Member { FirstName = "Test", LastName = "User", Dob = new DateOnly(1990, 1, 1) };
        _mockRepository.Setup(repo => repo.GetOldestPersonAsync()).ReturnsAsync(expectedMember);

        // Act
        var result = await _service.GetOldestPersonAsync();

        // Assert
        _mockRepository.Verify(repo => repo.GetOldestPersonAsync(), Times.Once);
        Assert.AreEqual(expectedMember, result);
    }
    
    [Test]
    public void GetOldestPersonAsync_NoMember_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetOldestPersonAsync()).ReturnsAsync((Member?)null);

        Assert.ThrowsAsync<Exception>(async () => await _service.GetOldestPersonAsync());
    }

    [Test]
    public async Task GetFullNameMembersAsync_CallsGetFullNameMembersAsync_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new Member { FirstName = "Test", LastName = "User" } };
        _mockRepository.Setup(repo => repo.GetFullNameMembersAsync()).ReturnsAsync(expectedMembers);

        // Act
        var result = await _service.GetFullNameMembersAsync();

        // Assert
        _mockRepository.Verify(repo => repo.GetFullNameMembersAsync(), Times.Once);
        Assert.AreEqual(expectedMembers, result);
    }

    [Test]
    public void GetFullNameMembersAsync_NoMembers_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetFullNameMembersAsync()).ReturnsAsync((List<Member>)null);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _service.GetFullNameMembersAsync());
    }
    
    [Test]
    public async Task GetMembersBornInYearAsync_ValidYear_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new Member { FirstName = "Test", LastName = "User", Dob = new DateOnly(1990, 1, 1) } };
        _mockRepository.Setup(repo => repo.GetMembersBornInYearAsync(1990)).ReturnsAsync(expectedMembers);

        // Act
        var result = await _service.GetMembersBornInYearAsync(1990);

        // Assert
        _mockRepository.Verify(repo => repo.GetMembersBornInYearAsync(1990), Times.Once);
        Assert.AreEqual(expectedMembers, result);
    }

    [Test]
    public void GetMembersBornInYearAsync_NoMembers_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetMembersBornInYearAsync(It.IsAny<int>())).ReturnsAsync((List<Member>)null);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _service.GetMembersBornInYearAsync(1990));
    }
    
    [Test]
    public async Task GetMembersBornAfterYearAsync_ValidYear_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new Member { FirstName = "Test", LastName = "User", Dob = new DateOnly(1991, 1, 1) } };
        _mockRepository.Setup(repo => repo.GetMembersBornAfterYearAsync(1990)).ReturnsAsync(expectedMembers);

        // Act
        var result = await _service.GetMembersBornAfterYearAsync(1990);

        // Assert
        _mockRepository.Verify(repo => repo.GetMembersBornAfterYearAsync(1990), Times.Once);
        Assert.AreEqual(expectedMembers, result);
    }
    
    [Test]
    public void GetMembersBornAfterYearAsync_NoMembers_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetMembersBornAfterYearAsync(It.IsAny<int>())).ReturnsAsync((List<Member>)null);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _service.GetMembersBornAfterYearAsync(1990));
    }

    [Test]
    public async Task GetMembersBornBeforeYearAsync_ValidYear_ReturnsCorrectResult()
    {
        // Arrange
        var expectedMembers = new List<Member> { new Member { FirstName = "Test", LastName = "User", Dob = new DateOnly(1989, 1, 1) } };
        _mockRepository.Setup(repo => repo.GetMembersBornBeforeYearAsync(1990)).ReturnsAsync(expectedMembers);

        // Act
        var result = await _service.GetMembersBornBeforeYearAsync(1990);

        // Assert
        _mockRepository.Verify(repo => repo.GetMembersBornBeforeYearAsync(1990), Times.Once);
        Assert.AreEqual(expectedMembers, result);
    }
    
    [Test]
    public void GetMembersBornBeforeYearAsync_NoMembers_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetMembersBornBeforeYearAsync(It.IsAny<int>())).ReturnsAsync((List<Member>)null);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _service.GetMembersBornBeforeYearAsync(1990));
    }
    
    [Test]
    public async Task ExportToExcelAsync_CallsGetPeopleAsDataTableAsync_ReturnsCorrectResult()
    {
        // Arrange
        var dataTable = new DataTable();
        _mockRepository.Setup(repo => repo.GetPeopleAsDataTableAsync()).ReturnsAsync(dataTable);

        // Act
        var result = await _service.ExportToExcelAsync();

        // Assert
        _mockRepository.Verify(repo => repo.GetPeopleAsDataTableAsync(), Times.Once);
        Assert.AreEqual(dataTable, result);
    }
    
    [Test]
    public void ExportToExcelAsync_NoDataToExport_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetPeopleAsDataTableAsync())!.ReturnsAsync((DataTable)null!);

        Assert.ThrowsAsync<Exception>(async () => await _service.ExportToExcelAsync());
    }

    [Test]
    public async Task AddAsync_ValidMember_AddsMember()
    {
        // Arrange
        var newMember = new Member { FirstName = "Test", LastName = "User" };
        _mockRepository.Setup(repo => repo.AddAsync(newMember)).Returns(Task.CompletedTask);

        // Act
        await _service.AddAsync(newMember);

        // Assert
        _mockRepository.Verify(repo => repo.AddAsync(newMember), Times.Once);
    }
    
    
    [Test]
    public void AddAsync_InvalidMember_ThrowsException()
    {
        // Arrange
        Member newMember = null;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.AddAsync(newMember!));
    }

    [Test]
    public async Task UpdateAsync_ValidMember_UpdatesMember()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updatedMember = new Member { FirstName = "Test", LastName = "User" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(updatedMember);
        _mockRepository.Setup(repo => repo.UpdateAsync(id, updatedMember)).Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(id, updatedMember);

        // Assert
        _mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        _mockRepository.Verify(repo => repo.UpdateAsync(id, updatedMember), Times.Once);
    }

    [Test]
    public void UpdateAsync_InvalidMember_ThrowsException()
    {
        var id = Guid.NewGuid();
        Member updatedMember = null;

        Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAsync(id, updatedMember));
    }

    [Test]
    public void UpdateAsync_NonexistentId_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updatedMember = new Member { FirstName = "Test", LastName = "User" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Member?)null);

        // Act
        var exception = Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(id, updatedMember));

        // Assert
        Assert.AreEqual($"No member found with ID {id}", exception.Message);
    }

    [Test]
    public async Task RemoveAsync_ValidId_RemovesMember()
    {
        // Arrange
        var id = Guid.NewGuid();
        var member = new Member { FirstName = "Test", LastName = "User" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(member);
        _mockRepository.Setup(repo => repo.RemoveAsync(id)).Returns(Task.CompletedTask);

        // Act
        var result = await _service.RemoveAsync(id);

        // Assert
        _mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        _mockRepository.Verify(repo => repo.RemoveAsync(id), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    public async Task RemoveAsync_WithNonexistentId_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Member)null!);

        // Act
        var result = await _service.RemoveAsync(id);

        // Assert
        _mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        Assert.IsFalse(result);
    }
}