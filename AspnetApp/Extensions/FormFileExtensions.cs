using AspnetApp.Service.DTOs.Attachments;

namespace AspnetApp.Extensions
{
    public static class FormFileExtensions
    {
        public static AttachmentForCreationDTO ToAttachmentOrDefault(this IFormFile formFile)
        {
            if (formFile?.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    var filebyte = memoryStream.ToArray();
                    return new AttachmentForCreationDTO()
                    {
                        Name = Path.GetExtension(formFile.FileName),
                        Stream = new MemoryStream(filebyte)
                    };
                }
            }
            return null;
        }
    }
}