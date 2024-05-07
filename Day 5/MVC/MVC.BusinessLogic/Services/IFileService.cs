using System.Data;

namespace MVC.BusinessLogic.Services;

public interface IFileService
{
    Task<byte[]> CreateExcelFileAsync(DataTable dt);
}