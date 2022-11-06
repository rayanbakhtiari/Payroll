using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Payroll.Helper
{
    public class FileHelper
    {
        public static FileStreamResult GetFileResultFrom(string fileAddress)
        {
            string fileName = Path.GetFileName(fileAddress);
            string contentType = "";
            new FileExtensionContentTypeProvider().TryGetContentType(fileAddress, out contentType);
            FileStream fileStream = new FileStream(fileAddress, FileMode.Open, FileAccess.Read);
            if (string.IsNullOrEmpty(contentType))
                throw new FormatException("unkown file forma");
            return new FileStreamResult(fileStream, contentType) { FileDownloadName = fileName};
        }
    }
}
