using System.Data;
using ClosedXML.Excel;

namespace MVC.BusinessLogic.Services;

public  class FileService : IFileService
{
    public async Task<byte[]> CreateExcelFileAsync(DataTable dt)
    {
        return await Task.Run(() =>
        {
            using XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt);

            using MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);

            return stream.ToArray();
        });
    }
}