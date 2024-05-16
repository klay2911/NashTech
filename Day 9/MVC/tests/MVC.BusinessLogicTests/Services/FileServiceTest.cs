using System.Data;
using MVC.BusinessLogic.Services;

namespace MVC.BusinessLogicTests.Services;

public class FileServiceTest
{
    private FileService _service;

    [SetUp]
    public void Setup()
    {
        _service = new FileService();
    }

    [Test]
    public void FileService_Constructor_ShouldCreateInstance()
    {
        // Arrange & Act in Setup()

        // Assert
        Assert.IsNotNull(_service);
    }

    [Test]
    public async Task CreateExcelFileAsync_ValidDataTable_ReturnsByteArray()
    {
        // Arrange
        var dt = new DataTable("TestTable");
        dt.Columns.Add("TestColumn");
        dt.Rows.Add("TestValue");
        var service = new FileService();

        // Act
        var result = await service.CreateExcelFileAsync(dt);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<byte[]>(result);
    }
    
}